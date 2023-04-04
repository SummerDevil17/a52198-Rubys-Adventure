using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] int amountToGive = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Verifies if other has the controller and gets it, if returning true
        if (other.TryGetComponent<RubyController>(out RubyController rubyController))
        {
            if (rubyController.CurrentHealth >= rubyController.MaxHealth) return;

            rubyController.ChangeHealth(amountToGive);
            Destroy(gameObject);
        }
    }
}
