using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailQuest4 : MonoBehaviour
{
    [SerializeField] GameObject[] _objsToDestroy;
    [SerializeField] GameObject _canvasQuest4;
    [SerializeField] KeyCode _buttonInteractive = KeyCode.Return;
    private bool _isReading = false;
    private Pause _pause;

    [SerializeField] AudioClip _acceptSound;
    private AudioSource _myAudio;
    private Quest4 _quest4;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _quest4 = GetComponent<Quest4>();
        _pause = FindObjectOfType<Pause>();
    }

    void Start()
    {
        _isReading = false;
        _canvasQuest4.SetActive(false);
        foreach (var item in _objsToDestroy)
            item.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(_buttonInteractive) && _isReading)
        {
            _myAudio.PlayOneShot(_acceptSound);
            _pause.Defrize();
            _canvasQuest4.SetActive(true);
            _quest4.StartTime();
            foreach (var item in _objsToDestroy)
                Destroy(item.gameObject);

            Destroy(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _objsToDestroy[1].SetActive(true); // MUESTRO EL MAIL DE LA QUEST
            _pause.Freeze();
            _isReading = true;
        }
    }
}