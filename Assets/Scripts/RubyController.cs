using UnityEngine;
using UnityEngine.InputSystem;

public class RubyController : MonoBehaviour
{
    [Range(0, 10)][SerializeField] float speed = 3f;
    [SerializeField] float timeInvincible = 2f;

    [SerializeField] int maxHealth = 5;

    //Stat Control Variables
    private float invincibilityTimer;
    private int currentHealth;

    private bool isInvincible;

    #region Component References
    private PlayerInput rubyPlayerController;
    private Rigidbody2D playerRigidBody2D;
    private Vector2 currentMovementInput;
    #endregion

    #region Encapsulated Variables/ Properties
    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; }
    #endregion

    void Start()
    {
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            if (invincibilityTimer <= 0) isInvincible = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = playerRigidBody2D.position;
        position.x += speed * currentMovementInput.x * Time.deltaTime;
        position.y += speed * currentMovementInput.y * Time.deltaTime;

        playerRigidBody2D.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible) return;

            isInvincible = true;
            invincibilityTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log("HP: " + currentHealth + "/" + maxHealth);
    }

    private void OnMove(InputValue movementInput)
    {
        currentMovementInput = movementInput.Get<Vector2>();
    }
}
