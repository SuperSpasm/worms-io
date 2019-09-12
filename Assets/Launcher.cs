using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(Launcher))]
public class LauncherEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = target as Launcher;

        if (GUILayout.Button("Load"))
        {
            script.Load();
        }
    }
}
#endif

public class Launcher : MonoBehaviour
{
    [SerializeField] private int _sceneBuildIndex;
    [SerializeField] private bool _loadOnStart;

    [ContextMenu("Load Scene")]
    public void Load()
    {
        SceneManager.LoadScene(_sceneBuildIndex);
    }
    
    private void Start()
    {
        if (!_loadOnStart) return;
        Load();
    }
}
