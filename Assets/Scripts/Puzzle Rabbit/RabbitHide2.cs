using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitHide2 : MonoBehaviour
{
    public Carrot carrot;
    public RabbitHide2 myScript;
    public RabbitNew rabbit;
    private bool hide = false;
    public AudioSource audioRabbit;
    public AudioClip soundHide;
    public AudioClip soundOut;

    void Update()
    {
        if (!hide) rabbit.Out();
    }

    private void OnTriggerEnter(Collider other) // EFECTO DE SONIDO CUANDO SE ESCONDE EL CONEJO
    {
        if (other.gameObject.name == "Rabbit Hide" && !carrot.carrotInArea) audioRabbit.PlayOneShot(soundHide);
    }

    public void OnTriggerStay(Collider other)
    {

        if (other.gameObject.name == "Rabbit Hide" && !carrot.carrotInArea) // SI EL CONEJO DETECTA AL JUGADOR SE ESCONDE
        {
            hide = true;
            rabbit.GoToCave();
        }

        if (other.gameObject.name == "Rabbit Hide" && carrot.carrotInArea) rabbit.GoToCarrot(); // SI EL JUGADOR ESTÁ LEJOS DEL CONEJO, ENTONCES SALE
    }

    private void OnTriggerExit(Collider other)
    {
        if (!carrot.canLootRabbit) hide = false;
        if (other.gameObject.name == "Rabbit Hide" && !carrot.carrotInArea) audioRabbit.PlayOneShot(soundOut);

    }
}
