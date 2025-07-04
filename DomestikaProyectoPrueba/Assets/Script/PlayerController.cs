using UnityEditor.Experimental.GraphView;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private bool facingRight = true;

    private Vector2 direction;
    public float speed;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        direction = new Vector2(horizontalInput, 0); 

        if(facingRight == true && horizontalInput < 0)
        {
            Flip();
        }
        else if(facingRight == false && horizontalInput > 0){
            Flip();
        }
    }

    private void FixedUpdate()
    {
        float horizontalVelocity = direction.normalized.x * speed;
        _rigidBody.linearVelocity = new Vector2(horizontalVelocity, _rigidBody.linearVelocity.y);
    }

    private void LateUpdate()
    {
        _animator.SetBool("Idle", direction == Vector2.zero);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        this.transform.localScale = new Vector2(this.transform.localScale.x * -1, this.transform.localScale.y);
    }
}
