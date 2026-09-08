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
- Expose map, trains, tracks, junctions, signals and occupations as separate read surfaces behind `dispatch.read`.
- Expose junction and route-control intents behind separate permissions.
- Publish network-view, route-control and realtime capabilities.
- Bind authenticated snapshot readers and intent handlers to the Remote Dispatch transport.
- Reject unauthenticated or unbound control requests instead of mutating the world locally.

## Key surfaces

`DispatchWebModule` implements the portable BDVM web registration. `RemoteDispatchBridge` is the transport-specific boundary used by the current composition. The bridge integration source is excluded from the standalone project and linked into `BDVM.Full` until final packaging.

## Boundaries

Dispatch displays authoritative state and forwards user intent. It does not calculate company finances, own trains, set signals directly or make clients authoritative. The frontend source contains no wallet, company, market, lease or ownership path. Without the compatible transport, the module contracts and package remain valid but live data and controls are offline.

## Dependencies

- Build: `BDVM.Common`.
- Runtime platform: a compatible `BDVM.Web` host.
- Current HTTP/browser transport: [Bunchyearth23/dv-remote-dispatch](https://github.com/Bunchyearth23/dv-remote-dispatch), branch `bdvm-integration`.

## Build

With Common checked out beside this repository under `src/`:

```powershell
dotnet build .\BDVM.Dispatch.csproj -c Release
node --test .\tests\dispatch.test.cjs
.\Package.ps1
```

Build `BDVM.Full` to compile the current Remote Dispatch adapter. Building that adapter also requires the compatible fork and its game dependencies.

## Testing and installation

The frontend tests verify inert labels, the five physical-network layers and absence of Management authority. The shared Web tests validate route isolation, permissions and registration. `Package.ps1` produces an independent archive containing this module only; runtime still requires Web and the compatible transport.

## Upstream and provenance

- BDVM fork: [Bunchyearth23/dv-remote-dispatch](https://github.com/Bunchyearth23/dv-remote-dispatch), branch `bdvm-integration`.
- Upstream: [mspielberg/dv-remote-dispatch](https://github.com/mspielberg/dv-remote-dispatch).
- Recorded source revision: `0e032da48550e09e405b4b164f31136f2e925ebf`.
- Upstream license: MIT.

Any incorporated upstream code must retain its copyright and MIT notice.

## Compatibility

The module targets BDVM Web API 1.0. Authentication, authorization and host authority are mandatory for control intents. A future standalone BDVM Web transport may replace Remote Dispatch without changing the Dispatch feature contract.

## License

BDVM code is licensed under the Apache License, Version 2.0. See [LICENSE](LICENSE) and the applied copyright [NOTICE](NOTICE). Incorporated MIT-licensed upstream code remains subject to its original notice.
