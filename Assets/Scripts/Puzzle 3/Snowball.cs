using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
    private int totalTorchs = 0;
    public List<Torch> torchs = new List<Torch>();
    public GameObject snowball;
    [HideInInspector]
    public bool torch1;
    [HideInInspector]
    public bool torch2;
    [HideInInspector]
    public bool torch3;
    public GameObject fireInSnowBall;
    public float _timeToMelt = 10f;
    private float _speedMelt = 1f;
    [HideInInspector]
    public bool torchInArea = false;


    private void Start()
    {
        fireInSnowBall.gameObject.SetActive(false);
    }


    private void Update()
    {
        CheckTorchs();
    }

    void CheckTorchs()
    {
        if (torch1 && !torch2 && !torch3 || !torch1 && torch2 && !torch3 || !torch1 && !torch2 && torch3) totalTorchs = 1;
        if (torch1 && torch2 && !torch3 || torch1 && !torch2 && torch3 || !torch1 && torch2 && torch3) totalTorchs = 2;
        if (torch1 && torch2 && torch3) totalTorchs = 3;
        print(totalTorchs);
        if (totalTorchs == 3)
        {
            fireInSnowBall.gameObject.SetActive(true);
            _timeToMelt -= Time.deltaTime * _speedMelt;
            if (_timeToMelt <= 0)
            {
                Destroy(fireInSnowBall);
                Destroy(snowball);
                Destroy(gameObject);

            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.name == "Torch 1"))
        {
            torchInArea = true;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                torch1 = true;
                Destroy(other.gameObject);
            }
        }

        if ((other.gameObject.name == "Torch 2"))
        {
            torchInArea = true;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                torch2 = true;
                Destroy(other.gameObject);
            }
        }

        if ((other.gameObject.name == "Torch 3"))
        {
            torchInArea = true;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                torch3 = true;
                Destroy(other.gameObject);
            }
        }
    }
}