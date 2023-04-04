using UnityEngine;
using UnityEngine.InputSystem;

public class RubyController : MonoBehaviour
{
    [Range(0, 10)][SerializeField] float speed = 3f;
    [SerializeField] int maxHealth = 5;

    //Stat Control Variables
    private int currentHealth;

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
        currentHealth = 1;
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
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log("HP: " + currentHealth + "/" + maxHealth);
    }

    private void OnMove(InputValue movementInput)
    {
        currentMovementInput = movementInput.Get<Vector2>();
    }
}
