using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoadSceneBehaviour), true)]
public class LoadSceneBehaviourEditor : Editor
{
    private void OnSceneGUI()
    {
        LoadSceneBehaviour behaviour = (LoadSceneBehaviour)target;
        Vector2 point = behaviour.playerPosition;

        Handles.color = Color.cyan;
        Handles.DrawSolidDisc(point, Vector3.forward, 0.15f);
        point = Handles.PositionHandle(behaviour.playerPosition, Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(behaviour, "Change Player Respawn Point");
            behaviour.playerPosition = point;
        }
    }
}
