#if UNITY_EDITOR_LINUX

using com.InMetaTech.Unity.MCP.Editor.Utils;
using UnityEditor;

namespace com.InMetaTech.Unity.MCP.SecureKeyStore.Editor
{
    [InitializeOnLoad]
    internal static class SecureKeyStoreStartup
    {
        static SecureKeyStoreStartup()
        {
            LinuxSecretStoreSetup.Init();
        }
    }
}
#endif


