using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UfoMovement : MonoBehaviour
{
    public InputMaster inputMaster;


    [SerializeField] private Rigidbody rb;  //ufon rigidbody...


    private Vector2 inputAxis;
    private Vector3 movementAxis;
    [SerializeField] private float speed;//ufon nopeus...

    private void Awake()
    {
        inputMaster = new InputMaster();
        inputMaster.Enable();

        inputMaster.Player.Move.performed += ctx => inputAxis = ctx.ReadValue<Vector2>();
        inputMaster.Player.Move.canceled += ctx => inputAxis = ctx.ReadValue<Vector2>();
    }

    //liikkumishomma alhaalla
    #region movement
    private void Movement(Vector2 axis)
    {

        Debug.Log(axis + " on axis");
        //koska y on ylöäspäin, tehdään näin. Pelissä ufo ei pyöäti ympäriinsä niin tämä on ok
        movementAxis = new Vector3(axis.x, 0, axis.y);

        rb.velocity = movementAxis.normalized * speed; //lisätään noppeus...

    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        Movement(inputAxis);
        
    }
}
