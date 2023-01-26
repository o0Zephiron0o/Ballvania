using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls _controls;

    private InputAction _shootAction;
    private InputAction _moveAction;
    private InputAction _aimAction;

    [SerializeField] private PlayerInput _input;


    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _moveSpeed;

    private float _lookAngle;

    [SerializeField] GameObject _bullet;

    //public void OnShoot()
    //{
    //    Debug.Log("Shoot");
    //}

    private void Awake()
    {
        _controls = new PlayerControls();
        _shootAction = _input.actions[_controls.ClassNotes.Shoot.name];
        _moveAction = _input.actions[_controls.ClassNotes.Moves.name];
        _aimAction = _input.actions[_controls.ClassNotes.Aim.name];

    }

    private void OnEnable()
    {
        _shootAction.performed += OnShoot;
    }

    private void OnDisable()
    {
        _shootAction.performed -= OnShoot;
    }

    private void Update()
    {
        Vector2 moveInput;

        //mouse movement
        if (_input.currentControlScheme == _controls.MouseKeyboardScheme.name)
        {
            moveInput = _moveAction.ReadValue<Vector2>();
        }
        else
        {
            //gamepad movement

            //get the gamepad inputs
            Vector2 freeMovement = _moveAction.ReadValue<Vector2>();

            //check if there is any input
            if (freeMovement.magnitude > 0)
            {
                //get the angle from the input point
                float angle = Mathf.Atan2(freeMovement.y, freeMovement.x);

                //calculate vector direction
                float direction = ((angle / Mathf.PI) + 1) * 0.5f; // Convert to [0..1] range from [-pi..pi]

                //snap to the closest direction in 8 angles
                float snappedDirection = Mathf.Round(direction * 8) / 8; 
                snappedDirection = ((snappedDirection * 2) - 1) * Mathf.PI; // Convert back to [-pi..pi] range

                Vector2 snappedVector = new Vector2(Mathf.Cos(snappedDirection), Mathf.Sin(snappedDirection));

                moveInput = snappedVector;
            }
            else
            {
                moveInput = new Vector2(0,0);
            }

        }

            Debug.Log(moveInput);

            _rb.velocity = moveInput * _moveSpeed;
        //_rb.AddForce(moveInput * _moveSpeed);

        RotateShip();

    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        //Debug.Log("Shoot");
        Instantiate(_bullet,gameObject.transform.position,gameObject.transform.rotation);
    }

    private void RotateShip()
    {
        Vector3 lookDirection;

        //mouse
        if(_input.currentControlScheme == _controls.MouseKeyboardScheme.name)
        {
            //get mouse input
            Vector3 mousePos = Mouse.current.position.ReadValue();

            //make the z axis align
            mousePos.z = _rb.transform.position.z - Camera.main.transform.position.z;


            Vector3 Worldpos = (Camera.main.ScreenToWorldPoint(mousePos)- _rb.transform.position).normalized;

            Debug.Log(Worldpos);
            lookDirection = Worldpos;
            

        }
        else
        {
            //gamepad

            lookDirection = _aimAction.ReadValue<Vector2>().normalized;
        }



        if(lookDirection.magnitude > 0)
        {
            _rb.transform.rotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
        }
        

    }

}
