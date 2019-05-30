using UnityEditor;
using UnityEngine;

namespace KnifeHitTest
{
    [CustomEditor(typeof(GameSetup))]
    public class PersistenceEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GameSetup gameSetup = (GameSetup)target;
            if (GUILayout.Button("Clear All Data"))
            {
                gameSetup.ClearAllData();
            }
        }
    } 
}
