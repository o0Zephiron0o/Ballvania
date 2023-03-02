using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerControls _controls;

    private InputAction _dashAction;
    private InputAction _swapStateAction;
    private InputAction _aimAction;

    [SerializeField] private PlayerInput _input;

    [SerializeField] PlayerStats _playerStats;

    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _dashSpeed;

    [SerializeField] bool _isSticky;

    private Vector3 lookDirection;

    private void Awake()
    {
        _controls = new PlayerControls();
        _dashAction = _input.actions[_controls.MainCharacter.Dash.name];

        _swapStateAction = _input.actions[_controls.MainCharacter.SwapState.name];

        _aimAction = _input.actions[_controls.MainCharacter.Aim.name];

    }

    private void OnEnable()
    {
        _dashAction.performed += OnDash;
        _swapStateAction.performed += OnSwapState;
    }

    private void OnDisable()
    {
        _dashAction.performed -= OnDash;
        _swapStateAction.performed -= OnSwapState;
    }

    private void Update()
    {

        

        //mouse
        if (_input.currentControlScheme == _controls.MouseKeyboardScheme.name)
        {
            //get mouse input
            Vector3 mousePos = Mouse.current.position.ReadValue();

            //make the z axis align
            mousePos.z = _rb.transform.position.z - Camera.main.transform.position.z;


            Vector3 Worldpos = (Camera.main.ScreenToWorldPoint(mousePos) - _rb.transform.position).normalized;

            //Debug.Log(Worldpos);
            lookDirection = Worldpos;


        }
        else
        {
            //gamepad

            lookDirection = _aimAction.ReadValue<Vector2>().normalized;
        }



        if (lookDirection.magnitude > 0)
        {
            _rb.transform.rotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
        }


        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;

        if (_isSticky == false)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.cyan;

            _rb.constraints = RigidbodyConstraints2D.None;
        }
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        //Debug.Log("Dash");

        _isSticky = false;

        _rb.velocity = new Vector2(lookDirection.x * _dashSpeed, lookDirection.y * _dashSpeed);
    }
    private void OnSwapState(InputAction.CallbackContext context)
    {
        if(_isSticky == false)
        {
            _isSticky = true;
        }
        else if(_isSticky == true)
        {
            _isSticky = false;

           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_isSticky == true)
        {
            _rb.velocity = Vector2.zero;
            _rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        

        //_playerStats.TakeDamage(1);
    }


}
