using UnityEngine;

public class BoxQuest2 : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.Q;
    [SerializeField] BoxQuest _npc;

    [Header("AUDIO")]
    [SerializeField] AudioSource _myAudio;

    private void Start()
    {
        _iconInteract.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var dog = other.GetComponent<Dog>();
        if (dog != null) BoxPicked();

        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(_keyInteract)) 
            _iconInteract.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.SetActive(false);
    }

    private void BoxPicked()
    {
        _myAudio.Play();
        _npc.BoxPicked();
        Destroy(gameObject);
    }
}