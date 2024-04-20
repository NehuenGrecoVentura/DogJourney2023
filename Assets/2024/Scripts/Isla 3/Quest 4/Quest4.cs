using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class Quest4 : MonoBehaviour
{
    [Header("QUEST CONFIG")]
    [SerializeField] GameObject[] _canvasQuest4;
    [SerializeField] TMP_Text _textTimer;
    public float _timer = 180f;
    [SerializeField] int _maxAmount = 25;
    public bool isQuest4 = false;
    public int _initialAmount = 0;
    [SerializeField] TMP_Text _textAmount;
    
    [Header("INTERACTIVE CONFIG")]
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _buttonInteractive = KeyCode.F;

    [Header("WIN")]
    [SerializeField] GameObject[] _objsToDestroy;
    [SerializeField] GameObject[] _nextQuest;
    [SerializeField] Transform _nextPos;
    [SerializeField] GameObject _rewardMessage;
    [SerializeField] TMP_Text[] _textsReward;
    private LocationQuest _map;
    private Cheats _cheats;
    
    [Header("REFS")]
    private Inventory _inventory;
    private GManager _gm;

    private void Awake()
    {
        _inventory = FindObjectOfType<Inventory>();
        _gm = FindObjectOfType<GManager>();
        _map = FindObjectOfType<LocationQuest>();
        _cheats = FindObjectOfType<Cheats>();
    }

    private void Start()
    {
        _cheats._quest4Cheated = true;
        _iconInteractive.SetActive(false);
        _canvasQuest4[5].SetActive(true);
        _canvasQuest4[6].SetActive(false);
        _rewardMessage.SetActive(false);
    }

    public void StartTime()
    {
        StartCoroutine(Timer());
    }

    private void StageCompleted()
    {
        _canvasQuest4[2].SetActive(true);
        _canvasQuest4[4].SetActive(true);
        Destroy(_canvasQuest4[3]);
        Destroy(_canvasQuest4[5]);
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            _canvasQuest4[2].SetActive(false);
            _canvasQuest4[3].SetActive(true);
            _canvasQuest4[4].SetActive(false);

            _textAmount.text = "Reddish trunks: " + _initialAmount.ToString() + " /" + _maxAmount.ToString();

            isQuest4 = true;
            _timer -= Time.deltaTime;

            // Convierte los segundos en un objeto TimeSpan
            TimeSpan timeSpan = TimeSpan.FromSeconds(_timer);

            // Obtiene los minutos y segundos del objeto TimeSpan
            int minutes = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;

            // Formatea los minutos y segundos en formato MM:SS
            string formattedTime = string.Format("{0:00}:{1:00}", minutes, seconds);

            // Actualiza el TextMeshPro con el tiempo formateado
            _textTimer.text = formattedTime;

            if (_timer <= 0)
            {
                _timer = 0;
                _textTimer.text = "TIME: 0";
                isQuest4 = false;
                StageCompleted();
                StopAllCoroutines();
            }
        }
    }

    public void LevelCompleted()
    {
        if (_timer <= 0)
        {
            if (_initialAmount >= _maxAmount)
            {
                _rewardMessage.SetActive(true);
                _textsReward[0].text = "You won the bet."; 
                _textsReward[1].text = "+$200";
                _textsReward[1].color = Color.green;
                _inventory.amountOil++;
                _inventory.money += 200;
                _gm.LevelCompleted();

                foreach (var quest5 in _nextQuest)
                    quest5.SetActive(true);

                _map.target = _nextPos;

                foreach (var item in _objsToDestroy)
                    Destroy(item.gameObject);
            }

            else if (_initialAmount < _maxAmount)
            {
                _rewardMessage.SetActive(true);
                _textsReward[0].text = "You lose the bet.";
                _textsReward[1].text = "-$200";
                _textsReward[1].color = Color.red;
                _inventory.amountOil++;
                _inventory.money -= 200;
                if (_inventory.money <= 0)
                    _inventory.money = 0;

                _gm.LevelCompleted();

                foreach (var quest5 in _nextQuest)
                    quest5.SetActive(true);

                _map.target = _nextPos;

                foreach (var item in _objsToDestroy)
                    Destroy(item.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive))
                _iconInteractive.SetActive(true);

            else LevelCompleted();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null) _iconInteractive.SetActive(false);
    }
}