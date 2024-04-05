using UnityEngine;
using TMPro;
using DG.Tweening;

public class ButtonMainMenu : MonoBehaviour
{
    [Header("AUDIOS")]
    [SerializeField] AudioClip[] _buttonSounds;
    private AudioSource _myAudio;

    [Header("STYLES TEXTS")]
    [SerializeField] TMP_FontAsset _styleButtonEnter;
    [SerializeField] TMP_FontAsset _styleButtonExit;
    [SerializeField] TMP_Text _textButton;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
    }

    public void ButtonEnter()
    {
        if (_buttonSounds.Length > 0)
        {
            int random = Random.Range(0, _buttonSounds.Length);
            _myAudio.PlayOneShot(_buttonSounds[random]);
        }

        _textButton.fontMaterial = _styleButtonEnter.material;
        transform.DOScale(0.38f, 0.5f);
    }

    public void ButtonExit()
    {
        _textButton.fontMaterial = _styleButtonExit.material;
        transform.DOScale(0.3f, 0.5f);
    }

    public void ButtonEnterPause()
    {
        if (_buttonSounds.Length > 0)
        {
            int random = Random.Range(0, _buttonSounds.Length);
            _myAudio.PlayOneShot(_buttonSounds[random]);
        }

        _textButton.fontMaterial = _styleButtonEnter.material;
    }

    public void ButtonExitPause()
    {
        _textButton.fontMaterial = _styleButtonExit.material;
    }
}
