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

    public void UpBarriel(float time)
    {
        StartCoroutine(DownBarriel(time));
    }

    private IEnumerator DownBarriel(float time)
    {
        _animBarriel.enabled = true;
        _animBarriel.SetBool("Up", true);
        _animBarriel.SetBool("Down", false);
        yield return new WaitForSeconds(time);
        _animBarriel.SetBool("Up", false);
        _animBarriel.SetBool("Down", true);
        Destroy(this);
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