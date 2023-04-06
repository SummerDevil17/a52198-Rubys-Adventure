using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] int amountToGive = 1;
    [SerializeField] ParticleSystem healthPickUpVFX;
    [SerializeField] AudioClip pickUpSFX;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Verifies if other has the controller and gets it, if returning true
        if (other.TryGetComponent<RubyController>(out RubyController rubyController))
        {
            if (rubyController.CurrentHealth >= rubyController.MaxHealth) return;

            rubyController.ChangeHealth(amountToGive);
            rubyController.PlaySound(pickUpSFX);
            Instantiate(healthPickUpVFX, transform.position, healthPickUpVFX.transform.rotation);

            Destroy(gameObject);
        }
    }
}
