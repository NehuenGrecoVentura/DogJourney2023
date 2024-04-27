using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Fishing : MonoBehaviour
{
    private FishingMinigame _fishing;
    [SerializeField] Camera _camFishing;

    private void Awake()
    {
        _fishing = FindObjectOfType<FishingMinigame>();
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //_fishing.Victory = false;
                //_fishing.Reset();

                _fishing.start = true;
                _fishing.Gaming = true;
                _camFishing.enabled = true;
            }
        }
    }
}