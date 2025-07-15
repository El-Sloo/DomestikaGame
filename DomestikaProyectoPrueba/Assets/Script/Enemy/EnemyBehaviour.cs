using System.Collections;
using NUnit.Framework.Constraints;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Animations;

public class EnemyBehaviour : MonoBehaviour
{
    //Components variables
    private Animator _animator;
    [SerializeField] private Weapon _weapon;
    private Rigidbody2D _rigidBody;
    public LayerMask _rayCastLayerMask;

    //Variables
    private Vector3 _initialWeaponLocalPosition;
    private Quaternion _initialWeaponLocalRotation;
    private bool isAttacking;
    private bool playerInZone;
    private bool isWaiting;

    //Position variables
    private Vector2 direction;
    private bool facingRight = true;

    //Public behaviour variables
    public float velocity;
    public float distanceToWall;
    public int shootingTime;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _weapon = GetComponentInChildren<Weapon>(true); 

        if (_weapon != null)
        {
            _initialWeaponLocalPosition = _weapon.transform.position - transform.position;
            _initialWeaponLocalRotation = _weapon.transform.rotation;

            _weapon.transform.SetParent(null);
            _weapon.transform.SetParent(transform);
        }

        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer, true);

    }
    void Update()
    {
        if (!isWaiting && !isAttacking)
        {
            direction = new Vector2(this.transform.localScale.x, 0);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distanceToWall, _rayCastLayerMask);
            if (hit == true)
            {
                Flip();
            }
        }
    }



    void FixedUpdate()
    {
        if (!isWaiting && !isAttacking)
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
        if (collider.CompareTag("Player") && gameObject.activeInHierarchy)
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
        yield return new WaitForSeconds(shootingTime);

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
        if (_weapon != null && gameObject.activeInHierarchy)
        {
            _weapon.shoot();
        }
    }
    private IEnumerator InitialWait()
    {
        yield return new WaitForSeconds(3f);
        isWaiting = false;
    }



    private void OnEnable()
    {
        isAttacking = false;
        isWaiting = true;
        _rigidBody.linearVelocity = Vector2.zero;

        if (_weapon != null)
        {
            _weapon.transform.position = transform.position + _initialWeaponLocalPosition;
            _weapon.transform.rotation = _initialWeaponLocalRotation;
            _weapon.transform.localScale = Vector3.one;
        }

        StartCoroutine(InitialWait());
    }
    private void OnDisable()
    {
        _rigidBody.linearVelocity = Vector2.zero;
        StopAllCoroutines(); 

        if (_animator != null)
        {
            _animator.Rebind();
            _animator.Update(0f);
        }

        if (_weapon != null)
        {
            _weapon.transform.localPosition = _initialWeaponLocalPosition;
            _weapon.transform.localRotation = Quaternion.identity;
        }

    }
}