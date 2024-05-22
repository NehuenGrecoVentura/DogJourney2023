using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFound : MonoBehaviour
{
    [SerializeField] QuestSearch _quest;

    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;

    [Header("REPOS")]
    [SerializeField] Transform[] _repos;
    private int _index = 0;

    void Start()
    {
        _iconInteract.SetActive(false);
    }

    private void Repos()
    {
        if (_repos.Length > 0)
        {
            _index = (_index + 1) % _repos.Length;
            Transform nextPos = _repos[_index];
            transform.position = nextPos.position;
            _quest.AddFound();
            _iconInteract.SetActive(false);
        }

        else return;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(_keyInteract))
            Repos();
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.SetActive(false);
    }
}