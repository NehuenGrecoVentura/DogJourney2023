using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    [SerializeField] Slider _sliderloadBar;
    [SerializeField] GameObject _panelLoadBar;
    [SerializeField] GameObject _menu;

    public void SceneLoad(int sceneIndex)
    {
        _menu.SetActive(false);
        _panelLoadBar.SetActive(true);
        StartCoroutine(LoadAsync(sceneIndex));
    }

    IEnumerator LoadAsync(int sceneIndex)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncOperation.isDone)
        {
            _sliderloadBar.value = asyncOperation.progress / 0.9f;
            yield return null;
        }
    }
}
