using UnityEngine;

public class EnemyPooling : MonoBehaviour
{
    public GameObject prefab;
    public int numberOfEnemies;
    public float respawningTime;
    void Start()
    {
        InitializePool();
        InvokeRepeating("GetEnemyFromPool", 1, respawningTime);
    }

    private void InitializePool()
    {
        for(int i = 0; i < numberOfEnemies; i++)
        {
            AddEnemyToPool();
        }
    }

    private void AddEnemyToPool()
    {
        GameObject enemy = Instantiate(prefab, transform.position, Quaternion.identity, this.transform);
        enemy.SetActive(false);
    }

    private GameObject GetEnemyFromPool()
    {
        GameObject enemy = null;


        for(int i = 0;i < numberOfEnemies;i++)
        {
            if (!transform.GetChild(i).gameObject.activeSelf)
            {
                enemy = transform.GetChild(i).gameObject;
                break;
            }
        }
        enemy.transform.position = transform.position;
        enemy.SetActive(true);
        return enemy;
    }
}
