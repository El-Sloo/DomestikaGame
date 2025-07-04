using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject shooter;

    public GameObject bulletExplosion;
    public LineRenderer lineRenderer;

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
    public IEnumerator shootRayCast2D()
    {
        if (bulletExplosion != null && lineRenderer != null && firePoint != null && shooter != null)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);
            if (hitInfo)
            {
                Instantiate(bulletExplosion, hitInfo.point, Quaternion.identity);
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, hitInfo.point);
            }
            else
            {
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);
            }
            lineRenderer.enabled = true;
            yield return new WaitForSeconds(0.08f);
            lineRenderer.enabled = false;
        }

    }
}
