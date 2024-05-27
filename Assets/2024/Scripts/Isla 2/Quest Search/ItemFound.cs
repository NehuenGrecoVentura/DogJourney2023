using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ItemFound : MonoBehaviour
{
    [SerializeField] QuestSearch _quest;
    [SerializeField] Character _player;

    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.Q;
    [SerializeField] Collider _myCol;
    [SerializeField] ParticleSystemRenderer _smoke;
    private bool _isSearching = false;

    [Header("REPOS")]
    [SerializeField] Transform[] _repos;
    private int _index = 0;

    [Header("SENSOR")]
    [SerializeField] Dog _dog;
    [SerializeField] DogBall _dogBall;
    [SerializeField] Slider _sensor;
    [SerializeField] float _maxDist = 10f;
    [SerializeField] TMP_Text _textTooFar;

    [Header("CAM FOCUS")]
    [SerializeField] Camera _camFocus;
    [SerializeField] CameraOrbit _camPlayer;

    [Header("AUDIOS")]
    [SerializeField] AudioSource _myAudio;
    [SerializeField] AudioClip _soundSearch;
    [SerializeField] AudioClip _soundItem;

    void Start()
    {
        _iconInteract.transform.DOScale(0f, 0f);
        _sensor.gameObject.SetActive(false);
        _textTooFar.gameObject.SetActive(false);
        _smoke.enabled = false;
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, _dog.transform.position);
        float invertedDist = 1 - (dist / _maxDist);
        _sensor.value = invertedDist;

        //if (_isSearching && dist <= 1f)
        //{

        //    StartCoroutine(DogSearch());
        //}

        if (_isSearching && dist <= 3f)
        {
            _iconInteract.transform.DOScale(1f, 0.5f);
        }


        

        float distPlayer = Vector3.Distance(transform.position, _player.transform.position);
        if (Input.GetKeyDown(KeyCode.Q) && _quest._found > 0)
        {
            if (distPlayer >= 100) _textTooFar.gameObject.SetActive(true);
            else _textTooFar.gameObject.SetActive(false);

        }

        else return;



    }

    public void Repos()
    {
        if (_repos.Length > 0)
        {
            _myAudio.PlayOneShot(_soundItem);
            _index = (_index + 1) % _repos.Length;
            Transform nextPos = _repos[_index];
            transform.position = nextPos.position;
            _quest.AddFound(gameObject);
            ChangeCam(false, true);
            _iconInteract.transform.DOScale(0f, 0.5f);
            _myCol.enabled = true;
        }

        else return;
    }

    private IEnumerator DogSearch()
    {
        _isSearching = false;
        _myCol.enabled = false;
        _dog.Stop();
        _dog.Search();
        _smoke.enabled = true;
        _myAudio.PlayOneShot(_soundSearch);
        _player.enabled = false;

        ChangeCam(true, false);
        Animator animDog = _dog.GetComponentInParent<Animator>();

        yield return new WaitForSeconds(3f);
        animDog.enabled = false;
        _smoke.enabled = false;

        yield return new WaitForSeconds(0.1f);
        animDog.enabled = true;
        _myAudio.Stop();
        _player.enabled = true;

        Repos();
    }

    public void ChangeCam(bool camFocus, bool camPlayer)
    {
        if(_quest._found >= 1)
        {
            _camFocus.gameObject.SetActive(camFocus);
            _camPlayer.gameObject.SetActive(camPlayer);
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.transform.DOScale(1f, 0.5f);

        var dog = other.GetComponent<Dog>();
        if(dog != null && _isSearching)
        {
            StartCoroutine(DogSearch());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(_keyInteract))
        {
            _dogBall.transform.position = transform.position;
            _dog.OrderGo();
            _isSearching = true;
        }
        //Repos();
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _iconInteract.transform.DOScale(0f, 0.5f);
        }
    }
}