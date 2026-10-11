using UnityEditor;
using UnityEngine;

public class SceneValidatorWindow : EditorWindow
{
    [MenuItem(
        "Advanced Arena/Scene Validator")]
    public static void ShowWindow()
    {
        GetWindow<SceneValidatorWindow>(
            "Scene Validator");
    }

    private void OnGUI()
    {
        GUILayout.Label(
            "Scene Validator",
            EditorStyles.boldLabel);

        if (GUILayout.Button(
            "VALIDATE SCENE"))
        {
            ValidateScene();
        }
    }

    private void ValidateScene()
    {
        EnemySpawnPoint[] spawnPoints =
            FindObjectsByType<EnemySpawnPoint>(
                FindObjectsSortMode.None);

        if (spawnPoints.Length == 0)
        {
            Debug.LogError(
                "Scene'de EnemySpawnPoint yok!");
        }
        else
        {
            Debug.Log(
                "SpawnPoint kontrolü başarılı.");
        }
    }
}