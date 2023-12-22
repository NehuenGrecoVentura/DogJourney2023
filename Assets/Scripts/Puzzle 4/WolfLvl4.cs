using UnityEngine;

public class WolfLvl4 : MonoBehaviour
{
    private bool _detected = false;
    public Transform player;
    private AudioSource _myAudio;
    public AudioClip soundDetected;
    public float speedRotate = 5.0f;
    public DogTest bobby;
    public DogTest choripan;


    private void Start()
    {
        _myAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        Rotate();
    }


    void Rotate()
    {
        if (!_detected) transform.Rotate(0, speedRotate * Time.deltaTime, 0, 0.0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            choripan.Scared = true;
            bobby.Scared = true;
            _myAudio.PlayOneShot(soundDetected);
        }
    }
}
