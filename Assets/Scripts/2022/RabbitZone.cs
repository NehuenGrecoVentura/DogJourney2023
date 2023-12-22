using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitZone : MonoBehaviour
{
    public Rabbit2022 rabbit;
    public Carrot2022 carrot;
    private bool _isHide = false;

    private void Update()
    {
        if (_isHide && !carrot.carrotInArea) rabbit.GoToCave();
        else rabbit.Out();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _isHide = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _isHide = false;
        }
    }
}
