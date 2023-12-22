using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inputs : MonoBehaviour
{
    private PickAxe _axe;    
    private bool _w, _a, _s, _d;

    [SerializeField] GameObject _tutorialTrees;
    [SerializeField] Image[] _keys;
    [SerializeField] TMP_Text _text;
    [SerializeField] Button _buttonSellWoods;
    [SerializeField] GameObject[] _nextStep;
    [SerializeField] float _timeToNextMessage;
    [SerializeField] Collider _mailQuest1;
    [SerializeField] GameObject _tutorialAddDog;
    [SerializeField] GameObject _womanInMarket;
    [SerializeField] DogBall _dogBall;

    [Header("RADAR")]
    [SerializeField] Transform _nextPos;
    private LocationQuest _radar;

    private void Awake()
    {
        _axe = FindObjectOfType<PickAxe>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    void Start()
    {
        _axe.enabled = false;
        _mailQuest1.enabled = false;
        _tutorialTrees.SetActive(false);
        _buttonSellWoods.gameObject.SetActive(false);
        _tutorialAddDog.SetActive(false);
        _womanInMarket.SetActive(false);
        _radar.StatusRadar(false);
        _dogBall.enabled = false;
        NextStep(false);
    }

    void Update()
    {
        CheckKey(KeyCode.W, ref _w, _keys[0]);
        CheckKey(KeyCode.A, ref _a, _keys[1]);
        CheckKey(KeyCode.S, ref _s, _keys[2]);
        CheckKey(KeyCode.D, ref _d, _keys[3]);

        if (_w && _a && _s && _d)
            TutorialSuccess();
    }

    private void TutorialSuccess()
    {
        foreach (var key in _keys)
        {
            key.color = Color.green;
            _text.color = Color.green;
            _text.text = "GOOD";
        }
    }

    private void NextStep(bool active)
    {
        foreach (var item in _nextStep)
            item.SetActive(active);
    }

    private void CheckKey(KeyCode key, ref bool keyState, Image keyImage)
    {
        switch (keyState)
        {
            case false:
                if (Input.GetKeyDown(key))
                {
                    keyState = true;
                    keyImage.color = Color.yellow;

                    if (_w && _a && _s && _d) StartCoroutine(NextMessage());
                }
                break;

            case true:
                if (Input.GetKeyUp(key)) keyImage.color = Color.white;
                break;
        }
    }

    private IEnumerator NextMessage()
    {
        yield return new WaitForSeconds(_timeToNextMessage);
        NextStep(true);
        _mailQuest1.enabled = true;
        _radar.StatusRadar(true);
        _radar.target = _nextPos;
        Destroy(gameObject);
    }
}