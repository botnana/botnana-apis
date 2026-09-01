import re
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


if __name__ == "__main__":
    unittest.main()
