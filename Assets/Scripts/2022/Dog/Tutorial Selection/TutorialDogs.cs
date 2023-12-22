using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialDogs : MonoBehaviour
{
    [SerializeField] GameObject _messagePressEnter;
    [SerializeField] GameObject _messageCallAllDogs;
    [SerializeField] float _countDown;
    float _initialCount;
    [Header("BUTTONS TUTORIAL")]
    [SerializeField] GameObject _messagePressButtons;
    [SerializeField] Image _button1;
    [SerializeField] Image _button2;
    [SerializeField] Image _buttonR;
    public bool finish = false;
    [SerializeField] TMP_Text _text;
    [SerializeField] TMP_Text _textR;
    [SerializeField] bool _1, _2, _enter = false;
    [SerializeField] bool _tutorialR = false;
    [SerializeField] Upgrade _upgradeAddDog;

    void Start()
    {
        finish = false;
        _initialCount = _countDown;
        _messagePressButtons.SetActive(false);
        _messageCallAllDogs.SetActive(false);
        _messagePressEnter.SetActive(true);
    }

    void Update()
    {
        TutorialSelectDogs();
        TutorialCallDogs();
    }

    void TutorialSelectDogs()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // SI TOCO ENTER SE ELIMINA EL MENSAJE DE PRESS ENTER Y SE ACTIVA EL SIGUIENTE MENSAJE
        {
            _enter = true;
            Destroy(_messagePressEnter);
            _messagePressButtons.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && _enter)
        {
            _1 = true;
            _button1.color = Color.yellow;
        }

        else if (Input.GetKeyUp(KeyCode.Alpha1) && _enter)
            _button1.color = Color.white;

        if (Input.GetKeyDown(KeyCode.Alpha2) && _enter)
        {
            _2 = true;
            _button2.color = Color.yellow;
        }

        else if (Input.GetKeyUp(KeyCode.Alpha2) && _enter)
            _button2.color = Color.white;

        if (_1 && _2 && !finish && !_tutorialR && _enter)
        {
            _text.text = "GOOD";
            _text.color = Color.green;
            _button1.color = Color.green;
            _button2.color = Color.green;
            _countDown -= Time.deltaTime;
            if (_countDown <= 0)
            {
                _countDown = 0;
                Destroy(_messagePressButtons);
                _messageCallAllDogs.SetActive(true);
                _countDown = _initialCount;
                _tutorialR = true;
            }
        }
    }

    public void TutorialCallDogs()
    {
        if (Input.GetKeyDown(KeyCode.R) && _enter) finish = true;

        if (finish)
        {
            _textR.text = "GOOD";
            _textR.color = Color.green;
            _buttonR.color = Color.green;
            _countDown -= Time.deltaTime;
            if (_countDown <= 0)
            {
                _countDown = 0;
                _upgradeAddDog.activeTutorialDogs = false;
                Destroy(gameObject);
            }
        }
    }
}