using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIQuestStatus : MonoBehaviour
{
    [Header("ICONS CONFIG")]
    [SerializeField] Image _iconQuestActive;
    [SerializeField] Image _iconQuestCompleted;
    [SerializeField] Animation _anim;

    [Header("TIME CONFIG")]
    [SerializeField] float _timeInScreen;
    float _auxTime;

    [Header("TEXTS CONFIG")]
    [SerializeField] TMP_Text _text;
    public string nextDescription;
    public bool completed = false;

    [Header("AUDIO CONFIG")]
    [SerializeField] AudioSource _myAudio;
    [SerializeField] AudioClip _soundCompleted;
    public AudioClip[] sound;

    void Start()
    {
        _auxTime = _timeInScreen;
        _anim.Stop();
        _iconQuestActive.gameObject.SetActive(true);
        _iconQuestCompleted.gameObject.SetActive(false);
        _text.color = Color.white;
    }

    void Update()
    {
        if (completed) StatusCompleted();
        else _timeInScreen = _auxTime;
    }

    public void StatusCompleted()
    {
        if(!_myAudio.isPlaying)
        {
            _myAudio.PlayOneShot(_soundCompleted);
            _soundCompleted = null;
        }

        _anim.Play();
        _iconQuestCompleted.gameObject.SetActive(true);
        _iconQuestActive.gameObject.SetActive(false);
        _timeInScreen -= Time.deltaTime;
        if (_timeInScreen <= 0)
        {
            completed = false;
            _anim.Stop();
            _timeInScreen = 0;
            _iconQuestCompleted.gameObject.SetActive(false);
            _iconQuestActive.gameObject.SetActive(true);
            //_timeInScreen = _auxTime;
            _text.text = nextDescription;
            _text.color = Color.white;
            foreach (var item in sound) _soundCompleted = item;
            
        }
    }
}