using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] bool isMovingVertical = false;
    [SerializeField] float timeBetweenDirections = 2.5f;
    [SerializeField] int amountToDamage = 2;

    private float timeTillNewDirection;
    private int currentDirection = 1;
    private Rigidbody2D enemyRigidbody2D;


    void Start()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        timeTillNewDirection = timeBetweenDirections;
    }

    void Update()
    {
        timeTillNewDirection -= Time.deltaTime;

        if (timeTillNewDirection < 0)
        {
            currentDirection = -currentDirection;
            timeTillNewDirection = timeBetweenDirections;
        }
    }

    void FixedUpdate()
    {
        Vector2 pos = enemyRigidbody2D.position;
        if (isMovingVertical)
            pos.y += moveSpeed * Time.deltaTime * currentDirection;
        else
            pos.x += moveSpeed * Time.deltaTime * currentDirection;

        enemyRigidbody2D.MovePosition(pos);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Verifies if other has the controller and gets it, if returning true
        if (other.gameObject.TryGetComponent<RubyController>(out RubyController rubyController))
        {
            rubyController.ChangeHealth(-amountToDamage);
        }
    }
}
