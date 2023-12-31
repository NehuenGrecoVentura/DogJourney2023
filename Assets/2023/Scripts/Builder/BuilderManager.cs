using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuilderManager : MonoBehaviour
{
    [SerializeField] GameObject _objToBuild;
    [SerializeField] GameObject _objToDestroy;
    [SerializeField] GameObject _iconsMaterials;

    [Header("ITEMS")]
    public int _amountItem1;
    public int _amountItem2;
    [SerializeField] MeshRenderer _item1;
    [SerializeField] MeshRenderer _item2;


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
    private bool _isBuilding = false;
    [SerializeField] GameObject _canvasQuest;

    [Header("RADAR")]
    [SerializeField] Transform _posRadar;
    private LocationQuest _radar;

    [Header("AUDIO")]
    [SerializeField] AudioClip _audioBuild;
    private AudioSource _myAudio;

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
        _myAudio.PlayOneShot(_audioBuild);
        _canvasQuest.SetActive(false);
        _radar.StatusRadar(false);
        _iconsMaterials.SetActive(false);
        _playCinematic = true;
        Destroy(_myRender);
        Destroy(_myCol);
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);       
        _player.speed = 0;
        _player.PlayAnim("Build");
        _camPlayer.gameObject.SetActive(false);
        _cinematic.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        _objToBuild.SetActive(true);
        Destroy(_objToDestroy);
        _playCinematic = false;
        yield return new WaitForSeconds(_timeToBuild);
        _camPlayer.gameObject.SetActive(true);
        _radar.StatusRadar(true);
        _radar.target = _posRadar;
        if (gameObject.name == "Build Stairs") _canvasQuest.SetActive(true);
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
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

    public void  BuildObj(int item1, int item2)
    {
        if (Input.GetKey(_keyInteractive))
        {
            if (item1 < _amountItem1) _item1.material.color = Color.red;
            if (item2 < _amountItem2) _item2.material.color = Color.red;

            else if (item1 >= _amountItem1 && item2 >= _amountItem2 && !_isBuilding)
            {
                if (gameObject.name == "Icon Bridge 1" || gameObject.name == "Build Stairs") RemoveItemsBridge1();
                if (gameObject.name == "Icon Bridge 2") RemoveItemsBridge2();

                StartCoroutine(Construct());
                _isBuilding = true;
            }
        }

        else
        {
            _iconsMaterials.SetActive(true);
            _item1.material.color = Color.white;
            _item2.material.color = Color.white;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _iconsMaterials.SetActive(false);
            _isBuilding = false;
        }   
    }
}