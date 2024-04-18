using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class SceneLoadManager : MonoBehaviour
{
    [SerializeField] Slider _sliderloadBar;
    [SerializeField] GameObject _panelLoadBar;
    [SerializeField] GameObject _menu;
    [SerializeField] GameObject _logo;

    [SerializeField] RectTransform _fadeOut;
    [SerializeField] Image _imageLoad;
    [SerializeField] TMP_Text _textPercentage;

    public void SceneLoad(int sceneIndex)
    {
        //_menu.SetActive(false);
        //_panelLoadBar.SetActive(true);
        //StartCoroutine(LoadAsync(sceneIndex));
        StartCoroutine(StartGame(sceneIndex));
    }

    private IEnumerator StartGame(int sceneIndex)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _logo.SetActive(false);
        _menu.SetActive(false);
        _fadeOut.gameObject.SetActive(true);
        _fadeOut.DOScale(100, 2f);
        yield return new WaitForSeconds(2f);
        _panelLoadBar.SetActive(true);
        _imageLoad.gameObject.SetActive(true);
        _imageLoad.DOColor(new Color(255, 255, 255, 255), 10f);
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        _sliderloadBar.value = 0;
        float progress = 0;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncOperation.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
            _sliderloadBar.value = progress;
            _textPercentage.text = Mathf.RoundToInt(progress * 100).ToString() + "%";
            if (progress >= 0.9f) _sliderloadBar.value = 1;
            yield return null;
        }
    }
}
