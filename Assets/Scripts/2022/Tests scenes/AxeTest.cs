using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AxeTest : MonoBehaviour
{
    [Header("INTERACTIVE CONFIG")]
    [SerializeField] KeyCode _pickButton = KeyCode.F;
    [SerializeField] KeyCode _dropButton = KeyCode.G;
    [SerializeField] GameObject _iconInteractive;
    
    [Header("AXE CONFIG")]
    [SerializeField] GameObject _axeInHand;
    [SerializeField] GameObject _axeInFloor;
    [SerializeField] GameObject _arrow;

    [Header("TEXTS CONFIG")]
    [SerializeField] TMP_Text _textQuest;
    [SerializeField] string _nextMessage;
    [SerializeField] GameObject _checkpoint;
    bool _axePicked = false;

    [Header("ICONS UI OBJETIVES")]
    [SerializeField] Image _iconAxe;
    [SerializeField] Image _iconForest;
    [SerializeField] Transform _nextLocation;
    LocationQuest _map;

    private void Awake()
    {
        _map = FindObjectOfType<LocationQuest>();
    }

    void Start()
    {
        _iconInteractive.gameObject.SetActive(false);
        _axeInHand.SetActive(false);
        _arrow.SetActive(true);
        _iconForest.gameObject.SetActive(false);
        
    }

    public void PickAxe()
    {
        _axeInHand.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            if (Input.GetKeyDown(_pickButton) && !_axePicked)
            {
                //_textQuest.text = _nextMessage;
                //_checkpoint.SetActive(true);
                //_arrow.SetActive(false);
                //_iconInteractive.gameObject.SetActive(false);
                //_axeInHand.SetActive(true);
                //_axeInFloor.transform.parent = player.transform.parent;
                //_axeInFloor.SetActive(false);
                //_axePicked = true;
                //_iconAxe.gameObject.SetActive(false);
                //_iconForest.gameObject.SetActive(true);
                //_map.target = _nextLocation;

                _textQuest.text = _nextMessage;
                _checkpoint.SetActive(true);
                _arrow.SetActive(false);
                _iconInteractive.gameObject.SetActive(false);
                _axeInHand.SetActive(true);
                _axeInFloor.transform.parent = player.transform.parent;
                _axeInFloor.SetActive(false);
                _iconAxe.gameObject.SetActive(false);
                _iconForest.gameObject.SetActive(true);
                _map.target = _nextLocation;
                _axePicked = true;
                Destroy(this);
            }

            else if (!Input.GetKeyDown(_pickButton) && !_axePicked)
                _iconInteractive.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null) _iconInteractive.SetActive(false);
    }
}