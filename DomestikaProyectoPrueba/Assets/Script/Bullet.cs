using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction;
    public float livingTime = 3f;
    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidBody;
    public Color initialColor = Color.white;
    public Color finalColor;
    private float _startingTime;
    public int damage;

    void Awake()
    {
        _renderer = this.GetComponent<SpriteRenderer>();
        _rigidBody = this.GetComponent<Rigidbody2D>();

        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer, true);
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
        _rigidBody.linearVelocity = new Vector2(speed * direction.x, _rigidBody.linearVelocityY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.SendMessageUpwards("AddDamage", damage);
            Destroy(this.gameObject);
        }
    }
}
