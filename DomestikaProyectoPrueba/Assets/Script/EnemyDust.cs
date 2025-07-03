using UnityEngine;

public class EnemyDust : MonoBehaviour
{
    public GameObject prefab;
    public Transform point;
    public float livingTime;

    public void instantiate()
    {
        GameObject DustInstance = Instantiate(prefab, point.position, Quaternion.identity);
        if(livingTime > 0f)
        {
            Destroy(DustInstance, livingTime);
        }
    }
}
