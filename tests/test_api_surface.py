import re
import tomllib
import unittest
from pathlib import Path


REPOSITORY_ROOT = Path(__file__).resolve().parents[1]
SOURCE_SUFFIXES = {".c", ".cpp", ".cs", ".h", ".rs"}
MUTATION_PATTERNS = [
    re.compile(r"\b(?:config|configure)_(?:slave|motion|group|axis)_set(?:_[A-Za-z0-9_]+)?\s*\("),
    re.compile(r"\bConfig(?:Slave|Motion|Group|Axis)Set[A-Za-z0-9_]*\s*\("),
    re.compile(r"\b(?:config_save|configure_save|ConfigSave)\s*\("),
    re.compile(r'"method":"config\.(?:slave|motion|group|axis)\.set"'),
    re.compile(r'"method":"config\.save"'),
]


def production_source_files():
    for path in REPOSITORY_ROOT.rglob("*"):
        if path.suffix not in SOURCE_SUFFIXES:
            continue
        if any(part in {".git", "bin", "obj", "target", "tests"} for part in path.parts):
            continue
        yield path


class PublicApiSurfaceTest(unittest.TestCase):
    def test_configuration_mutation_helpers_removed_then_source_has_no_mutation_surface(self):
        # Arrange
        matches = []

        # Act
        for path in production_source_files():
            for line_number, line in enumerate(path.read_text(errors="replace").splitlines(), 1):
                if any(pattern.search(line) for pattern in MUTATION_PATTERNS):
                    matches.append(f"{path.relative_to(REPOSITORY_ROOT)}:{line_number}: {line.strip()}")

        # Assert
        self.assertEqual([], matches, "\n".join(matches))

    def test_configuration_mutations_removed_then_reads_and_raw_transport_remain_available(self):
        # Arrange
        rust_api = (REPOSITORY_ROOT / "botnanars/src/json_api.rs").read_text()
        c_header = (REPOSITORY_ROOT / "botnanac/src/botnana.h").read_text()

        # Act
        domains = ("slave", "motion", "group", "axis")
        rust_reads = [f'"method":"config.{domain}.get"' in rust_api for domain in domains]
        c_reads = [f"config_{domain}_get(" in c_header for domain in domains]
        raw_transport_available = "void botnana_send_message(" in c_header

        # Assert
        self.assertTrue(all(rust_reads))
        self.assertTrue(all(c_reads))
        self.assertTrue(raw_transport_available)

    def test_axis_configuration_becomes_read_only_then_designer_has_no_missing_handlers(self):
        # Arrange
        control_path = REPOSITORY_ROOT / "botnanacs/BotnanaClassLib/BotnanaClassLib/AxisControl.cs"
        designer_path = REPOSITORY_ROOT / "botnanacs/BotnanaClassLib/BotnanaClassLib/AxisControl.Designer.cs"
        control = control_path.read_text()
        designer = designer_path.read_text()

        # Act
        declared_handlers = set(re.findall(r"private void (\w+)\(", control))
        wired_handlers = set(re.findall(r"\+= new [^(]+\(this\.(\w+)\);", designer))

        # Assert
        self.assertIn('tabPageAxisConfig.Text = "Configuration (read only)"', control)
        self.assertEqual(set(), wired_handlers - declared_handlers)

    def test_release_workflow_builds_and_publishes_the_win64_cplusplusbuilder_package(self):
        # Arrange
        release_workflow_path = REPOSITORY_ROOT / ".github/workflows/release.yml"
        win64_workflow_path = REPOSITORY_ROOT / ".github/workflows/build-win64.yml"
        main_workflow_path = REPOSITORY_ROOT / ".github/workflows/main.yml"
        project_path = REPOSITORY_ROOT / "botnanacs/BotnanaApi/BotnanaApi/BotnanaApi.vcxproj"
        header_path = REPOSITORY_ROOT / "botnanacs/BotnanaApi/BotnanaApi/BotnanaApi.h"
        c_manifest_path = REPOSITORY_ROOT / "botnanac/Cargo.toml"
        rust_manifest_path = REPOSITORY_ROOT / "botnanars/Cargo.toml"
        rust_lock_path = REPOSITORY_ROOT / "botnanars/Cargo.lock"

        # Act
        release_workflow = release_workflow_path.read_text()
        win64_workflow = win64_workflow_path.read_text()
        main_workflow = main_workflow_path.read_text()
        project = project_path.read_text()
        header = header_path.read_text()
        c_version = tomllib.loads(c_manifest_path.read_text())["package"]["version"]
        rust_version = tomllib.loads(rust_manifest_path.read_text())["package"]["version"]
        rust_lock = rust_lock_path.read_text()

        # Assert
        self.assertEqual(c_version, rust_version)
        self.assertRegex(rust_lock, rf'name = "botnanars"\nversion = "{re.escape(rust_version)}"')
        self.assertIn("tomllib", release_workflow)
        self.assertIn("cargo test --locked", release_workflow)
        self.assertIn("uses: ./.github/workflows/build-win64.yml", release_workflow)
        self.assertIn("uses: ./.github/workflows/build-win64.yml", main_workflow)
        self.assertIn("windows-2022", win64_workflow)
        self.assertIn("x86_64-pc-windows-msvc", win64_workflow)
        self.assertIn("/p:Configuration=Release /p:Platform=x64", win64_workflow)
        self.assertIn("BotnanaApi_x86_64.dll", win64_workflow)
        self.assertIn("BotnanaApi.h", win64_workflow)
        self.assertIn("SHA256SUMS", release_workflow)
        self.assertIn('gh release view "$GITHUB_REF_NAME"', release_workflow)
        self.assertIn('gh release create "$GITHUB_REF_NAME"', release_workflow)
        self.assertIn("contents: read", release_workflow)
        self.assertNotIn("CARGO_HTTP_CHECK_REVOKE", win64_workflow)
        self.assertIn('#ifdef __cplusplus\nextern "C" {\n#endif', header)
        self.assertIn('#ifdef __cplusplus\n}\n#endif', header)
        self.assertRegex(
            project,
            r"(?s)Release\|x64.*?<PlatformToolset>v143</PlatformToolset>",
            "The published Win64 DLL must use the supported hosted-runner toolset.",
        )


if __name__ == "__main__":
    unittest.main()
