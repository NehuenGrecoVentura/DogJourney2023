using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfTutotiral : MonoBehaviour
{
    public GameObject wolfAlert;
    public GameObject zzz;
    public AreaWolfTutorial area;

    private void Start()
    {
        wolfAlert.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (area.detected) wolfAlert.gameObject.SetActive(true);
        else wolfAlert.gameObject.SetActive(false);
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            wolfAlert.gameObject.SetActive(true);
            zzz.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }


}
