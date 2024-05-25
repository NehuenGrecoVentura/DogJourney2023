using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemFound : MonoBehaviour
{
    [SerializeField] QuestSearch _quest;

    [Header("INTERACT")]
    [SerializeField] GameObject _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.Q;
    [SerializeField] Collider _myCol;

    [Header("REPOS")]
    [SerializeField] Transform[] _repos;
    private int _index = 0;

    [Header("SENSOR")]
    [SerializeField] Dog _dog;
    [SerializeField] DogBall _dogBall;
    [SerializeField] Slider _sensor;
    [SerializeField] float _maxDist = 10f;

    [Header("CAM FOCUS")]
    [SerializeField] Camera _camFocus;
    [SerializeField] CameraOrbit _camPlayer;

    private bool _isSearching = false;

    void Start()
    {
        _iconInteract.transform.DOScale(0f, 0f);
        _sensor.gameObject.SetActive(false);
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
    }

    public void Repos()
    {
        if (_repos.Length > 0)
        {
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


        ChangeCam(true, false);
        Animator animDog = _dog.GetComponentInParent<Animator>();

        yield return new WaitForSeconds(3f);
        animDog.enabled = false;

        yield return new WaitForSeconds(0.1f);
        animDog.enabled = true;

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