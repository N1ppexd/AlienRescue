using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

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

        foreach(Transform target in fov.targets)//piirret‰‰n editorissa viiva kaikkiin juttuihin jotka on siin‰ edess‰ ja t‰‰ on punanen viiva...
        {
            Handles.color = Color.red;
            Vector3 pos = new Vector3(target.position.x, fov.transform.position.y, target.position.z);//viiva pysyy lingassa vihollisen kanssa..,..
            Handles.DrawLine(fov.transform.position, pos);
        }
    }
}
