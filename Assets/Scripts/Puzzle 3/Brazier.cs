using UnityEngine;

public class Brazier : MonoBehaviour
{
    public GameObject fire;
    public Transform torch1;
    public Transform torch2;
    public Transform torch3;
    public Snowball snowball;
    public GameObject iconTorch;

    void Start()
    {
        fire.gameObject.SetActive(false);
        iconTorch.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (snowball.torchInArea)
            {
                fire.gameObject.SetActive(true);
            }

        }

        if (collision.gameObject.tag == "Player")
        {
            iconTorch.gameObject.SetActive(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(snowball.torchInArea)
            {
                fire.gameObject.SetActive(true);
                iconTorch.gameObject.SetActive(false);
            }  
        }

        if (collision.gameObject.tag == "Player")
        {
            iconTorch.gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            iconTorch.gameObject.SetActive(false);
        }
    }


}