using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageSlide : MonoBehaviour
{
    [SerializeField] GameObject _message;
    [SerializeField] float _timeActive;
    [SerializeField] Image _icon;
    [SerializeField] TMP_Text _text;
    [SerializeField] Animator _anim;

    void Start()
    {
        _message.SetActive(false);
    }

    public void ShowMessage(string text, Sprite icon)
    {
        StartCoroutine(ShowMessageCoroutine(text, icon));
    }

    private IEnumerator ShowMessageCoroutine(string text, Sprite icon)
    {
        _message.SetActive(true);
        _anim.SetBool("In", true);
        _anim.SetBool("Out", false);
        _text.text = text;
        _icon.sprite = icon;
        yield return new WaitForSeconds(_timeActive);
        _anim.SetBool("In", false);
        _anim.SetBool("Out", true);
        yield return new WaitForSeconds(1.5f);
        _message.SetActive(false);
    }
}