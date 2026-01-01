#if UNITY_EDITOR_LINUX

using com.InMetaTech.Unity.MCP.Editor.Utils;
using UnityEditor;

namespace com.InMetaTech.Unity.MCP.SecureKeyStore.Editor
{
    internal static class SecureKeyStoreMenuItems
    {
        [MenuItem("Tools/AI Game Developer/Setup Linux Credential Store", priority = 1004)]
        public static void SetupLinuxCredentialStore() => LinuxSecretStoreSetup.CheckFromMenu();
    }
}
#endif


