using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float timeTillSelfDestroy = 3.5f;
    private Rigidbody2D projectileRigidbody2D;
    private float timeTillDestroy;

    void Awake()
    {
        projectileRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timeTillDestroy -= Time.deltaTime;

        if (timeTillDestroy < 0f)
            Destroy(gameObject);
    }

    public void Launch(Vector2 direction, float force)
    {
        projectileRigidbody2D.AddForce(direction * force);

        timeTillDestroy = timeTillSelfDestroy;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<EnemyController>(out EnemyController enemy))
            enemy.Fix();

        Destroy(gameObject);
    }
}
