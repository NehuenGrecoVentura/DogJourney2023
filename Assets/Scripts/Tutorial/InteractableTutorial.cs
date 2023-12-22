using UnityEngine;

public class InteractableTutorial : MonoBehaviour
{
    public GameObject message;
    public DogOrders dogOrders;
    public NewDogUp dogUp;
    public testanim anim;
    public LevelController levelController;
    public GameObject iconDiary;
    private AudioSource _myAudio;
    public AudioClip soundNotification;

    private void Start()
    {
        _myAudio = GetComponent<AudioSource>();
        iconDiary.gameObject.SetActive(false);
        message.gameObject.SetActive(false);
        if (gameObject.name == "Tutorial 6") levelController.enabled = false;
        if (gameObject.name == "Tutorial 2")
        {
            dogOrders.enabled = false;
            dogUp.enabled = false;
            anim.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            message.gameObject.SetActive(true);
            _myAudio.PlayOneShot(soundNotification);
        }
            
        if (other.gameObject.tag == "Player" && gameObject.name=="Tutorial 6") levelController.enabled=true;
        if (other.gameObject.tag == "Player" && gameObject.name == "Tutorial 2")
        {
            dogOrders.enabled = true;
            dogUp.enabled = true;
            anim.enabled = true;
        }

        if (other.gameObject.tag == "Player" && gameObject.name == "Tutorial 3") iconDiary.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") message.gameObject.SetActive(false);
    }
}
