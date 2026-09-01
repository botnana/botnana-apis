# ADR-0001: Remove configuration mutation helpers

## Status

Accepted

## Date

2026-09-02

## Context

Botnana Control configuration protocol v3 requires protocol negotiation, exact
draft revisions, serialized edits, stale-state recovery, and domain-specific
save operations. The existing Botnana APIs send independent fire-and-forget
configuration mutations. They cannot meet the v3 editing contract.

The Botnana Control HMI already owns the operator configuration workflow.
Adding the same stateful workflow to each language binding would create another
configuration client and duplicate concurrency and recovery policy.

## Decision

Remove typed configuration mutation and save helpers from the Rust, C, C++, and
C# APIs. Keep configuration read helpers for diagnostics. Keep the raw message
transport for application-specific protocols.

Make configuration views in repository examples read-only. Direct users to the
Botnana Control HMI for machine configuration changes.

## Alternatives considered

### Implement protocol v3 in every binding

Rejected. This would add session state, mutation serialization, and recovery
policy to a library that primarily provides runtime control.

### Keep obsolete helpers as no-ops

Rejected. Silent success would hide that the requested configuration change did
not occur.

### Remove configuration reads

Rejected. Reads remain compatible and useful for diagnostics. They do not own
configuration workflow state.

## Consequences

- Version 0.4.0 is a breaking API release.
- Applications must remove calls to retired configuration mutation helpers.
- Existing immutable customer release artifacts remain available at their
  historical tags.
- A future automation use case needs a separate protocol-v3 configuration
  client with explicit state and recovery behavior.
