using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BuilderManager : MonoBehaviour
{
    [SerializeField] GameObject _objToBuild;
    [SerializeField] GameObject[] _objToDestroy;
    [SerializeField] GameObject _iconsMaterials;

    [Header("ITEMS")]
    public int _amountItem1;
    public int _amountItem2;
    [SerializeField] SpriteRenderer _item1;
    [SerializeField] SpriteRenderer _item2;

    [SerializeField] KeyCode _keyInteractive = KeyCode.Space;

    public float _timeToBuild = 3f;

    [Header("CINEMATIC")]
    [SerializeField] GameObject _cinematic;
    [SerializeField] Camera _camPlayer;
    private MeshRenderer _myRender;
    private Collider _myCol;
    private Character _player;
    private bool _playCinematic = false;
    public CharacterInventory _inventory;

    [Header("RADAR")]
    [SerializeField] Transform _posRadar;
    private LocationQuest _radar;

    [Header("AUDIO")]
    [SerializeField] AudioClip _audioBuild;
    private AudioSource _myAudio;

    [SerializeField] QuestUI _questUI;

    void Start()
    {
        _myCol = GetComponent<Collider>();
        _myRender = GetComponent<MeshRenderer>();
        _myAudio = GetComponent<AudioSource>();

        _player = FindObjectOfType<Character>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _radar = FindObjectOfType<LocationQuest>();

        _iconsMaterials.SetActive(false);
        _objToBuild.SetActive(false);
        _cinematic.SetActive(false);

        _item1.transform.localScale = Vector3.zero;
        _item2.transform.localScale = Vector3.zero;
    }
    private void Update()
    {
        if (_playCinematic)
        {
            _player.PlayAnim("Build");
            _player.gameObject.transform.LookAt(_objToBuild.transform.position);
        } 
    }
    private IEnumerator Construct()
    {
        _questUI.UIStatus(false);
        _myAudio.PlayOneShot(_audioBuild);
        _radar.StatusRadar(false);
        _iconsMaterials.SetActive(false);
        _playCinematic = true;
        Destroy(_myRender);
        Destroy(_myCol);
        _player.FreezePlayer();       
        _player.speed = 0;
        _player.PlayAnim("Build");
        _camPlayer.gameObject.SetActive(false);
        _cinematic.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        _objToBuild.SetActive(true);

        //foreach (var item in _objToDestroy)
        //{
        //    Destroy(item);
        //}

        if (_objToDestroy != null && _objToDestroy.Length > 0)
        {
            foreach (var item in _objToDestroy)
            {
                if (item != null) Destroy(item);
            }
        }

        _playCinematic = false;
        yield return new WaitForSeconds(_timeToBuild);
        _camPlayer.gameObject.SetActive(true);
        _radar.StatusRadar(true);

        if (gameObject.name == "Build Stairs") _questUI.UIStatus(true);
        else _questUI.UIStatus(false);

        _radar.target = _posRadar;
        //if (gameObject.name == "Build Stairs") //_canvasQuest.SetActive(true);
        _player.DeFreezePlayer();
        _player.speed = _player.speedAux;
        Destroy(_iconsMaterials);
        Destroy(_cinematic);
        Destroy(gameObject);
    }

    public void RemoveItemsBridge1()
    {
        Destroy(_myCol);
        _inventory.nails -= _amountItem1;
        _inventory.greenTrees -= _amountItem2;
    }

    public void RemoveItemsBridge2()
    {
        Destroy(_myCol);
        _inventory.greenTrees -= _amountItem1;
        _inventory.ropes -= _amountItem2;
    }

    public void BuildObj(int item1, int item2)
    {
        if (Input.GetKey(_keyInteractive))
        {
            if (item1 < _amountItem1) _item1.color = Color.red;
            if (item2 < _amountItem2) _item2.color = Color.red;

            else if (item1 >= _amountItem1 && item2 >= _amountItem2 && !_player.isConstruct)
            {
                if (gameObject.name == "Icon Bridge 1" || gameObject.name == "Build Stairs") RemoveItemsBridge1();
                if (gameObject.name == "Icon Bridge 2") RemoveItemsBridge2();

                StartCoroutine(Construct());
                _player.isConstruct = true;
            }
        }

        else
        {
            _iconsMaterials.SetActive(true);
            _item1.transform.DOScale(1.5f, 0.5f);
            _item2.transform.DOScale(1.5f, 0.5f);
            _item1.color = Color.white;
            _item2.color = Color.white;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _item1.material.color = Color.white;
            _item2.material.color = Color.white;
            StartCoroutine(ExitCoroutine());
            _player.isConstruct = false;
        }   
    }

    private IEnumerator ExitCoroutine()
    {
        _item1.transform.DOScale(0f, 0.5f);
        _item2.transform.DOScale(0f, 0.5f);
        yield return new WaitForSeconds(0.6f);
        _iconsMaterials.SetActive(false);
    }
}