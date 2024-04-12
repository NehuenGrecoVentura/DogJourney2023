using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Item : MonoBehaviour
{
    [Header("BORDERS")]
    [SerializeField] Image _borderSelected;
    [SerializeField] Image _border;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    private void ButtonSelectStatus(bool borderActive, bool borderSelectActive, float scale, float time)
    {
        _border.gameObject.SetActive(borderActive);
        _borderSelected.gameObject.SetActive(borderSelectActive);
        transform.DOScale(scale, time);
    }

    public void EnterItem()
    {
        ButtonSelectStatus(false, true, 7.5f, 0.5f);
    }

    public void ExitArticle()
    {
        ButtonSelectStatus(true, false, 7f, 0.5f);
    }
}
