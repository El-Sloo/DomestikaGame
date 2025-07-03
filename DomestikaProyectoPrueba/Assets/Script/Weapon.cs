using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject shooter;
    private Transform firePoint;
    private void Awake()
    {
        firePoint = transform.Find("FirePoint");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shoot()
    {
        if(bulletPrefab != null && firePoint != null && shooter != null)
        {
            GameObject myBullet = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.identity);
            Bullet bulletScript = myBullet.GetComponent<Bullet>();

            if (shooter.transform.localScale.x > 0)
            {
                bulletScript.direction = Vector2.right;
            }
            else
            {
                bulletScript.direction = Vector2.left; 
            }

        }
    }
}
