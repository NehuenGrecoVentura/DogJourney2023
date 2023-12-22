using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carrot2022 : MonoBehaviour
{
    SphereCollider _col;
    [HideInInspector]
    public bool carrotInArea = false;
    public Image iconArea;
    public float timeToDestruction;
    public float speedToDestruction;

    private void Awake()
    {
        gameObject.AddComponent<Grabbable>();
    }

    private void Start()
    {
        iconArea.gameObject.SetActive(false);
        _col = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (carrotInArea)
        {
            Destroy(_col);
            Destroy(gameObject.GetComponent<Grabbable>());
            Destroy(gameObject.GetComponent<Prompt>());
            timeToDestruction -= speedToDestruction * Time.deltaTime;
            if (timeToDestruction <= 0)
            {
                timeToDestruction = 0;
                carrotInArea = false;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Rabbit Hide") iconArea.gameObject.SetActive(true);
        if (other.gameObject.name == "Rabbit Hide" && Input.GetKeyDown(KeyCode.F))
        {
            carrotInArea = true;
            Destroy(iconArea);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Rabbit Hide" && !carrotInArea) iconArea.gameObject.SetActive(false);
    }


}
