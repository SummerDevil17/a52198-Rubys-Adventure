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
    private Animator enemyAnimator;
    private bool isBroken = true;


    void Start()
    {
        enemyRigidbody2D = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        timeTillNewDirection = timeBetweenDirections;
    }

    void Update()
    {
        //if is not broken then it has been repaired
        if (!isBroken) return;

        timeTillNewDirection -= Time.deltaTime;

        if (timeTillNewDirection < 0)
        {
            currentDirection = -currentDirection;
            timeTillNewDirection = timeBetweenDirections;
        }
    }

    void FixedUpdate()
    {
        if (!isBroken) return;

        Vector2 pos = enemyRigidbody2D.position;
        if (isMovingVertical)
        {
            enemyAnimator.SetFloat("Move X", 0);
            enemyAnimator.SetFloat("Move Y", currentDirection);

            pos.y += moveSpeed * Time.deltaTime * currentDirection;
        }
        else
        {
            enemyAnimator.SetFloat("Move X", currentDirection);
            enemyAnimator.SetFloat("Move Y", 0);

            pos.x += moveSpeed * Time.deltaTime * currentDirection;
        }

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

    public void Fix()
    {
        isBroken = false;
        enemyRigidbody2D.simulated = false;

        enemyAnimator.SetTrigger("Fixed");
    }
}
