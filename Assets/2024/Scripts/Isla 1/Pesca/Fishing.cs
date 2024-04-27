using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Fishing : MonoBehaviour
{
    private FishingMinigame _fishing;
    private Dialogue _dialogue;

    private void Awake()
    {
        _fishing = FindObjectOfType<FishingMinigame>();
        _dialogue = FindObjectOfType<Dialogue>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _dialogue.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _fishing.start = true;
            }
        }
    }
}