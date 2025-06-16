using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
#if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(SceneGeneration))]
    public class CustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            SceneGeneration sceneGeneration = (SceneGeneration)target;

            if (GUILayout.Button("Generation level"))
            {
                sceneGeneration.GenerateLevel();
            }

            if (GUILayout.Button("Clear all prefabs"))
            {
                sceneGeneration.ClearTest();
            }

            if (GUILayout.Button("AddItemOnList"))
            {
                sceneGeneration.AddVectorsOnList();
            } 
        }
    }
#endif
}