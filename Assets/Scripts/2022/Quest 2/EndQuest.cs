using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndQuest : MonoBehaviour
{
    Collider _col;
    [SerializeField] Character2022 _player;
    [SerializeField] Inventory _inventory;
    [SerializeField] GManager _gm;

    [Header("INTERACTIVE CONFIG")]
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _buttonInteractive = KeyCode.F;

    [Header("OBJECTS TO DESTROY")]
    [SerializeField] GameObject[] _objectsToDestroy;
    [SerializeField] EndQuest _myScript;
    bool ending = false;

    [Header("NEXT QUEST CONFIG")]
    [SerializeField] LocationQuest _map;
    [SerializeField] Transform _nextLocation;
    [SerializeField] TMP_Text _textQuest;
    [SerializeField] string _nextText;
    [SerializeField] GameObject _nextQuestCanvas;

    [SerializeField] BoxUpgradeQuest _box;

    [SerializeField] Slider _sliderloadBar;
    [SerializeField] GameObject _panelLoadBar;

    private void Start()
    {
        _iconInteractive.SetActive(false);
        _col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (ending) LevelCompleted();
    }

    public void LevelCompleted()
    {
        _gm._winText.gameObject.SetActive(true);
        _gm.timeTextInScreen -= Time.deltaTime;
        if (_gm.timeTextInScreen <= 0)
        {
            _gm.timeTextInScreen = 0;
            _gm.timeTextInScreen = 2f;
            _gm._winText.gameObject.SetActive(false);
            _nextQuestCanvas.SetActive(true);
            Destroy(_myScript);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive) && _box.boxPicked)
                _iconInteractive.SetActive(true);

            else if (Input.GetKeyDown(_buttonInteractive) && _box.boxPicked)
            {
                _map.target = _nextLocation;
                _gm.level++;
                _player.EjecuteAnim("Win");
                _gm._myAudio.PlayOneShot(_gm._winSound);
                ending = true;
                foreach (var obj in _objectsToDestroy) Destroy(obj.gameObject);
                _textQuest.text = _nextText;
                Destroy(_col);
                _inventory.upgrade = true;
            }
        }
    }

    public void SceneLoad(int sceneIndex)
    {
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

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive) && _box.boxPicked)
                _iconInteractive.SetActive(true);

            else if (Input.GetKeyDown(_buttonInteractive) && _box.boxPicked)
            {
                _map.target = _nextLocation;
                _gm.level++;
                _player.EjecuteAnim("Win");
                _gm._myAudio.PlayOneShot(_gm._winSound);
                ending = true;
                foreach (var obj in _objectsToDestroy) Destroy(obj.gameObject);
                _textQuest.text = _nextText;
                Destroy(_col);
                _inventory.upgrade = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null) _iconInteractive.SetActive(false);
    }
}