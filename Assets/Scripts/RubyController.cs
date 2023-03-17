using UnityEngine;
using UnityEngine.InputSystem;

public class RubyController : MonoBehaviour
{
    private PlayerInput rubyPlayerController;

    void Start()
    {

    }


    void Update()
    {
        Vector2 position = transform.position;
        position.x = position.x + 0.1f;
        transform.position = position;
    }

    void OnMove()
    {

    }
}
