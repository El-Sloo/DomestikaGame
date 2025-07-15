using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject shooter;

    public GameObject bulletExplosion;
    public LineRenderer lineRenderer;

    private Transform firePoint;

    private Vector3 initialLocalPosition;
    private Quaternion initialLocalRotation;
    private void Awake()
    {
        firePoint = transform.Find("FirePoint");
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
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
    private void OnEnable()
    {
        transform.localPosition = initialLocalPosition;
        transform.localRotation = initialLocalRotation;
    }
}
