using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using log4net.Util;

[CustomEditor(typeof(PlayerDetect))]
public class fovEditor : Editor
{
    private void OnSceneGUI()
    {
        PlayerDetect fov = (PlayerDetect)target;
        Handles.color = Color.white;

        Handles.DrawWireArc(fov.transform.position, fov.transform.up, Vector3.forward, 360, fov.seeRadius);


        Vector3 viewAngleA = fov.dirFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.dirFromAngle(fov.viewAngle / 2, false);


        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.seeRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.seeRadius);
    }
}
