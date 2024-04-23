
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerScript_Refactor : MonoBehaviour
{
    [Header("Components of the character")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _camera;
    [SerializeField] private PlayerInput _plInput;
    [SerializeField] private Animator _anim;

    [Header("Movement values")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private float _airMultiplier;
    private float _horizontalImput;
    private float _verticalImput;
    private Vector3 _direction;

    //Sprint Values
    [SerializeField] private float _sprintValue;
    private float _sprintMultiplier;
    private bool _sprinting;
    
    //Jump Values
    private bool _jump;
    private bool _readyToJump = true;

    //Dance freeze
    [SerializeField] private AnimationClip _danceAnimation;
    private bool _dancing;



    [Header("Ground Check")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private float _groundDrag;
    private bool _grounded;



    private void Update()
    {
        PlayerImput();
        SpeedControl();

        //Drag control
        if (_grounded ) _rb.drag = _groundDrag;
        else _rb.drag = 0;
    }

    private void FixedUpdate()
    {
       
        if (!_dancing) MovePlayer();
    }

    private void OnTriggerEnter(Collider collider)
    {

        //Ground check
        _grounded = collider != null && (((1<< collider.gameObject.layer) & _groundLayer) != 0);
    }

    private void OnTriggerExit(Collider other)
    {
        _grounded = false;
    }


    private void PlayerImput()
    {
        //Movement input
        Vector2 movementInput = _plInput.actions["Move"].ReadValue<Vector2>();
        _horizontalImput = movementInput.x;
        _verticalImput = movementInput.y;

        if (movementInput.magnitude != 0 && !_sprinting)
        {
            _anim.SetBool("run", false);
            _anim.SetBool("walk", true);
        }
        else if (movementInput.magnitude != 0 && _sprinting)
        {
            _anim.SetBool("walk", false);
            _anim.SetBool("run", true);
        }
        else 
        {
            _anim.SetBool("walk", false);
            _anim.SetBool("run", false);
        }
        


            //jump input
            _jump = _plInput.actions["Jump"].IsPressed();

        if (_jump && _readyToJump && _grounded && !_dancing)
        {
            _readyToJump = false;

            Jump();
            _anim.SetBool("jump", true);

            Invoke(nameof(ResetJump), _jumpCooldown);
        }

        //Sprint imput
        _sprinting = _plInput.actions["Sprint"].IsPressed();

        if (_sprinting && _grounded) _sprintMultiplier = _sprintValue;
        else _sprintMultiplier = 1f;

        //Dance imput
        if (_plInput.actions["Dance"].IsPressed())
        {
            _anim.SetBool("dance", true);
            _dancing = true;
        }
        else
        {
            _anim.SetBool("dance", false);
            _dancing = false;
        }

        //Camera input
        transform.rotation = Quaternion.Euler(0f, _camera.eulerAngles.y, 0f);
    }

    private void MovePlayer()
    {
        //calculamos la direccion de nuestro movimiento
        _direction = transform.forward * _verticalImput + transform.right * _horizontalImput;

        //On ground
        if (_grounded) _rb.AddForce(_direction.normalized * _speed * _sprintMultiplier * 10f, ForceMode.Force);
        //On air
        else _rb.AddForce(_direction.normalized * _speed * 10f * _airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);

        if (flatVel.magnitude > _speed)
        {
            Vector3 limitedVel = flatVel.normalized * _speed;
            _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //reset y velocity
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);

        _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        _readyToJump = true;
        _anim.SetBool("jump", false);
    }

}