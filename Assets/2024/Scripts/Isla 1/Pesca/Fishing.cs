using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Fishing : MonoBehaviour
{
    private FishingMinigame _fishing;
    [SerializeField] Camera _camRender;
    [SerializeField] Camera _myCam;
    private Collider _myCol;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _fishing = FindObjectOfType<FishingMinigame>();
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                StartCoroutine(StartMiniGame());
            }
        }
    }

    private IEnumerator StartMiniGame()
    {
        Destroy(_myCol);
        _fishing.start = true;
        yield return new WaitForSeconds(2f);
        _fishing.Gaming = true;
        _camRender.enabled = true;
    }





}