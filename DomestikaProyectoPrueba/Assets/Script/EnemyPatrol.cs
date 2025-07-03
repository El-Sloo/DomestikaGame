using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Animations;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 1f;
    public float minX;
    public float maxX;
    public float waitingTime = 2f;
    private Animator animator;
    private GameObject target;
    private Weapon weapon;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();
    }
    void Start()
    {
        updateTarget();
        StartCoroutine(PatrolToTarget()); 
    }

 
    void Update()
    {
        
    }

    private void updateTarget()
    {
        if (target == null)
        {
            target = new GameObject("target");
            target.transform.position = new Vector2(maxX, this.transform.position.y);
            return;
        }
        if (target.transform.position.x == minX)
        {
            target.transform.position = new Vector2(maxX, this.transform.position.y);
            this.transform.localScale = new Vector3(1, 1, 1);
        } 
        else if (target.transform.position.x == maxX)
        {
            target.transform.position = new Vector2(minX, this.transform.position.y);
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private IEnumerator PatrolToTarget()
    {
        animator.SetBool("Idle", false);
        while (Vector2.Distance(transform.position, target.transform.position) > 0.05f)
        {
            Vector2 direction = target.transform.position - transform.position;
            this.transform.Translate(direction.normalized * speed * Time.deltaTime);
            yield return null;
        }
        
        this.transform.position = new Vector2(target.transform.position.x, this.transform.position.y);
        updateTarget();

        animator.SetBool("Idle", true);
        animator.SetTrigger("Shoot");

        yield return new WaitForSeconds(waitingTime);
        StartCoroutine("PatrolToTarget");
    }
    public void CanShoot()
    {
        if (weapon != null)
        {
            weapon.shoot();
        }
    }
}
