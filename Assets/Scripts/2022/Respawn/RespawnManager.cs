using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [Header("POS CONFIG")]
    public Transform _posRespawn;

    [Header("AUDIO CONFIG")]
    [SerializeField] AudioClip _soundSplash;
    AudioSource _myAudio;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            _myAudio.PlayOneShot(_soundSplash);
            player.gameObject.transform.position = _posRespawn.position;
        }
    }
}