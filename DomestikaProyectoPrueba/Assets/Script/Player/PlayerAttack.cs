using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator _animator;
    private bool _isAttacking;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        _isAttacking = _animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isAttacking && (collision.CompareTag("Enemy") || collision.CompareTag("BigBullet")))
        {

            if (collision.gameObject.activeInHierarchy)
            {
                collision.SendMessageUpwards("AddDamage");
            }
        }
    }
}

