using UnityEngine;

public class ContactRabbit : MonoBehaviour
{
    public ParticleSystem eatEffect;
    public AudioSource audioRabbit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Rabbit walk" && Input.GetKeyDown(KeyCode.F))
        {
            Destroy(eatEffect);
            Destroy(audioRabbit);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Rabbit walk" && Input.GetKeyDown(KeyCode.F))
        {
            Destroy(eatEffect);
            Destroy(audioRabbit);
        }   
    }
}