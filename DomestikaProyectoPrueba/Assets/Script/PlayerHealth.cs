using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public int totalHealth;
    private int health;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        health = totalHealth;
    }

    void AddDamage(int damage)
    {
        health = health - damage;

        if(health < 0)
        {
            health = 0;
        }

        StartCoroutine(VisualFeedBack());
    }

    void AddHealth(int health)
    {
        health += health;

        if(health > totalHealth)
        {
            health = totalHealth;
        }
    }

    private IEnumerator VisualFeedBack()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        _spriteRenderer.color = Color.white;
    }
        
}
