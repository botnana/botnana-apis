# Changelog

## [0.4.2] - 2026-09-03

### Added

- Published a Win64 `BotnanaApi` package for C++Builder applications. The
  package contains the native DLL, C-compatible header, and import-library
  generation instructions.

### Changed

- Updated the Rust and C library package versions to 0.4.2. No API or runtime
  behavior changed.

## [0.4.1] - 2026-09-03

### Changed

- Updated the Rust and C library package versions to 0.4.1. This release does
  not change API or runtime behavior.

### Fixed

- Recorded the release date for version 0.4.0.

## [0.4.0] - 2026-09-02

### Removed

- Removed the slave, motion, group, and axis configuration mutation helpers from
  the Rust, C, C++, and C# APIs.
- Removed the shared configuration save helper.
- Removed configuration editing from the C examples and the C# axis control.

### Changed

- Configuration examples and the C# axis control now show configuration as
  read-only diagnostic data.
- Machine configuration changes must use the Botnana Control HMI.

Configuration read helpers and the raw message transport remain available.
This release changes the Rust and native public APIs and requires applications
to remove calls to the retired helpers before they upgrade.
