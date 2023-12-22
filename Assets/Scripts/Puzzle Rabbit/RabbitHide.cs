using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitHide : MonoBehaviour
{
    public GameObject rabbit;
    public GameObject rabbitHiden;
    [SerializeField] Carrot _carrot;
    [SerializeField] RabbitHide _myScript;
    [SerializeField] AudioSource _myAudio;
    [SerializeField] AudioSource _myAudioHiden;
    public AudioClip soundRabbit;
    public AudioClip soundRabbitHide;
    public ParticleSystem effectEat;

    private void Start()
    {
        rabbitHiden.gameObject.SetActive(false);
        effectEat.Stop();
    }


    private void Update()
    {
        if (_carrot.carrotInArea)
        {
            Destroy(_myScript);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Rabbit Hide") _myAudioHiden.PlayOneShot(soundRabbitHide);
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name=="Rabbit Hide")
        {
            rabbit.SetActive(false);
            rabbitHiden.gameObject.SetActive(true);
            //    _myAudioHiden.PlayOneShot(soundRabbitHide);
            effectEat.Stop();
        }

        if (other.gameObject.name == "Rabbit Hide" && !_carrot.carrotInArea)
        {
            rabbit.SetActive(false);
            rabbitHiden.gameObject.SetActive(true);
            effectEat.Stop();
           
        }

        if (other.gameObject.name == "Rabbit Hide" && _carrot.carrotInArea)
        {
            rabbit.SetActive(true);
            rabbitHiden.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Rabbit walk" && Input.GetKeyDown(KeyCode.F)) Destroy(_myAudio);

    }

    public void OnTriggerExit(Collider other)
    {
        if (_carrot.carrotInArea)
        {
            rabbit.SetActive(true);
            rabbitHiden.gameObject.SetActive(false);
        }

        rabbit.SetActive(true);
        rabbitHiden.gameObject.SetActive(false);
        if (other.gameObject.name == "Rabbit Hide")
        {
            _myAudio.PlayOneShot(soundRabbit);
            effectEat.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rabbit walk" && Input.GetKeyDown(KeyCode.F)) Destroy(_myAudio);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Rabbit walk" && Input.GetKeyDown(KeyCode.F)) Destroy(_myAudio);
    }


}
