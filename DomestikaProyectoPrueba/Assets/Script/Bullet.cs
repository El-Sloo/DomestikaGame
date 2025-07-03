using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2f;
    public Vector2 direction;
    public float livingTime = 3f;
    private SpriteRenderer _renderer;
    public Color initialColor = Color.white;
    public Color finalColor;
    private float _startingTime;

    void Awake()
    {
        _renderer = this.GetComponent<SpriteRenderer>();
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
        Vector2 movement = direction.normalized * speed * Time.deltaTime;
        this.transform.position = new Vector2(transform.position.x + movement.x, transform.position.y + movement.y);

        float _timeSinceStarted = Time.time - _startingTime;
        float _percentageCompleted = _timeSinceStarted / livingTime;
        _renderer.color = Color.Lerp(initialColor, finalColor, _percentageCompleted);
    }
}
