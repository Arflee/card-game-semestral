using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BasicBehaviour), true)]
public class BasicBehaviourEditor : Editor
{
    private void OnSceneGUI()
    {
        BasicBehaviour basicBehaviour = (BasicBehaviour)target;
        Vector2[] points = basicBehaviour.points;

        if (points.Length == 0)
            return;

        Handles.color = Color.red;
        GUIStyle style = new GUIStyle { fontSize = 20, normal = new GUIStyleState { textColor = Color.white } };

        for (int i = 0; i < points.Length; i++)
        {
            Handles.DrawSolidDisc(points[i], Vector3.forward, 0.15f);
            Vector2 labelPos = points[i] + Vector2.left * .2f + Vector2.up * .3f;
            Handles.Label(labelPos, i.ToString(), style);
        }

        Handles.color = Color.blue;
        for (int i = 0; i < points.Length - 1; i++)
            Handles.DrawLine(points[i], points[i + 1], 3);
        if (basicBehaviour.mode == BasicBehaviour.Mode.Loop)
            Handles.DrawLine(points[points.Length - 1], points[0], 3);
        
        for (int i = 0; i < points.Length; i++)
            points[i] = Handles.PositionHandle(basicBehaviour.points[i], Quaternion.identity);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(basicBehaviour, "Change Path Points");
            basicBehaviour.points = points;
        }
    }
}
