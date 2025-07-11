using System.Collections;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.Animations;

public class EnemyBehaviour : MonoBehaviour
{
    //Components variables
    private Animator _animator;
    private Weapon _weapon;
    private Rigidbody2D _rigidBody;
    public LayerMask _rayCastLayerMask;

    //Variables
    private bool isAttacking;
    private bool playerInZone;

    //Position variables
    private Vector2 direction;
    private bool facingRight = true;

    //Public behaviour variables
    public float velocity;
    public float distanceToWall;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _weapon = GetComponentInChildren<Weapon>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }   

    void Update()
    {
        direction = new Vector2(this.transform.localScale.x, 0);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distanceToWall, _rayCastLayerMask);
        if(hit == true)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        if (isAttacking == false)
        {
            _rigidBody.linearVelocity = new Vector2(velocity * transform.localScale.x, _rigidBody.linearVelocity.y);
        }
    }

    private void LateUpdate()
    {
        _animator.SetBool("Idle", _rigidBody.linearVelocity == Vector2.zero);    
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInZone = true;
            StartCoroutine(Shooting());   
        } 
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }

    private IEnumerator Shooting()
    {
        isAttacking = true;

        _rigidBody.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(1);
        _animator.SetTrigger("Shoot");
        yield return new WaitForSeconds(1);

        isAttacking = false;

        if (playerInZone == true)
        {
            StartCoroutine(Shooting());
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        this.transform.localScale = new Vector2(transform.localScale.x * (-1), 1);
    }

    public void CanShoot()
    {
        if (_weapon != null)
        {
            _weapon.shoot();
        }
    }
}
