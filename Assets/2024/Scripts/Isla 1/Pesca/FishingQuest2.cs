using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class FishingQuest2 : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;

    [Header("DIALOGUE")]
    [SerializeField, TextArea(4, 6)] string[] _lines;
    [SerializeField] TMP_Text _textLine;
    [SerializeField] TMP_Text _textName;
    [SerializeField] Button _buttonConfirm;
    private Dialogue _dialogue;

    [Header("FISHING")]
    [SerializeField] GameObject[] _objs;
    [SerializeField] Fishing _fishingMinigame;
    [SerializeField] int _totalAmount = 5;
    private FishingMinigame _fish;

    private bool _questActive = false;
    private Collider _myCol;
    private Manager _gm;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _dialogue = FindObjectOfType<Dialogue>();
        _fish = FindObjectOfType<FishingMinigame>();
        _gm = FindObjectOfType<Manager>();
    }

    private void Start()
    {
        _dialogue.gameObject.SetActive(false);
        _iconInteract.SetActive(false);
        _fishingMinigame.gameObject.SetActive(false);
        _fishingMinigame._totalAmount = _totalAmount;
    }

    private void Update()
    {
        if (_questActive && _fish.fishedPicked == _totalAmount)
        {
            print("COMPLETADO");
            StartCoroutine(Ending());
        }
    }

    private void Confirm()
    {
        _myCol.enabled = false;
        _iconInteract.SetActive(false);
        _dialogue.canTalk = false;
        _dialogue.Close();



        foreach (var item in _objs)
        {
            item.SetActive(false);
        }

        _fishingMinigame.gameObject.SetActive(true);
        _fishingMinigame.StarFishing();




        print("MISION ACEPTADA");
        _questActive = true;
    }

    private void SetDialogue()
    {
        _textName.text = "Fisherman";
        _iconInteract.SetActive(true);
        _buttonConfirm.onClick.AddListener(() => Confirm());

        for (int i = 0; i < _dialogue._lines.Length; i++)
            _dialogue._lines[i] = _lines[i];

        _dialogue.gameObject.SetActive(true);
        _dialogue.playerInRange = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled && !_questActive)
        {
            SetDialogue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _dialogue.playerInRange = false;
            _iconInteract.SetActive(false);
        }
    }

    private IEnumerator Ending()
    {
        _questActive = false;
        yield return new WaitForSeconds(1f);
        Destroy(_fishingMinigame.gameObject);

        foreach (var item in _objs)
        {
            item.SetActive(true);
        }

        











        yield return new WaitForSeconds(1.5f);



        _gm.QuestCompleted();
        Destroy(this);
    }
}