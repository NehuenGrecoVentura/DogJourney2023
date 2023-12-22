using UnityEngine;
using UnityEngine.UI;

public class DogNidus : MonoBehaviour
{
    public AudioSource aS;
    public AudioClip soundEagle;
    public Image hint;
    private Nidus _nidus;

    private void Start()
    {
        hint.gameObject.SetActive(false);
        _nidus = FindObjectOfType<Nidus>();
    }

    private void Update()
    {
        if (_nidus.eggInNidus) aS.Stop();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            aS.PlayOneShot(soundEagle);
            hint.gameObject.SetActive(true);
            Destroy(hint, 2f);
        }           
    }
}