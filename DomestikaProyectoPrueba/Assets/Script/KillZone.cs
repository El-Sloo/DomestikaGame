using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((collision.CompareTag("Enemy") && LayerMask.LayerToName(collision.gameObject.layer) == "Enemy") || collision.CompareTag("Player")) )
        {
            collision.gameObject.SetActive(false);
        }
    }
}
 