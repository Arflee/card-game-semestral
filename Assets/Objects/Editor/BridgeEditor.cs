using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Bridge), true)]
public class BridgeEditor : Editor
{
    private void OnSceneGUI()
    {
        Bridge bridge = (Bridge)target;
        Vector2[] points = bridge.points;

        if (points.Length == 0)
            return;

        Handles.color = Color.blue;

        for (int i = 0; i < points.Length; i++)
        {
            Handles.DrawSolidDisc(points[i], Vector3.forward, 0.15f);
            points[i] = Handles.PositionHandle(bridge.points[i], Quaternion.identity);
        }

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(bridge, "Change Path Points");
            bridge.points = points;
        }
    }
}
