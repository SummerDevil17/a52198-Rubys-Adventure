using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] int amountToDamage = 1;

    void OnTriggerStay2D(Collider2D other)
    {
        //Verifies if other has the controller and gets it, if returning true
        if (other.TryGetComponent<RubyController>(out RubyController rubyController))
        {
            rubyController.ChangeHealth(-amountToDamage);
        }
    }
}
