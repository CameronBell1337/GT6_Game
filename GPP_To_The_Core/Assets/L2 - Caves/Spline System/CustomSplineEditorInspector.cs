using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SplineEditor))]
public class CustomSplineEditorInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SplineEditor splineEditorScript = (SplineEditor)target;
        if (GUILayout.Button("Setup/Reset Spline"))
        {
            splineEditorScript.ResetSpline();
        }
        if (GUILayout.Button("Add Waypoint"))
        {
            splineEditorScript.AddWaypoint();
        }
    }
}
