using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWolfTutorial : MonoBehaviour
{
    public bool stealth;
    public bool detected;
    public GameObject wolfSleep;
    public GameObject wolfAlert;
    public Transform player;
    private AudioSource _aS;
    public AudioClip soundWakeUp;

    private void Start()
    {
        wolfSleep.gameObject.SetActive(true);
        wolfAlert.gameObject.SetActive(false);
        _aS = GetComponent<AudioSource>();
    }




    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            stealth = false;
        }

        else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift)  || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift)  || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
        {
            stealth = false;
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftControl))
        {
            stealth = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !stealth)
        {
            wolfSleep.gameObject.SetActive(false);
            wolfAlert.gameObject.SetActive(true);
            wolfAlert.gameObject.transform.LookAt(player);
            _aS.PlayOneShot(soundWakeUp);
        }

        if (other.gameObject.tag == "Player" && stealth)
        {
            wolfSleep.gameObject.SetActive(true);
            wolfAlert.gameObject.SetActive(false);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !stealth)
        {
            wolfSleep.gameObject.SetActive(false);
            wolfAlert.gameObject.SetActive(true);
            wolfAlert.gameObject.transform.LookAt(player);
        }

        if (other.gameObject.tag == "Player" && stealth)
        {
            wolfSleep.gameObject.SetActive(true);
            wolfAlert.gameObject.SetActive(false);
        }
    }



    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            wolfSleep.gameObject.SetActive(true);
            wolfAlert.gameObject.SetActive(false);
        }
    }
}
