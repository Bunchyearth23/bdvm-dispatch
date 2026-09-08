using System;

namespace BDVM;

public static class RemoteDispatchBridge
{
    private static readonly object Gate = new object();
    private static Func<string, string>? stateReader;
    private static Func<string, string, string>? intentHandler;

    public static void Configure(Func<string, string> readState, Func<string, string, string> submitIntent)
    { lock (Gate) { stateReader = readState ?? throw new ArgumentNullException(nameof(readState)); intentHandler = submitIntent ?? throw new ArgumentNullException(nameof(submitIntent)); } }

    public static string GetState(string authenticatedTransportIdentity)
    { lock (Gate) return (stateReader ?? throw new InvalidOperationException("BDVM runtime bridge is unavailable."))(RequireIdentity(authenticatedTransportIdentity)); }

    public static string SubmitIntent(string authenticatedTransportIdentity, string payload)
    { lock (Gate) return (intentHandler ?? throw new InvalidOperationException("BDVM runtime bridge is unavailable."))(RequireIdentity(authenticatedTransportIdentity), payload ?? ""); }

    private static string RequireIdentity(string identity) => !string.IsNullOrWhiteSpace(identity) && identity.Length <= 96 ? identity : throw new UnauthorizedAccessException("An authenticated bounded RemoteDispatch identity is required.");
}
