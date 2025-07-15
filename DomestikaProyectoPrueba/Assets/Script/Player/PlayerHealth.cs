using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public Transform RespawnPoint;
    public RectTransform totalHealthWidthUI;
    public float hearthUISize = 13;
    public int totalHealth;
    private float health;

    public GameObject gameOverMenu;
    public GameObject horde;

    private SpriteRenderer _spriteRenderer;
    private PlayerController _playerController;
    private Animator _animator;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        health = totalHealth;
    }

    public void AddDamage(int damage)
    {
        health = health - damage;

        if (health <= 0f)
        {
            health = 0;
            totalHealthWidthUI.sizeDelta = new Vector2(hearthUISize * health, hearthUISize);
            gameObject.SetActive(false);
            return;
        }

        totalHealthWidthUI.sizeDelta = new Vector2(hearthUISize * health, hearthUISize);
        StartCoroutine(VisualFeedBack());
        

    }

    public void AddHealth(int healing)
    {
        health = health + healing;

        if(health > totalHealth)
        {
            health = totalHealth;
        }
        totalHealthWidthUI.sizeDelta = new Vector2(hearthUISize * health, hearthUISize);
    }

    private IEnumerator VisualFeedBack()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        _spriteRenderer.color = Color.white;
    }
    private void OnEnable()
    {
        gameObject.transform.position = RespawnPoint.position;
        health = totalHealth;

        _animator.enabled = true;
        _playerController.enabled = true;
    }

    private void OnDisable()
    {
        gameOverMenu.SetActive(true);
        horde.SetActive(false);
        _animator.enabled = false;
        _playerController.enabled = false;
    }
}
