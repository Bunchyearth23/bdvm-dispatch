using System;

namespace BDVM;

public static class RemoteDispatchBridge
{
    private static readonly object Gate = new object();
    private static Func<string, string>? stateReader;
    private static Func<string, string, string>? intentHandler;
    private static Func<string, bool, string>? trustedStateReader;
    private static Func<string, string, bool, string>? trustedIntentHandler;
    private static Func<string, string>? webShellReader;
    private static Func<string, string>? managementReader;
    private static Func<string, string, string>? managementIntentHandler;
    private static Func<string, string, string>? webAssetReader;
    private static Func<string, bool, string>? trustedWebShellReader;
    private static Func<string, bool, string>? trustedManagementReader;
    private static Func<string, string, bool, string>? trustedManagementIntentHandler;
    private static Func<string, string, bool, string>? trustedWebAssetReader;

    public static void Configure(Func<string, string> readState, Func<string, string, string> submitIntent)
    { lock (Gate) { stateReader = readState ?? throw new ArgumentNullException(nameof(readState)); intentHandler = submitIntent ?? throw new ArgumentNullException(nameof(submitIntent)); } }

    public static void ConfigureTrustedTransport(Func<string, bool, string> readState, Func<string, string, bool, string> submitIntent)
    { lock (Gate) { trustedStateReader = readState ?? throw new ArgumentNullException(nameof(readState)); trustedIntentHandler = submitIntent ?? throw new ArgumentNullException(nameof(submitIntent)); } }

    public static void ConfigureWeb(Func<string, string> readShell, Func<string, string> readManagement,
        Func<string, string, string> submitManagementIntent, Func<string, string, string> readAsset)
    {
        lock (Gate)
        {
            webShellReader = readShell ?? throw new ArgumentNullException(nameof(readShell));
            managementReader = readManagement ?? throw new ArgumentNullException(nameof(readManagement));
            managementIntentHandler = submitManagementIntent ?? throw new ArgumentNullException(nameof(submitManagementIntent));
            webAssetReader = readAsset ?? throw new ArgumentNullException(nameof(readAsset));
        }
    }

    public static void ConfigureTrustedWebTransport(Func<string, bool, string> readShell, Func<string, bool, string> readManagement,
        Func<string, string, bool, string> submitManagementIntent, Func<string, string, bool, string> readAsset)
    {
        lock (Gate)
        {
            trustedWebShellReader = readShell ?? throw new ArgumentNullException(nameof(readShell));
            trustedManagementReader = readManagement ?? throw new ArgumentNullException(nameof(readManagement));
            trustedManagementIntentHandler = submitManagementIntent ?? throw new ArgumentNullException(nameof(submitManagementIntent));
            trustedWebAssetReader = readAsset ?? throw new ArgumentNullException(nameof(readAsset));
        }
    }

    public static string GetState(string authenticatedTransportIdentity)
    { lock (Gate) return (stateReader ?? throw new InvalidOperationException("BDVM runtime bridge is unavailable."))(RequireIdentity(authenticatedTransportIdentity)); }

    public static string GetState(string authenticatedTransportIdentity, bool isLoopbackRequest)
    { lock (Gate) return (trustedStateReader ?? ((identity, _) => (stateReader ?? throw new InvalidOperationException("BDVM runtime bridge is unavailable."))(identity)))(RequireIdentity(authenticatedTransportIdentity), isLoopbackRequest); }

    public static string SubmitIntent(string authenticatedTransportIdentity, string payload)
    { lock (Gate) return (intentHandler ?? throw new InvalidOperationException("BDVM runtime bridge is unavailable."))(RequireIdentity(authenticatedTransportIdentity), payload ?? ""); }

    public static string SubmitIntent(string authenticatedTransportIdentity, string payload, bool isLoopbackRequest)
    { lock (Gate) return (trustedIntentHandler ?? ((identity, body, _) => (intentHandler ?? throw new InvalidOperationException("BDVM runtime bridge is unavailable."))(identity, body)))(RequireIdentity(authenticatedTransportIdentity), payload ?? "", isLoopbackRequest); }

    public static string GetWebShell(string authenticatedTransportIdentity)
    { lock (Gate) return (webShellReader ?? throw new InvalidOperationException("BDVM web bridge is unavailable."))(RequireIdentity(authenticatedTransportIdentity)); }

    public static string GetWebShell(string authenticatedTransportIdentity, bool isLoopbackRequest)
    { lock (Gate) return (trustedWebShellReader ?? ((identity, _) => (webShellReader ?? throw new InvalidOperationException("BDVM web bridge is unavailable."))(identity)))(RequireIdentity(authenticatedTransportIdentity), isLoopbackRequest); }

    public static string GetManagementState(string authenticatedTransportIdentity)
    { lock (Gate) return (managementReader ?? throw new InvalidOperationException("BDVM management bridge is unavailable."))(RequireIdentity(authenticatedTransportIdentity)); }

    public static string GetManagementState(string authenticatedTransportIdentity, bool isLoopbackRequest)
    { lock (Gate) return (trustedManagementReader ?? ((identity, _) => (managementReader ?? throw new InvalidOperationException("BDVM management bridge is unavailable."))(identity)))(RequireIdentity(authenticatedTransportIdentity), isLoopbackRequest); }

    public static string SubmitManagementIntent(string authenticatedTransportIdentity, string payload)
    { lock (Gate) return (managementIntentHandler ?? throw new InvalidOperationException("BDVM management bridge is unavailable."))(RequireIdentity(authenticatedTransportIdentity), payload ?? ""); }

    public static string SubmitManagementIntent(string authenticatedTransportIdentity, string payload, bool isLoopbackRequest)
    { lock (Gate) return (trustedManagementIntentHandler ?? ((identity, body, _) => (managementIntentHandler ?? throw new InvalidOperationException("BDVM management bridge is unavailable."))(identity, body)))(RequireIdentity(authenticatedTransportIdentity), payload ?? "", isLoopbackRequest); }

    public static string GetWebAsset(string authenticatedTransportIdentity, string assetKey)
    { lock (Gate) return (webAssetReader ?? throw new InvalidOperationException("BDVM web asset bridge is unavailable."))(RequireIdentity(authenticatedTransportIdentity), assetKey ?? ""); }

    public static string GetWebAsset(string authenticatedTransportIdentity, string assetKey, bool isLoopbackRequest)
    { lock (Gate) return (trustedWebAssetReader ?? ((identity, key, _) => (webAssetReader ?? throw new InvalidOperationException("BDVM web asset bridge is unavailable."))(identity, key)))(RequireIdentity(authenticatedTransportIdentity), assetKey ?? "", isLoopbackRequest); }

    private static string RequireIdentity(string identity) => !string.IsNullOrWhiteSpace(identity) && identity.Length <= 96 ? identity : throw new UnauthorizedAccessException("An authenticated bounded RemoteDispatch identity is required.");
}
