/*
┌──────────────────────────────────────────────────────────────────┐
│  Author: Tristyn Mackay (https://github.com/InMetaTech-Tristyn)  │
│  Repository: GitHub (https://github.com/TristynMackay/Unity-MCP)    │
│  Copyright (c) 2025 Tristyn Mackay                                  │
│  Licensed under the Apache License, Version 2.0.                 │
│  See the LICENSE file in the project root for more information.  │
└──────────────────────────────────────────────────────────────────┘
*/

#if !UNITY_EDITOR_WIN && !UNITY_EDITOR_OSX && !UNITY_EDITOR_LINUX

#nullable enable

namespace com.InMetaTech.Unity.MCP.Editor.Utils
{
    public static partial class SecureKeyStore
    {
        private static string BuildTargetName(string key)
        {
            return key;
        }

        private static string? Read(string targetName)
        {
            return null;
        }

        private static void Write(string targetName, string value)
        {
        }

        private static void DeleteInternal(string targetName)
        {
        }
    }
}

#endif



