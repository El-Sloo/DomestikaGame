
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    //Public behaviour variables
    public float speed;
    public float jumpForce;

    //Jump variables
    public LayerMask _layerMask;
    public Transform _floorPoint;
    private bool isGrounded;
    public float circleRatius;
    
    //Move variables
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private bool facingRight = true;
    private Vector2 direction;

    //Attacking variables
    private bool isAttacking = false;

    //IdleLong variables
    public float idleLongTime;
    private float idleLongTimer;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        direction = Vector2.zero;    
    }

    void Update()
    {
        idleLongAnimation();
        characterMovement();
        
        
    }

    private void FixedUpdate()
    {
        if (isAttacking == false)
        {
            float horizontalVelocity = direction.normalized.x * speed;
            _rigidBody.linearVelocity = new Vector2(horizontalVelocity, _rigidBody.linearVelocity.y);
        }
    }

    private void LateUpdate()
    {
        _animator.SetBool("Idle", direction == Vector2.zero && isGrounded && isAttacking == false);
        _animator.SetBool("IsGrounded", isGrounded);
        _animator.SetFloat("VerticalVelocity", _rigidBody.linearVelocity.y);

        
    }

    private void idleLongAnimation()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            idleLongTimer += Time.deltaTime;
            if(idleLongTimer > idleLongTime)
            {
                _animator.SetTrigger("LongIdle");
            }
        }
        else
        {
            idleLongTimer = 0;
        }
    }
    private void characterMovement()
    {
        isGrounded = Physics2D.OverlapCircle(_floorPoint.position, circleRatius, _layerMask);

        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        if (Input.GetButtonDown("Fire1") && isGrounded && isAttacking == false)
        {
            _rigidBody.linearVelocity = Vector2.zero;
            _animator.SetBool("Idle", false);
            _animator.ResetTrigger("Attack");
            _animator.SetTrigger("Attack");
        }

        if (isAttacking == true)
        {
            _rigidBody.linearVelocity = Vector2.zero;
        }
        else
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            direction = new Vector2(horizontalInput, 0);

            if (facingRight == true && horizontalInput < 0)
            {
                Flip();
            }
            else if (facingRight == false && horizontalInput > 0)
            {
                Flip();
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                _rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                _animator.SetTrigger("Jump");
            }
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        this.transform.localScale = new Vector2(this.transform.localScale.x * -1, this.transform.localScale.y);
    }
}
