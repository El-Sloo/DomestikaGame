using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction;
    public float livingTime = 3f;
    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidBody;
    private Collider2D _collider;
    public Color initialColor = Color.white;
    public Color finalColor;
    private float _startingTime;
    public int damage;
    private bool isReturned;
    private bool hasCollisioned;

    void Awake()
    {
        int hitLayer = LayerMask.NameToLayer("Hit");
        int hurtLayer = LayerMask.NameToLayer("Hurt");
        int enemyLayer = LayerMask.NameToLayer("Enemy");

        _renderer = this.GetComponent<SpriteRenderer>();
        _rigidBody = this.GetComponent<Rigidbody2D>();
        _collider = this.GetComponent<Collider2D>();


    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startingTime = Time.time;
        Destroy(this.gameObject, livingTime);
    }

    // Update is called once per frame
    void Update()
    {
        float _timeSinceStarted = Time.time - _startingTime;
        float _percentageCompleted = _timeSinceStarted / livingTime;
        _renderer.color = Color.Lerp(initialColor, finalColor, _percentageCompleted);
    }

    private void FixedUpdate()
    {
        _rigidBody.linearVelocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasCollisioned)
        {
            if (isReturned && collision.CompareTag("Enemy"))
            {

                collision.SendMessageUpwards("AddDamage", damage);
                Destroy(this.gameObject);
                hasCollisioned = true;

            }
            if (collision.CompareTag("Player"))
            {
                collision.SendMessageUpwards("AddDamage", damage);
                Destroy(this.gameObject);
                hasCollisioned = true;
            }
        }

        
        
    }
    private void AddDamage()
    {
        direction = -1 * direction;
        isReturned = true;
    }
}
