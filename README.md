# Secure Key Store (Unity)

Editor-only secure key storage for Unity tools. Stores secrets per user in the OS credential manager instead of the project directory when available.

## Features

- Uses Windows Credential Manager, macOS Keychain, or Linux Secret Service when available.
- Editor-only API designed to keep secrets out of source control.
- Simple `Get`, `Set`, and `Delete` calls.
- Optional in-memory mode for tests/CI.

## Install (UPM)

Add to `Packages/manifest.json`:

```json
{
  "dependencies": {
    "com.in-meta-tech.unity.securekeystore": "file:../GitPackages/com.in-meta-tech/secure-keystore/Unity-Package/Assets/root"
  }
}
```

If you use a scoped registry, add the package name and registry URL, then reference the version you need.

## Usage

```csharp
using com.InMetaTech.Unity.MCP.Editor.Utils;

SecureKeyStore.Set("OPENAI_API_KEY", "sk-...");
var key = SecureKeyStore.Get("OPENAI_API_KEY");
SecureKeyStore.Delete("OPENAI_API_KEY");
```

Notes:
- `Set(key, null)` or an empty value will delete the entry.
- Storage is per user and not written to the project.

## In-memory mode (tests/CI)

Set the environment variable `UNITY_USE_IN_MEMORY_KEYSTORE` to `1`, `true`, or `yes`.
This keeps secrets in memory for the current editor session only.

## Linux requirements

Linux uses `secret-tool` from `libsecret`. If it is missing, the package logs an error and cannot persist secrets.

## Supported platforms

- Windows Editor
- macOS Editor
- Linux Editor

Other editor platforms return `null` for reads and ignore writes unless in-memory mode is enabled.

## Security and warranty

This package is provided "as is" without warranties or guarantees of any kind. Security depends on your OS, environment, and usage. You are responsible for evaluating the package for your needs and compliance requirements.

## License

Apache 2.0. See `LICENSE`.
