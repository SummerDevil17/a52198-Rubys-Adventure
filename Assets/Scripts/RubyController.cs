using UnityEngine;
using UnityEngine.InputSystem;

public class RubyController : MonoBehaviour
{
    [Range(0, 10)][SerializeField] float speed = 3f;
    [SerializeField] int maxHealth = 5;
    [SerializeField] float timeInvincible = 2f;

    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float launchForce = 300f;
    [SerializeField] float timeBetweenLaunches = 1.5f;

    [SerializeField] ParticleSystem hurtVFX;
    [SerializeField] AudioClip launchCogSFX;
    [SerializeField] AudioClip hurtSFX;

    //Stat Control Variables
    private float invincibilityTimer;
    private float timeTillNextLaunch;
    private int currentHealth;

    private bool isInvincible, isDead = false;

    #region Component References
    private PlayerInput rubyPlayerController;
    private Rigidbody2D rubyRigidBody2D;
    private Animator rubyAnimator;
    private AudioSource rubyAudioSource;
    private Vector2 currentMovementInput;
    private Vector2 animationLookDirection = new Vector2(1f, 0f);
    #endregion

    #region Encapsulated Variables/ Properties
    public int MaxHealth { get => maxHealth; }
    public int CurrentHealth { get => currentHealth; }
    #endregion

    void Start()
    {
        rubyRigidBody2D = GetComponent<Rigidbody2D>();
        rubyAnimator = GetComponent<Animator>();
        rubyAudioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (Time.timeScale == 0) rubyAudioSource.Stop();

        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            if (invincibilityTimer <= 0) isInvincible = false;
        }
        timeTillNextLaunch -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        Vector2 position = rubyRigidBody2D.position;
        position += speed * currentMovementInput * Time.deltaTime;

        rubyRigidBody2D.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible) return;

            isInvincible = true;
            invincibilityTimer = timeInvincible;

            hurtVFX.Play();
            rubyAudioSource.PlayOneShot(hurtSFX);
            rubyAnimator.SetTrigger("Hit");
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

        if (currentHealth <= 0)
        {
            isDead = true;
            GameSessionController.instance.LoseGame();
        }
    }

    public void PlaySound(AudioClip clipToPlay)
    {
        rubyAudioSource.PlayOneShot(clipToPlay);
    }

    private void OnMove(InputValue movementInput)
    {
        if (isDead) return;

        currentMovementInput = movementInput.Get<Vector2>();

        if (!Mathf.Approximately(currentMovementInput.x, 0.0f) || !Mathf.Approximately(currentMovementInput.y, 0.0f))
        {
            animationLookDirection.Set(currentMovementInput.x, currentMovementInput.y);
            animationLookDirection.Normalize();
        }

        if (currentMovementInput.magnitude > 0f && rubyAudioSource.isPlaying == false)
            rubyAudioSource.Play();
        else if (currentMovementInput.magnitude <= 0f && !isInvincible && timeBetweenLaunches >= 0)
            rubyAudioSource.Stop();

        rubyAnimator.SetFloat("Look X", animationLookDirection.x);
        rubyAnimator.SetFloat("Look Y", animationLookDirection.y);
        rubyAnimator.SetFloat("Speed", currentMovementInput.magnitude);
    }

    private void OnLaunch()
    {
        if (timeTillNextLaunch >= 0 || isDead) return;

        GameObject projectileObject = Instantiate(projectilePrefab, rubyRigidBody2D.position + Vector2.up * 0.5f, Quaternion.identity);
        projectileObject.GetComponent<Projectile>().Launch(animationLookDirection, launchForce);

        timeTillNextLaunch = timeBetweenLaunches;

        rubyAnimator.SetTrigger("Launch");
        rubyAudioSource.PlayOneShot(launchCogSFX);
    }

    private void OnInteract()
    {
        if (isDead) return;

        RaycastHit2D hit = Physics2D.Raycast(rubyRigidBody2D.position + Vector2.up * 0.2f, animationLookDirection, 1.5f, LayerMask.GetMask("NPC"));
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent<NonPlayerCharacter>(out NonPlayerCharacter character))
                character.DisplayDialog();
        }
    }

}
