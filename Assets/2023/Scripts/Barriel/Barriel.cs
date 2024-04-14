using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Barriel : MonoBehaviour
{
    [SerializeField] Animator _animBarriel;

    [Header("MESSAGE")]
    [SerializeField] GameObject _boxMessage;
    private bool _firstContact = false;

    void Start()
    {
        _animBarriel.enabled = false;
        _boxMessage.SetActive(false);
        _boxMessage.transform.DOScale(0, 0);
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
            StartCoroutine(ShowMessage());
            _firstContact = true;
        }   
    }

    private IEnumerator ShowMessage()
    {
        _boxMessage.gameObject.SetActive(true);
        _boxMessage.transform.DOScale(0.5f, 0.5f);
        yield return new WaitForSeconds(3f);
        _boxMessage.transform.DOScale(0f, 0.5f);
    }
}