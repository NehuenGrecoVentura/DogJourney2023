using UnityEngine;

public class UIUp : MonoBehaviour
{
    public GameObject icon1UpDog;
    public GameObject icon2UpDog;
    public GameObject icon3UpDog;
    public NewDogUp dogUpManager;

    private void Start()
    {
        icon1UpDog.gameObject.SetActive(false);
        icon2UpDog.gameObject.SetActive(false);
        icon3UpDog.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (dogUpManager.isUp)
        {
            icon1UpDog.gameObject.SetActive(false);
            icon2UpDog.gameObject.SetActive(false);
            icon3UpDog.gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && gameObject.name=="chungo") icon1UpDog.gameObject.SetActive(true);
        if (other.gameObject.tag == "Player" && gameObject.name=="bobby") icon2UpDog.gameObject.SetActive(true);
        if (other.gameObject.tag == "Player" && gameObject.name=="choripan") icon3UpDog.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && gameObject.name == "chungo") icon1UpDog.gameObject.SetActive(false);
        if (other.gameObject.tag == "Player" && gameObject.name == "bobby") icon2UpDog.gameObject.SetActive(false);
        if (other.gameObject.tag == "Player" && gameObject.name == "choripan") icon3UpDog.gameObject.SetActive(false);
    }
}