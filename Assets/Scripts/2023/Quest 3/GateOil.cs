using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GateOil : MonoBehaviour
{
    [Header("NEXT POS")]
    [SerializeField] Transform _nextLocation;
    private LocationQuest _map;

    [Header("START QUEST 3")]
    [SerializeField] TMP_Text _canvasQuestGeneral;
    [SerializeField] GameObject[] _objsQuest3;
    [SerializeField] Material[] _matPurpleTrees;

    [Header("START QUEST 4")]
    [SerializeField] GameObject[] _objsQuest4;

    [Header("START QUEST 5")]
    [SerializeField] GameObject[] _objsQuest5;

    [Header("START QUEST 6")]
    [SerializeField] GameObject[] _objsQuest6;

    [Header("OPEN DOORS")]
    [SerializeField] float _timeToOpen;
    [SerializeField] Collider[] _myCols;
    [SerializeField] Slider _slider;
    [SerializeField] GameObject _canvas;
    [SerializeField] GameObject _iconInteractive;
    private Inventory _inventory;
    private Animator _myAnim;
    private bool _isMessage = false;

    private void Awake()
    {
        _myAnim = GetComponent<Animator>();
        _map = FindObjectOfType<LocationQuest>();
        _inventory = FindObjectOfType<Inventory>();
    }

    private void Start()
    {
        _slider.value = 0;
        _canvas.SetActive(false);
        _iconInteractive.SetActive(false);
        _myAnim.enabled = false;
        StateTrees(false, 0);

        foreach (var quest3 in _objsQuest3)
            quest3.SetActive(false);

        foreach (var quest4 in _objsQuest4)
            quest4.SetActive(false);

        foreach (var quest4 in _objsQuest5)
            quest4.SetActive(false);

        foreach (var quest6 in _objsQuest6)
            quest6.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.gameObject.GetComponent<Character2022>();
        if (player != null && !_isMessage)
        {
            _objsQuest3[0].SetActive(true);
            _objsQuest3[1].SetActive(true);
            _objsQuest3[5].SetActive(true);
            _canvasQuestGeneral.text = "";
            _map.target = _nextLocation;
            _isMessage = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            if (_inventory.amountOil >= 4 && Input.GetKey(KeyCode.Space))
                StartCoroutine(Open());

            else if (_inventory.amountOil >= 4 && Input.GetKeyUp(KeyCode.Space))
            {
                StopAllCoroutines();
                _slider.value = 0;
                _canvas.SetActive(false);
                _iconInteractive.SetActive(true);
            }

            if (_inventory.amountOil >= 4 && !Input.GetKey(KeyCode.Space))
                _iconInteractive.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            _slider.value = 0;
            _canvas.SetActive(false);
            _iconInteractive.SetActive(false);
            StopAllCoroutines();
        }
    }

    public void StateTrees(bool isActive, int indexMat)
    {
        BoxCollider[] treesPurpleCols = FindObjectsOfType<BoxCollider>();
        MeshRenderer[] decals = FindObjectsOfType<MeshRenderer>();
        MeshRenderer[] materials = FindObjectsOfType<MeshRenderer>();

        string tagTree = "Purple Tree";
        string tagDecal = "Purple Decal";
        string leaves = "Leaves";

        // Colliders de los árboles purpuras
        foreach (BoxCollider tree in treesPurpleCols)
        {
            if (tree.gameObject.CompareTag(tagTree))
                tree.GetComponent<BoxCollider>().enabled = isActive;
        }

        // Decals de los árboles purpuras
        foreach (MeshRenderer decal in decals)
        {
            if (decal.gameObject.CompareTag(tagDecal))
                decal.GetComponent<MeshRenderer>().enabled = isActive;
        }

        // Materials de los árboles purpuras
        foreach (MeshRenderer leave in materials)
        {
            if (leave.gameObject.name == leaves && leave.gameObject.CompareTag(tagTree))
                leave.GetComponent<MeshRenderer>().material = _matPurpleTrees[indexMat];
        }
    }

    IEnumerator Open()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            _canvas.SetActive(true);
            _iconInteractive.SetActive(false);
            _slider.value += Time.deltaTime * 14.4f;
            yield return new WaitForSeconds(_timeToOpen);
            _inventory.amountOil = 0;
            _myAnim.enabled = true;

            foreach (var col in _myCols)
                Destroy(col);

            Destroy(_iconInteractive);
            Destroy(_canvas);
            Destroy(this);
        }
    }
}