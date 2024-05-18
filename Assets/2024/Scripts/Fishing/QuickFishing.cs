using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class QuickFishing : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] GameObject _iconFish;

    [Header("SCORE")]
    [SerializeField] TMP_Text[] _score;

    [Header("CAMERAS")]
    [SerializeField] Camera _camRender;
    [SerializeField] Camera _myCam;
    private CameraOrbit _camPlayer;

    [Header("FISHING")]
    [SerializeField] ParticleSystem _bubbles;
    public int _totalAmount = 3;
    private FishingMinigame _fishing;
    private Manager _gm;
    private Character _player;
    private Collider _myCol;
    private bool _isActive = false;
    private bool _completed = false;

    [Header("MESSAGE")]
    [SerializeField] Image _fadeOut;

    [Header("POS")]
    [SerializeField] Transform _posEnter;
    [SerializeField] Transform _posExit;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();

        _fishing = FindObjectOfType<FishingMinigame>();
        _player = FindObjectOfType<Character>();
        _camPlayer = FindObjectOfType<CameraOrbit>();
        _gm = FindObjectOfType<Manager>();
    }


    private void Start()
    {
        _iconInteract.SetActive(false);
    }

    private void Update()
    {
        _score[1].text = _fishing.fishedPicked.ToString();

        if (_isActive && _fishing.fishedPicked == _totalAmount)
            StartCoroutine(FinishMiniGame());

        else if (_isActive && !_completed)
        {
            _player.speed = 0;
            _player.FreezePlayer();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && !_isActive) _iconInteract.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (Input.GetKeyDown(KeyCode.F) && !_isActive)
                StartCoroutine(StartMiniGame());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && !_isActive) _iconInteract.SetActive(false);
    }

    private IEnumerator StartMiniGame()
    {
        _completed = false;
        
        
        _iconFish.SetActive(false);
        _bubbles.Stop();
        _fishing.fishedPicked = 0;
        _fishing._textAmount = _score;
        _fadeOut.DOColor(Color.black, 1f);
        yield return new WaitForSeconds(1f);
        _fadeOut.DOColor(Color.clear, 1f);

        foreach (var item in _score)
        {
            item.gameObject.SetActive(true);
        }

        _player.gameObject.transform.position = _posEnter.position;
        _player.SetFishingMode(true);
        _player.PlayAnim("Fish");
        _camPlayer.gameObject.SetActive(false);
        _myCam.gameObject.SetActive(true);
        _player.speed = 0;
        _player.FreezePlayer();
        _isActive = true;

        Destroy(_myCol);
        Destroy(_iconInteract);
        _fishing.start = true;
        yield return new WaitForSeconds(2f);
        _fishing.Gaming = true;
        _camRender.enabled = true;
    }

    private IEnumerator FinishMiniGame()
    {
        _isActive = false;
        _completed = true;

        foreach (var item in _score)
        {
            item.gameObject.SetActive(false);
        }

        _fishing.Quit();
        _gm.levelFishing++;
        _gm.Upgrade();
        _fadeOut.DOColor(Color.black, 1f);

        yield return new WaitForSeconds(1f);
        _player.SetFishingMode(false);
        _player.gameObject.transform.position = _posExit.position;
        _player.PlayAnim("Hit");

        _fadeOut.DOColor(Color.clear, 1f);

        Destroy(_myCam.gameObject);
        _camPlayer.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        _player.speed = _player.speedAux;
        _player.DeFreezePlayer();
    }
}