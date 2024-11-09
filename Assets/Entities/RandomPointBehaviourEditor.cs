using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomPointBehaviour), true)]
public class RandomPointBehaviourEditor : Editor
{
    private void OnSceneGUI()
    {
        RandomPointBehaviour behaviour = (RandomPointBehaviour)target;
        Vector2[] points = behaviour.points;

        if (points.Length == 0)
            return;

        Handles.color = behaviour.gizmosColor;

        for (int i = 0; i < points.Length; i++)
        {
            Handles.DrawSolidDisc(points[i], Vector3.forward, 0.15f);
            points[i] = Handles.PositionHandle(behaviour.points[i], Quaternion.identity);
        }

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(behaviour, "Change Path Points");
            behaviour.points = points;
        }
    }
}
