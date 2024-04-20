using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MailQuest5 : MonoBehaviour
{
    [SerializeField] GameObject[] _objsToDestroy;
    [SerializeField] GameObject _canvasQuest5;
    [SerializeField] KeyCode _buttonInteractive = KeyCode.Return;
    private bool _isReading = false;
    private Pause _pause;

    [SerializeField] AudioClip _acceptSound;
    private AudioSource _myAudio;
    private Quest5 _quest5;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _quest5 = GetComponent<Quest5>();
        _pause = FindObjectOfType<Pause>();
    }

    void Start()
    {
        _isReading = false;
        _canvasQuest5.SetActive(false);
        _quest5.enabled = false;
        foreach (var item in _objsToDestroy)
            item.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(_buttonInteractive) && _isReading)
        {
            _myAudio.PlayOneShot(_acceptSound);
            _pause.Defrize();
            _canvasQuest5.SetActive(true);
            _quest5.enabled = true;
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
            _objsToDestroy[0].SetActive(true); // MUESTRO EL MAIL DE LA QUEST
            _pause.Freeze();
            _isReading = true;
        }
    }
}