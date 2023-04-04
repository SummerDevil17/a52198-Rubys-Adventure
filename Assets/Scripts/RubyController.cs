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
    private Animator rubyAnimator;
    private Vector2 currentMovementInput;
    private Vector2 animationLookDirection = new Vector2(1f, 0f);
    #endregion

    #region Encapsulated Variables/ Properties
    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; }
    #endregion

    void Start()
    {
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        rubyAnimator = GetComponent<Animator>();
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
        position += speed * currentMovementInput * Time.deltaTime;

        playerRigidBody2D.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible) return;

            isInvincible = true;
            invincibilityTimer = timeInvincible;

            rubyAnimator.SetTrigger("Hit");
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log("HP: " + currentHealth + "/" + maxHealth);
    }

    private void OnMove(InputValue movementInput)
    {
        currentMovementInput = movementInput.Get<Vector2>();

        if (!Mathf.Approximately(currentMovementInput.x, 0.0f) || !Mathf.Approximately(currentMovementInput.y, 0.0f))
        {
            animationLookDirection.Set(currentMovementInput.x, currentMovementInput.y);
            animationLookDirection.Normalize();
        }

        rubyAnimator.SetFloat("Look X", animationLookDirection.x);
        rubyAnimator.SetFloat("Look Y", animationLookDirection.y);
        rubyAnimator.SetFloat("Speed", currentMovementInput.magnitude);
    }
}
