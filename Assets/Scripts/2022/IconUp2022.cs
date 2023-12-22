using UnityEngine;
using UnityEngine.UI;

public class IconUp2022 : MonoBehaviour
{
    public Image iconDog;
    private NewDogUp _isUp;

    private void Start()
    {
        iconDog.gameObject.SetActive(false);
        _isUp = FindObjectOfType<NewDogUp>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && _isUp.isUp) iconDog.gameObject.SetActive(false);
        else if (other.gameObject.tag == "Player" && !_isUp.isUp) iconDog.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") iconDog.gameObject.SetActive(false);
    }
}