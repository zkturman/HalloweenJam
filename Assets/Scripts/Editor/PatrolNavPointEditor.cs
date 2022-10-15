using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(PatrolNavPoint))]
public class PatrolNavPointEditor : Editor
{
    // This script draws a sphere to show the position of the navpoint in the Scene view, but not in the game.

    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(PatrolNavPoint currentNavPoint, GizmoType gizmoType)
    {
        // Set colour of sphere to be drawn
        Gizmos.color = currentNavPoint.sphereColor;

        // Set size of sphere to be drawn
        float sphereSize = 0.25f;

        // Draw sphere for the PatrolNavPoint
        Gizmos.DrawSphere(currentNavPoint.transform.position, sphereSize);


    }


}
