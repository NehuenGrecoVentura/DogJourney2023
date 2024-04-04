using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Article : MonoBehaviour
{
    [Header("BORDERS")]
    [SerializeField] Image _borderSelected;
    [SerializeField] Image _border;

    [Header("AUDIOS")]
    [SerializeField] AudioClip[] _buttonSounds;
    private AudioSource _myAudio;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        ExitArticle();
    }

    private void ButtonSelectStatus(bool borderActive, bool borderSelectActive, float scale, float time)
    {
        _border.gameObject.SetActive(borderActive);
        _borderSelected.gameObject.SetActive(borderSelectActive);
        transform.DOScale(scale, time);
    }

    public void EnterArticle()
    {
        if(_buttonSounds.Length > 0)
        {
            int random = Random.Range(0, _buttonSounds.Length);
            _myAudio.PlayOneShot(_buttonSounds[random]);
        }

        ButtonSelectStatus(false, true, 7.5f, 0.5f);
    }

    public void ExitArticle()
    {
        ButtonSelectStatus(true, false, 7f, 0.5f);
    }
}   