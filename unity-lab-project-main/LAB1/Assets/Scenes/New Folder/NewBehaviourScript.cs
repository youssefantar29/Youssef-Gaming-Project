using UnityEngine;
using UnityEditor;

public static class RemoveMissingScriptsPrefabs
{
    [MenuItem("Tools/Remove Missing Scripts In All Prefabs")]
    static void Run()
    {
        int total = 0;
        foreach (var guid in AssetDatabase.FindAssets("t:Prefab"))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var root = PrefabUtility.LoadPrefabContents(path);
            int c = 0;
            foreach (var t in root.GetComponentsInChildren<Transform>(true))
                c += GameObjectUtility.RemoveMonoBehavioursWithMissingScript(t.gameObject);
            if (c > 0) PrefabUtility.SaveAsPrefabAsset(root, path);
            PrefabUtility.UnloadPrefabContents(root);
            total += c;
        }
        Debug.Log("Removed " + total + " missing scripts from prefabs");
    }
}