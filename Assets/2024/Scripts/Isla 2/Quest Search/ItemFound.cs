using System.Collections;
using System.Collections.Generic;
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

    private bool _isSearching = false;

    void Start()
    {
        _iconInteract.SetActive(false);
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, _dog.transform.position);
        float invertedDist = 1 - (dist / _maxDist);
        _sensor.value = invertedDist;

        if (_isSearching && dist <= 1f)
        {
            
            StartCoroutine(DogSearch());
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
            _iconInteract.SetActive(false);
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

        Animator animDog = _dog.GetComponentInParent<Animator>();

        yield return new WaitForSeconds(3f);
        animDog.enabled = false;

        yield return new WaitForSeconds(0.1f);
        animDog.enabled = true;

        Repos();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.SetActive(true);

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
            _iconInteract.SetActive(false);
        }
            
            
    }
}