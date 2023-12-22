using UnityEngine;

public class Buttons : MonoBehaviour
{
    [HideInInspector] public bool button1Acitve = false;
    [HideInInspector] public bool button2Acitve = false;
    [HideInInspector] public bool button3Acitve = false;
    [HideInInspector] public bool button4Acitve = false;
    AudioSource _myAudio;
    public AudioClip soundButton;

    private void Start()
    {
        _myAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == 9)
        {
            if (gameObject.name == "Button 1") _myAudio.PlayOneShot(soundButton);
            if (gameObject.name == "Button 2") _myAudio.PlayOneShot(soundButton);
            if (gameObject.name == "Button 3") _myAudio.PlayOneShot(soundButton);
            if (gameObject.name == "Button 4") _myAudio.PlayOneShot(soundButton);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == 9)
        {
            if (gameObject.name == "Button 1") button1Acitve = true;
            if (gameObject.name == "Button 2") button2Acitve = true;
            if (gameObject.name == "Button 3") button3Acitve = true;
            if (gameObject.name == "Button 4") button4Acitve = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == 9)
        {
            if (gameObject.name == "Button 1") button1Acitve = false;
            if (gameObject.name == "Button 2") button2Acitve = false;
            if (gameObject.name == "Button 3") button3Acitve = false;
            if (gameObject.name == "Button 4") button4Acitve = false;
        }
    }
}