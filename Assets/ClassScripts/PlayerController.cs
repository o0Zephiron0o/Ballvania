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

    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        //Debug.Log("Shoot");
        Instantiate(_bullet,gameObject.transform.position,Quaternion.identity);
    }
}
