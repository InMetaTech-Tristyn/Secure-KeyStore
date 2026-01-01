/*
┌──────────────────────────────────────────────────────────────────────────────┐
│  Author: Tristyn Mackay (https://github.com/InMetaTech-Tristyn)              │
│  Repository: GitHub (https://github.com/InMetaTech-Tristyn/Secure-Keystore)  │
│  Copyright (c) 2025 Tristyn Mackay                                           │
│  Licensed under the Apache License, Version 2.0.                             │
│  See the LICENSE file in the project root for more information.              │
└──────────────────────────────────────────────────────────────────────────────┘
*/

#nullable enable

using System;
using System.Diagnostics;
using com.InMetaTech.Unity.Editor.Utils;
using NUnit.Framework;
using UnityEngine;

namespace com.InMetaTech.Unity.SecureKeyStore.Editor.Tests
{
    [TestFixture]
    public class SecureKeyStoreTests
    {
        [Test]
        public void EmptyKey_NoThrow()
        {
            Assert.DoesNotThrow(() => SecureKeyStore.Set(string.Empty, "value"));
            Assert.DoesNotThrow(() => SecureKeyStore.Set("   ", "value"));
            Assert.DoesNotThrow(() => SecureKeyStore.Delete(string.Empty));
            Assert.IsNull(SecureKeyStore.Get(string.Empty));
            Assert.IsNull(SecureKeyStore.Get("   "));
        }

        [Test]
        public void RoundTrip_CurrentPlatform()
        {
            if (!TryGetPlatformName(out var platformName, out var warning))
            {
                Assert.Inconclusive(warning);
            }

            RunRoundTrip(platformName);
        }

        [Test]
        public void InMemoryStore_RoundTrip()
        {
            var key = $"unity-inmemory-{Guid.NewGuid():N}";
            const string value = "inmemory-test";

            try
            {
                SecureKeyStore.SetInMemoryForTests(key, value);
                var read = SecureKeyStore.GetInMemoryForTests(key);
                Assert.AreEqual(value, read);

                SecureKeyStore.DeleteInMemoryForTests(key);
                Assert.IsNull(SecureKeyStore.GetInMemoryForTests(key));
            }
            finally
            {
                Assert.DoesNotThrow(() => SecureKeyStore.DeleteInMemoryForTests(key));
            }
        }

        static void RunRoundTrip(string platformName)
        {
            var key = $"unity-test-{Guid.NewGuid():N}";
            const string value = "secure-key-store-test";

            try
            {
                SecureKeyStore.Delete(key);
                SecureKeyStore.Set(key, value);

                var read = SecureKeyStore.Get(key);
                Assert.IsFalse(string.IsNullOrWhiteSpace(read), $"Expected {platformName} secure store to return a value.");
                Assert.AreEqual(value, read);

                SecureKeyStore.Set(key, null);
                var removed = SecureKeyStore.Get(key);
                Assert.IsTrue(string.IsNullOrWhiteSpace(removed), "Expected null value to remove stored key.");
            }
            finally
            {
                Assert.DoesNotThrow(() => SecureKeyStore.Delete(key));
            }
        }

        static bool TryGetPlatformName(out string platformName, out string warning)
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                    platformName = "Windows Credential Manager";
                    warning = string.Empty;
                    return true;
                case RuntimePlatform.OSXEditor:
                    platformName = "macOS Keychain";
                    warning = string.Empty;
                    return true;
                case RuntimePlatform.LinuxEditor:
                    platformName = "Linux Secret Service";
                    if (!IsLinuxSecretServiceAvailable(out warning))
                        return false;
                    warning = string.Empty;
                    return true;
                default:
                    platformName = string.Empty;
                    warning = $"SMOKE: SecureKeyStore test skipped on {Application.platform}. Run on Windows/macOS/Linux to validate OS credential manager integration.";
                    return false;
            }
        }

        static bool IsLinuxSecretServiceAvailable(out string warning)
        {
            warning = string.Empty;

            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = "secret-tool",
                    Arguments = "lookup service com.in-meta-tech.unity account unity-test",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using var process = Process.Start(startInfo);
                if (process == null)
                {
                    warning = "Linux Secret Service unavailable: failed to start secret-tool.";
                    return false;
                }

                var output = process.StandardOutput.ReadToEnd();
                var error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrWhiteSpace(error))
                {
                    var trimmedError = error.Trim();
                    if (trimmedError.Contains("org.freedesktop.secrets", StringComparison.OrdinalIgnoreCase))
                    {
                        warning = $"Linux Secret Service unavailable: {trimmedError}";
                        return false;
                    }

                    if (trimmedError.Contains("not found", StringComparison.OrdinalIgnoreCase))
                    {
                        warning = $"Linux Secret Service unavailable: {trimmedError}";
                        return false;
                    }
                }

                _ = output;
                return true;
            }
            catch (Exception ex)
            {
                warning = $"Linux Secret Service unavailable: {ex.GetBaseException().Message}";
                return false;
            }
        }
    }
}


