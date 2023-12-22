using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeiserPuzzle4 : MonoBehaviour
{
    public GameObject geiser;
    [HideInInspector] public bool button1On = false;
    [HideInInspector] public bool button2On = false;
    [HideInInspector] public bool button3On = false;
    [HideInInspector] public bool button4On = false;
    public GameObject geiser2;
    public GameObject geiser3;
    public GameObject wolf2;
    public GameObject wolf3;


    public GeiserPuzzle4 script1;
    public GeiserPuzzle4 script2;
    public GeiserPuzzle4 script3;
    public GeiserPuzzle4 script4;
    public bool allActive = false;
    private AudioSource _myAudio;
    public AudioClip soundButton;
    private void Start()
    {
        geiser.gameObject.SetActive(false);
        geiser2.gameObject.SetActive(false);
        geiser3.gameObject.SetActive(false);
        _myAudio = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        //if (gameObject.name == "Button 2") button2On = true;
        if (other.gameObject.tag == "Player" || other.gameObject.layer == 9) _myAudio.PlayOneShot(soundButton);
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == 9)
        {
            if (gameObject.name == "Button 1")
            {
                button1On = true;
                geiser.gameObject.SetActive(true);
            }

            if (gameObject.name == "Button 2")
            {
                button2On = true;
                print("BOTON 2 APRETADO");
            }

            if (gameObject.name == "Button 3")
            {
                button3On = true;
                print("BOTON 3 APRETADO");
            }    

            if (gameObject.name == "Button 4")
            {
                button4On = true;
                print("BOTON 4 APRETADO");
            }
                
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.layer == 9)
        {
            if (gameObject.name == "Button 1")
            {
                button1On = false;
                geiser.gameObject.SetActive(false);
            }

            if (gameObject.name == "Button 3") button3On = false;
            if (gameObject.name == "Button 4") button4On = false;



        }

        
    }
}