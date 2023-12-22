using UnityEngine;

public class SleepWolf : MonoBehaviour
{
    [HideInInspector]
    public bool detected = false;
    [HideInInspector]
    public bool stealthActive;
    SleepWolfAlert[] wolfs;
    [SerializeField] AudioSource _myAudio;
    public AudioClip soundDetected;

    private void Start()
    {
        stealthActive = false;
        wolfs = FindObjectsOfType<SleepWolfAlert>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && !stealthActive) stealthActive = true;
        if (Input.GetKey(KeyCode.LeftControl) && !stealthActive && (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.LeftControl) && !stealthActive && (Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.LeftControl) && !stealthActive && (Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.LeftControl) && !stealthActive && (Input.GetKey(KeyCode.D))))))
        {
            stealthActive = true;
        }

        foreach (var wolf in wolfs)
        {
            if (wolf.wolfWakeUp)
            {
                detected = true;
                
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftControl)) stealthActive = true;
        if (stealthActive) print("Estoy en sigilo");
        if (Input.GetKeyUp(KeyCode.LeftControl) && (Input.GetKey(KeyCode.W) ||
            (Input.GetKeyUp(KeyCode.LeftControl) && (Input.GetKey(KeyCode.A) ||
            (Input.GetKeyUp(KeyCode.LeftControl) && (Input.GetKey(KeyCode.S) ||
            (Input.GetKeyUp(KeyCode.LeftControl) && (Input.GetKey(KeyCode.D)))))))))
        {
            stealthActive = false;
        }
        else print("No estoy en sigilo");
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !stealthActive)
        {
            detected = true;
        }
            
            
            
        if (other.gameObject.tag == "Player" && stealthActive) detected = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !stealthActive)
        {
            detected = true;
            _myAudio.PlayOneShot(soundDetected);
           // Destroy(_myAudio);
        }
    }


}
