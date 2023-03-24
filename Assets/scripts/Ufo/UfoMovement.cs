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

        inputMaster.Player.Move.performed += ctx => inputAxis = ctx.ReadValue<Vector2>();
        inputMaster.Player.Move.canceled += ctx => inputAxis = ctx.ReadValue<Vector2>();
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

        Debug.Log(axis + " on axis");
        //koska y on ylˆ‰sp‰in, tehd‰‰n n‰in. Peliss‰ ufo ei pyˆ‰ti ymp‰riins‰ niin t‰m‰ on ok
        movementAxis = new Vector3(axis.x, 0, axis.y);

        rb.velocity = movementAxis.normalized * speed; //lis‰t‰‰n noppeus...
        //rb.AddForce(movementAxis.normalized * speed * 100 * Time.deltaTime); //lis‰t‰‰n noppeus...

        //transform.GetChild(0).LookAt(transform.position + rb.velocity, Vector3.left);

    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        Movement(inputAxis);
        
    }
}
