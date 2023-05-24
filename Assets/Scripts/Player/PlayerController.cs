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
    [SerializeField] int _maxDash;
    public int MaxDash => _maxDash;
    private int _remainingDash;
    [SerializeField] GameObject dashParticles;

    [SerializeField] bool _isSticky;

    private Vector3 lookDirection;

    [SerializeField] bool _canStick = false;
    public bool CanStick => _canStick;

    private void Awake()
    {
        _controls = new PlayerControls();
        _dashAction = _input.actions[_controls.MainCharacter.Dash.name];

        _swapStateAction = _input.actions[_controls.MainCharacter.SwapState.name];

        _aimAction = _input.actions[_controls.MainCharacter.Aim.name];

    }

    private void Start()
    {
        if (GameManager.Instance.HasSavedProgression)
        {
            _canStick = GameManager.Instance.CanStick;
            _maxDash = GameManager.Instance.MaxDash;
        }
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

        
        //Get Look Direction
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


        //Rotate to face look Direction
        if (lookDirection.magnitude > 0)
        {
            _rb.transform.rotation = Quaternion.LookRotation(Vector3.forward, lookDirection);
        }

        //Change to green under other circustances

        gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;

        if (_isSticky == false)
        {
            //go blue if not stcky instead
            gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.cyan;

            //transform.parent = null;

            _rb.constraints = RigidbodyConstraints2D.None;
        }
        
        
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        //Debug.Log("Dash");
        if(_remainingDash > 0)
        {
            _rb.constraints = RigidbodyConstraints2D.None;

            _rb.velocity = new Vector2(lookDirection.x * _dashSpeed, lookDirection.y * _dashSpeed);
            Instantiate(dashParticles, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
            _remainingDash -= 1;
        }
        
    }
    private void OnSwapState(InputAction.CallbackContext context)
    {


        if(_isSticky == false && _canStick == true)
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
        
        if(_isSticky == true && collision.collider.CompareTag("Sticky"))
        {
            _rb.velocity = Vector2.zero;
            transform.parent = collision.transform;

            _rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        

        //_playerStats.TakeDamage(1);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        _remainingDash = _maxDash;


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _remainingDash = _maxDash - 1;

        if(collision.CompareTag("Sticky"))
        {
            gameObject.transform.SetParent(null);
        }
        
    }

    public void UnlockPowerUp(int index)
    {
        if(index == 1)
        {
            _canStick = true;
            
        }

        if(index == 2)
        {
            _maxDash = 2;
        }

        GameManager.Instance.RecordPlayerPowerUps(this);
    }


}
