using UnityEngine;
using UnityEngine.InputSystem;

public class RubyController : MonoBehaviour
{
    [Range(0, 10)][SerializeField] float speed = 3f;

    private PlayerInput rubyPlayerController;
    private Vector2 currentMovementInput;

    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
    }

    void Update()
    {
        /*float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 position = transform.position;
        position.x += 0.1f * horizontal;
        position.y += 0.1f * vertical;
        transform.position = position; */

        Vector2 position = transform.position;
        position.x += speed * currentMovementInput.x * Time.deltaTime;
        position.y += speed * currentMovementInput.y * Time.deltaTime;
        transform.position = position;
    }

    private void OnMove(InputValue movementInput)
    {
        currentMovementInput = movementInput.Get<Vector2>();
    }
}
