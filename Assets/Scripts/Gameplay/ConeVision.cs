using UnityEngine;

public class ConeVision : MonoBehaviour
{
    public SensorWolf sensorWolf;
    public bool detected;
    private AudioSource _aS;
    public AudioClip soundDetected;
    private DogTest[] _dogs;

    private void Start()
    {
        _aS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            detected = true;
            _aS.PlayOneShot(soundDetected);
        }

        if (other.gameObject.layer == 9)
        {
            _aS.PlayOneShot(soundDetected);
            foreach (var dog in _dogs) dog.Scared = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            sensorWolf.WolfIconDetected();
            detected = true;
        }  
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            sensorWolf.ResetIconWolf();
            detected = false;
        }      
    }
}