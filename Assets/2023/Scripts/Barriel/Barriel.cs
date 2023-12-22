using UnityEngine;
using System.Collections;

public class Barriel : MonoBehaviour
{
    [SerializeField] Animator _animBarriel;

    [Header("MESSAGE")]
    [SerializeField] string _stringMessage;
    [SerializeField] Sprite _iconMessage;
    private bool _firstContact = false;
    private MessageSlide _message;
    

    private void Awake()
    {
        _message = FindObjectOfType<MessageSlide>();
    }

    void Start()
    {
        _animBarriel.enabled = false;
    }

    public void UpBarriel(float time, bool up, bool down)
    {
        StartCoroutine(DownBarriel(time, up, down));
    }

    private IEnumerator DownBarriel(float time, bool up, bool down)
    {
        _animBarriel.enabled = true;
        _animBarriel.SetBool("Up", up);
        _animBarriel.SetBool("Down", down);
        yield return new WaitForSeconds(time);
        _animBarriel.SetBool("Up", up);
        _animBarriel.SetBool("Down", down);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && !_firstContact)
        {
            _message.ShowMessage(_stringMessage, _iconMessage);
            _firstContact = true;
        }
            
    }
}