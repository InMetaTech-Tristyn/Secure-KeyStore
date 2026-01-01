/*
┌──────────────────────────────────────────────────────────────────────────────┐
│  Author: Tristyn Mackay (https://github.com/InMetaTech-Tristyn)              │
│  Repository: GitHub (https://github.com/InMetaTech-Tristyn/Secure-Keystore)  │
│  Copyright (c) 2025 Tristyn Mackay                                           │
│  Licensed under the Apache License, Version 2.0.                             │
│  See the LICENSE file in the project root for more information.              │
└──────────────────────────────────────────────────────────────────────────────┘
*/

#if UNITY_EDITOR_LINUX

using com.InMetaTech.Unity.Editor.Utils;
using UnityEditor;

namespace com.InMetaTech.Unity.SecureKeyStore.Editor
{
    internal static class SecureKeyStoreMenuItems
    {
        [MenuItem("Tools/AI Game Developer/Setup Linux Credential Store", priority = 1004)]
        public static void SetupLinuxCredentialStore() => LinuxSecretStoreSetup.CheckFromMenu();
    }
}
#endif


