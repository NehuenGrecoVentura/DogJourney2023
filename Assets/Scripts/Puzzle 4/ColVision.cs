using UnityEngine;

public class ColVision : MonoBehaviour
{
    public GameManager gameManager;
    public SensorWolf sensors1;
    public SensorWolf sensors2;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Sensor Wolf")
        {
            if (other.gameObject == sensors1) sensors1.WolfIconDetected();   
            if (other.gameObject == sensors2) sensors2.WolfIconDetected();
            print("ENTER");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Sensor Wolf")
        {
            print("STAY");
            if (other.gameObject == sensors1) sensors1.WolfIconDetected();
            if (other.gameObject == sensors2) sensors2.WolfIconDetected();
        }
    }
}
