using UnityEngine;

public class RayCast2D : MonoBehaviour
{
    private Animator animator;
    private Weapon weapon;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Shoot");
        }    
    }

    private void CanShoot()
    {
        if (weapon != null)
        {
            StartCoroutine(weapon.shootRayCast2D());
        }
    }
}
