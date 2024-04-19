using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TutorialFishing : MonoBehaviour
{
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _message;
    [SerializeField, TextArea(4,6)] string[] _lines;
    [SerializeField] Image _arrow;
    [SerializeField] Image[] _count;
    private FishingMinigame _fishing;


    private void Awake()
    {
        _fishing = FindObjectOfType<FishingMinigame>();
    }

    private void Start()
    {
        StartCoroutine(TutorialSpace());
    }

    private IEnumerator TutorialSpace()
    {
        _fishing.Gaming = false;
        _message.text = _lines[0];
        _boxMessage.DOAnchorPosY(-1000f, 0);
        _boxMessage.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        // Tutorial SPACEBAR
        _boxMessage.gameObject.SetActive(true);
        _boxMessage.DOAnchorPosY(70f, 0.5f);
        yield return new WaitForSeconds(3f);   
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);

        // TUTRORIAL KEEP FISH
        yield return new WaitForSeconds(0.5f);
        _message.text = _lines[1];
        _boxMessage.DOAnchorPosY(70f, 0.5f);

        // TUTORIAL SLIDER
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        _message.text = _lines[2];
        _arrow.DOColor(Color.white, 0.5f);
        _boxMessage.DOAnchorPosY(70f, 0.5f);
        _arrow.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);
        _arrow.DOColor(Color.clear, 0.5f);

        // CONTEO
        _count[0].DOColor(Color.white, 0.5f);
        yield return new WaitForSeconds(1f);
        _count[0].DOColor(Color.clear, 0.5f);

        _count[1].DOColor(Color.white, 0.5f);
        yield return new WaitForSeconds(1f);
        _count[1].DOColor(Color.clear, 0.5f);

        _count[2].DOColor(Color.white, 0.5f);
        yield return new WaitForSeconds(1f);
        _count[2].DOColor(Color.clear, 0.5f);

        _count[3].DOColor(Color.white, 0.5f);
        yield return new WaitForSeconds(1f);
        _count[3].DOColor(Color.clear, 0.5f);

        yield return new WaitForSeconds(1f);
        _fishing.Gaming = true;
    }
}