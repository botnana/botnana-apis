# Changelog

## [0.4.0] - Unreleased

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
