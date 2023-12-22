using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeiserController : MonoBehaviour
{
    [Header("Geisers Objects")]
    public GameObject geiserSensor1;
    public GameObject geiserSensor2;
    public GameObject geiserSensor3;
    public GameObject geiserSensor4;
    [Header("Geisers Effects")]
    public GameObject geiserPatricle1;
    public GameObject geiserPatricle2;
    public GameObject geiserPatricle3;
    public GameObject geiserPatricle4;
    [Header("Geisers Buttons")]
    public GameObject geiserController2;
    public GameObject geiserController3;
    [Header("Geisers Icons")]
    public GameObject geiser1IconDefault;
    public GameObject geiser2IconDefault;
    public GameObject geiser3IconDefault;
    public GameObject geiserWhiteIcon;
    public GameObject geiserOrangeIcon;
    public GameObject geiserGreenIcon;
    [HideInInspector]
    public bool allActive = false;
    public Text feedbackMessage;
    public float time = 2f;
    public float speed = 1f;
    private AudioSource _myAudio;
    public AudioClip soundButton;
    [HideInInspector]
    public bool button1On = false;
    [HideInInspector]
    public bool button2On = false;
    [HideInInspector]
    public bool button3On = false;
    [HideInInspector]
    public bool isAllActive = false;
    public GeiserController manager1;
    public GeiserController manager2;
    public GeiserController manager3;
    [Header("UI ICons")]
    public GameObject icon2;
    public GameObject icon3;
    private Animation anim;

    private void Start()
    {
        AllGeisersOff();
        feedbackMessage.gameObject.SetActive(false);
        geiserWhiteIcon.gameObject.SetActive(false);
        geiserOrangeIcon.gameObject.SetActive(false);
        geiserGreenIcon.gameObject.SetActive(false);
        icon2.gameObject.SetActive(false);
        icon3.gameObject.SetActive(false);
        _myAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animation>();

    }

    public void Update()
    {
        if (button1On && button2On && button3On)
        {
            isAllActive = true;
            AllGeisersOn();
            feedbackMessage.gameObject.SetActive(true);
            time -= Time.deltaTime * speed;
            if (time <= 0)
            {
                time = 0;
                feedbackMessage.gameObject.SetActive(false);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == 9) // SI EN EL GEISER ESTÁN ENCIMA EL JUGADOR O LOS PERROS...
        {
            if (gameObject.name == "Geiser Controller 1") _myAudio.PlayOneShot(soundButton);
            if (gameObject.name == "Geiser Controller 2") _myAudio.PlayOneShot(soundButton);
            if (gameObject.name == "Geiser Controller 3")
            {
                _myAudio.PlayOneShot(soundButton);
                AllGeisersOn();
                allActive = true;
                button3On = true;
                feedbackMessage.gameObject.SetActive(true);
                time -= Time.deltaTime * speed;
                if (time <= 0)
                {
                    time = 0;
                    feedbackMessage.gameObject.SetActive(false);
                }
            }
        }

        if (other.gameObject.layer == 15 && Input.GetKeyDown(KeyCode.F) && gameObject.name == "Hueco 2" && other.gameObject.name == "Cilinder 2") _myAudio.PlayOneShot(soundButton);
        if (other.gameObject.layer == 15 && Input.GetKeyDown(KeyCode.F) && gameObject.name == "Hueco 3" && other.gameObject.name == "Cilinder 3") _myAudio.PlayOneShot(soundButton);
    }




    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == 9) // SI EN EL GEISER ESTÁN ENCIMA EL JUGADOR O LOS PERROS...
        {
            if (gameObject.name == "Geiser Controller 1")
            {
                Geiser2and3On();
            }


            if (gameObject.name == "Geiser Controller 2")
            {
                Geiser1and4On();
            }

            if (gameObject.name == "Geiser Controller 3")
            {
                AllGeisersOn();
                allActive = true;
                button3On = true;
                feedbackMessage.gameObject.SetActive(true);
                time -= Time.deltaTime * speed;
                if (time <= 0)
                {
                    time = 0;
                    feedbackMessage.gameObject.SetActive(false);
                }
            }
        }

        if (other.gameObject.layer == 15 && Input.GetKeyDown(KeyCode.F) && gameObject.name == "Hueco 2" && other.gameObject.name == "Cilinder 2")
        {
            geiserController2.SetActive(true);
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        if (other.gameObject.layer == 15 && gameObject.name == "Hueco 2" && other.gameObject.name == "Cilinder 2") icon2.gameObject.SetActive(true);
        if (other.gameObject.layer == 15 && gameObject.name == "Hueco 3" && other.gameObject.name == "Cilinder 3") icon3.gameObject.SetActive(true);

        if (other.gameObject.layer == 15 && Input.GetKeyDown(KeyCode.F) && gameObject.name == "Hueco 3" && other.gameObject.name == "Cilinder 3")
        {
            geiserController3.SetActive(true);
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        anim.Stop("Button1");
        anim.Stop("Button2");
        anim.Stop("Button3");
        if (allActive) AllGeisersOn();
        else AllGeisersOff();
        geiser1IconDefault.gameObject.SetActive(true);
        geiserWhiteIcon.gameObject.SetActive(false);
        geiser2IconDefault.gameObject.SetActive(true);
        geiserOrangeIcon.gameObject.SetActive(false);
        geiser3IconDefault.gameObject.SetActive(true);
        if (other.gameObject.layer == 15 && gameObject.name == "Hueco 2" && other.gameObject.name == "Cilinder 2") icon2.gameObject.SetActive(false);
        if (other.gameObject.layer == 15 && gameObject.name == "Hueco 3" && other.gameObject.name == "Cilinder 3") icon3.gameObject.SetActive(false);
    }

    public void Geiser2and3On()
    {
        anim.Play("Button1");
        button1On = true;
        geiser1IconDefault.gameObject.SetActive(false);
        geiserWhiteIcon.gameObject.SetActive(true);
        geiserSensor2.SetActive(true);
        geiserPatricle2.SetActive(true);
        geiserSensor3.SetActive(true);
        geiserPatricle3.SetActive(true);
    }

    public void Geiser1and4On()
    {
        anim.Play("Button2");
        button2On = true;
        geiser2IconDefault.gameObject.SetActive(false);
        geiserOrangeIcon.gameObject.SetActive(true);
        geiserSensor1.SetActive(true);
        geiserPatricle1.SetActive(true);
    }

    public void AllGeisersOn()
    {
        anim.Play("Button3");
        geiserSensor1.SetActive(true);
        geiserPatricle1.SetActive(true);
        geiserSensor2.SetActive(true);
        geiserPatricle2.SetActive(true);
        geiserSensor3.SetActive(true);
        geiserPatricle3.SetActive(true);
        geiserSensor4.SetActive(true);
        geiserPatricle4.SetActive(true);

        geiser1IconDefault.SetActive(false);
        geiser2IconDefault.SetActive(false);
        geiser3IconDefault.SetActive(false);
        geiserWhiteIcon.SetActive(true);
        geiserOrangeIcon.SetActive(true);
        geiserGreenIcon.SetActive(true);

    }
    public void AllGeisersOff()
    {
        geiserSensor1.SetActive(false);
        geiserPatricle1.SetActive(false);
        geiserSensor2.SetActive(false);
        geiserPatricle2.SetActive(false);
        geiserSensor3.SetActive(false);
        geiserPatricle3.SetActive(false);
        geiserSensor4.SetActive(false);
        geiserPatricle4.SetActive(false);
    }


}