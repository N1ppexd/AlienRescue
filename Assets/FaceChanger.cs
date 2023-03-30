using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceChanger : MonoBehaviour
{
    [SerializeField] private MeshRenderer kupoli;
    [SerializeField] private Material normalFace, loveFace, angryFace;

    public static FaceChanger instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }
    public void SeenByAPerson()
    {
        StartCoroutine(changeFace(FaceMode.angry));
    }

    enum FaceMode
    {
        angry,
        love
    }
    public void OnCaptureAlien()
    {
        StartCoroutine(changeFace(FaceMode.love));
    }

    IEnumerator changeFace(FaceMode mode)
    {
        if (mode == FaceMode.love)
            kupoli.material = loveFace;
        else if (mode == FaceMode.angry)
            kupoli.material = angryFace;
        yield return new WaitForSeconds(1.5f);
        kupoli.material = normalFace;
    }
}
