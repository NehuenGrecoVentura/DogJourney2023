using UnityEngine;

public class SoundGround : MonoBehaviour
{
    AudioSource _myAudio;
    public AudioClip soundGround;
    public AudioClip soundDownGround;

    void Start()
    {
        _myAudio = GetComponent<AudioSource>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 11 && Input.GetKeyDown(KeyCode.Space)) _myAudio.PlayOneShot(soundGround);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == 11) _myAudio.PlayOneShot(soundDownGround);
    }
}