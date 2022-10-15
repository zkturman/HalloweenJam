using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(CreatureController))]
public class CreatureControllerEditor : Editor
{
    // This script draws a shape above the creature's head to indicate the creature's current state

    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(CreatureController currentCreature, GizmoType gizmoType)
    {
        // Set gizmo position and size
        string gizmoShape = "Sphere";     // Can be 'Sphere' or 'Cube'
        float gizmoSize = 0.15f;
        float gizmoPosHeight = 2f;
        Vector3 gizmoPosition = currentCreature.transform.position + (Vector3.up * gizmoPosHeight);
        
        // Set color depending on status
        if (currentCreature.currentState == CreatureState.Idle || 
            currentCreature.currentState == CreatureState.Patrol ||
            currentCreature.currentState == CreatureState.Returning)
        {
            Gizmos.color = Color.green;
            
        }

        if (currentCreature.currentState == CreatureState.Chase)
        {
            Gizmos.color = Color.red;
        }

        if (currentCreature.currentState == CreatureState.Attack)
        {
            gizmoShape = "Cube";
            gizmoSize = 1f;
            Gizmos.color = Color.red;
        }

        if (currentCreature.currentState == CreatureState.Surveillance)
        {
            gizmoShape = "Cube";
            Gizmos.color = Color.blue;
        }

        if (currentCreature.currentState == CreatureState.Returning)
        {
            Gizmos.color = Color.yellow;
        }

        // Draw gizmo in selected color
        if (gizmoShape == "Sphere")
        {
            Gizmos.DrawSphere(gizmoPosition, gizmoSize);
        } else if (gizmoShape == "Cube")
        {
            Gizmos.DrawCube(gizmoPosition, (Vector3.one * gizmoSize));
        }
        

    }
}
