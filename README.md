# BDVM - Dispatch

`BDVM.Dispatch` adds dispatcher views and control intents to the BDVM web platform. Its current transport adapter connects those contracts to the BDVM fork of Remote Dispatch.

## Status

| Property | Value |
| --- | --- |
| Module kind | Web feature and integration adapter |
| Target framework | .NET Framework 4.8 (`net48`) |
| Build dependency | `BDVM.Common` |
| Runtime dependencies | `BDVM.Web`, compatible Remote Dispatch fork |
| Standalone | No |

This repository is not Remote Dispatch itself and is not a web server. The upstream mod is not bundled.

## Responsibilities

- Register the Dispatch navigation entry, frontend asset and realtime topic.
- Expose `GET /api/modules/bdvm.dispatch/snapshot` behind `dispatch.read`.
- Expose `POST /api/modules/bdvm.dispatch/intent` behind `dispatch.control`.
- Publish the `bdvm.dispatch.network-view.v1` capability.
- Bind authenticated snapshot readers and intent handlers to the Remote Dispatch transport.
- Reject unauthenticated or unbound control requests instead of mutating the world locally.

## Key surfaces

`DispatchWebModule` implements the portable BDVM web registration. `RemoteDispatchBridge` is the transport-specific boundary used by the current composition. The bridge integration source is excluded from the standalone project and linked into `BDVM.Full` until final packaging.

## Boundaries

Dispatch displays authoritative state and forwards user intent. It does not calculate company finances, own trains, set signals directly or make clients authoritative. Without the compatible transport, the module contracts can build and register, but the current browser map and Remote Dispatch routes are unavailable.

## Dependencies

- Build: `BDVM.Common`.
- Runtime platform: a compatible `BDVM.Web` host.
- Current HTTP/browser transport: [Bunchyearth23/dv-remote-dispatch](https://github.com/Bunchyearth23/dv-remote-dispatch), branch `bdvm-integration`.

## Build

With Common checked out beside this repository under `src/`:

```powershell
dotnet build .\BDVM.Dispatch.csproj -c Release
```

Build `BDVM.Full` to compile the current Remote Dispatch adapter. Building that adapter also requires the compatible fork and its game dependencies.

## Testing and installation

The BDVM web tests validate route isolation, permissions and registration. The Remote Dispatch fork retains its own test suite. No standalone package is published yet; use matching builds of `BDVM.Full` and the Remote Dispatch fork for integration testing.

## Upstream and provenance

- BDVM fork: [Bunchyearth23/dv-remote-dispatch](https://github.com/Bunchyearth23/dv-remote-dispatch), branch `bdvm-integration`.
- Upstream: [mspielberg/dv-remote-dispatch](https://github.com/mspielberg/dv-remote-dispatch).
- Recorded source revision: `0e032da48550e09e405b4b164f31136f2e925ebf`.
- Upstream license: MIT.

Any incorporated upstream code must retain its copyright and MIT notice.

## Compatibility

The module targets BDVM Web API 1.0. Authentication, authorization and host authority are mandatory for control intents. A future standalone BDVM Web transport may replace Remote Dispatch without changing the Dispatch feature contract.

## License

BDVM code is licensed under the Apache License, Version 2.0. See [LICENSE](LICENSE). Incorporated MIT-licensed upstream code remains subject to its original notice.
