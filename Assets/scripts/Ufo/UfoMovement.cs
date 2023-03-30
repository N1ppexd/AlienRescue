using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UfoMovement : MonoBehaviour
{
    public InputMaster inputMaster;


    [SerializeField] private Rigidbody rb;  //ufon rigidbody...

    [SerializeField] private float tiltAmount, smoothSpeed = 6f;//angle jolla kallistutaan...

    private Vector2 inputAxis;
    private Vector3 movementAxis;
    [SerializeField] private float speed;//ufon nopeus...

    bool fastMove; //t‰m‰ on true, kun shifti‰ painetaan pohjassa...

    private void Awake()
    {
        inputMaster = new InputMaster();

        inputMaster.Player.Move.performed += ctx => inputAxis = ctx.ReadValue<Vector2>();
        inputMaster.Player.Move.canceled += ctx => inputAxis = ctx.ReadValue<Vector2>();

        inputMaster.Player.fastMove.performed += _ => fastMove = true;
        inputMaster.Player.fastMove.canceled += _ => fastMove = false;
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }

    //liikkumishomma alhaalla
    #region movement
    private void Movement(Vector2 axis)
    {

        //Debug.Log(axis + " on axis");
        //koska y on ylˆ‰sp‰in, tehd‰‰n n‰in. Peliss‰ ufo ei pyˆ‰ti ymp‰riins‰ niin t‰m‰ on ok
        movementAxis = new Vector3(axis.x, 0, axis.y);

        if (!fastMove)
            rb.velocity = movementAxis.normalized * speed; //lis‰t‰‰n noppeus...
        if(fastMove)
            rb.AddForce(movementAxis.normalized * speed * 100 * Time.deltaTime); //lis‰t‰‰n noppeus...

        Quaternion rotation = Quaternion.Euler(Vector3.Cross(transform.up, rb.velocity.normalized) * tiltAmount);
        transform.GetChild(0).rotation = Quaternion.Slerp(transform.rotation, rotation, smoothSpeed);
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        Movement(inputAxis);
        
    }
}
