using UnityEngine;

public class Cartel : MonoBehaviour
{
    public GameObject map;

    void Start()
    {
        map.gameObject.SetActive(false);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") map.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") map.gameObject.SetActive(false);
    }
}