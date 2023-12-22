using UnityEngine;
using UnityEngine.UI;

public class DogProhibed : MonoBehaviour
{
    public Image iconDogProhibed;

    void Start()
    {
        iconDogProhibed.gameObject.SetActive(false);
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9) iconDogProhibed.gameObject.SetActive(true);
    }

    public void OnTriggerExit(Collider other)
    {
        iconDogProhibed.gameObject.SetActive(false);
    }
}
