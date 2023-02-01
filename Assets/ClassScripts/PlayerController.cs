using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls _controls;

    private InputAction _shootAction;
    private InputAction _moveAction;

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
        Vector2 moveInput = _moveAction.ReadValue<Vector2>();

        //Debug.Log(moveInput);

        _rb.velocity = moveInput * _moveSpeed;
        //_rb.AddForce(moveInput * _moveSpeed);

        RotateShip();

    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        //Debug.Log("Shoot");
        Instantiate(_bullet,gameObject.transform.position,Quaternion.identity);
    }

    private void RotateShip()
    {
        Vector2 lookDirection;

        //mouse
        if(_input.currentControlScheme == _controls.MouseKeyboardScheme.name)
        {
            //get mouse input
            Vector3 mousePos = Mouse.current.position.ReadValue();

            //make the z axis align
            mousePos.z = _rb.transform.position.z - Camera.main.transform.position.z;


            Vector3 Worldpos = (Camera.main.ScreenToWorldPoint(mousePos)- _rb.transform.position).normalized;

            Debug.Log(Worldpos);


            _rb.transform.rotation = Quaternion.LookRotation(Vector3.forward,Worldpos);



            //_rb.transform.rotation = Quaternion.LookRotation( lookDirection);
        }


        //gamepad
    }
}
