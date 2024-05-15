using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BuildTable : MonoBehaviour
{
    [SerializeField] private KeyCode _keyInteract = KeyCode.Space;
    [SerializeField] GameObject _tablePrefab;
    [SerializeField] GameObject _icon;
    [SerializeField] SpriteRenderer _iconWood;
    [SerializeField] SpriteRenderer _iconNail;
    [SerializeField] int _totalNails = 10;
    [SerializeField] int _totalWoods = 10;
    private CharacterInventory _inventory;
    private Character _player;
    private Collider _myCol;

    [Header("NEXT QUEST")]
    [SerializeField] RectTransform _boxTAB;
    [SerializeField] GameObject _npcFish;
    private LocationQuest _radar;
    private Manager _gm;

    private AudioSource _myAudio;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _myCol = GetComponent<Collider>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _player = FindObjectOfType<Character>();
        _gm = FindObjectOfType<Manager>();
        _radar = FindObjectOfType<LocationQuest>();
    }

    private void Start()
    {
        _npcFish.SetActive(false);
        _iconWood.gameObject.SetActive(false);
        _iconNail.gameObject.SetActive(false);
        _tablePrefab.SetActive(false);
        _boxTAB.gameObject.SetActive(false);
        _myCol.enabled = false;
        _myAudio.Stop();
    }

    public void CheatSkip()
    {
        _player.isConstruct = false;
        _player.speed = _player.speedAux;
        _player.DeFreezePlayer();
        _tablePrefab.SetActive(true);
        Destroy(_iconNail.gameObject);
        Destroy(_iconWood.gameObject);
        Destroy(_icon);
        _inventory.greenTrees -= _totalWoods;
        _inventory.nails -= _totalNails;
        if (_inventory.greenTrees <= 0) _inventory.greenTrees = 0;
        if (_inventory.nails <= 0) _inventory.nails = 0;
        Destroy(_myCol);
        _gm.QuestCompleted();
        _npcFish.SetActive(true);
        _radar.target = _npcFish.transform;
        Destroy(_boxTAB.transform.parent.gameObject);
        Destroy(this);
    }

    private IEnumerator Build()
    {
        _myAudio.Play();

        Destroy(_icon);
        Destroy(_myCol);
        Destroy(_iconWood.gameObject);
        Destroy(_iconNail.gameObject);

        //_iconWood.gameObject.SetActive(false);
        //_iconNail.gameObject.SetActive(false);
        _player.isConstruct = true;
        _player.FreezePlayer();

        float elapsedTime = 0f;

        while (elapsedTime < 3f)
        {
            if (elapsedTime > 0f) _player.PlayAnim("Build");

            // Incrementa el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            // Espera un corto tiempo antes de la próxima iteración
            yield return null;
        }

        // Destruye el objeto después de tres segundos
        //Construct();

        _player.isConstruct = false;
        _player.DeFreezePlayer();
        _tablePrefab.SetActive(true);
        //Destroy(_iconNail.gameObject);
        //Destroy(_iconWood.gameObject);
        //Destroy(_icon);
        _inventory.greenTrees -= _totalWoods;
        _inventory.nails -= _totalNails;
        if (_inventory.greenTrees <= 0) _inventory.greenTrees = 0;
        if (_inventory.nails <= 0) _inventory.nails = 0;
        //Destroy(_myCol);
        _gm.QuestCompleted();
        _boxTAB.gameObject.SetActive(true);
        _npcFish.SetActive(true);
        _radar.target = _npcFish.gameObject.transform;
        yield return new WaitForSeconds(3f);
        Destroy(_boxTAB.transform.parent.gameObject);
        Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _iconWood.gameObject.SetActive(true);
            _iconNail.gameObject.SetActive(true);
            _iconWood.transform.DOScale(1.5f, 0.5f);
            _iconNail.transform.DOScale(1.5f, 0.5f);
            _iconWood.color = Color.white;
            _iconNail.color = Color.white;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (Input.GetKeyDown(_keyInteract))
            {
                if (_inventory.nails >= _totalNails && _inventory.greenTrees >= _totalWoods)
                {
                    player.PlayAnim("Build");
                    _iconWood.gameObject.SetActive(false);
                    _iconNail.gameObject.SetActive(false);
                    StartCoroutine(Build());
                }
                      
                else
                {
                    if (_inventory.nails < _totalNails) _iconNail.color = Color.red;
                    if (_inventory.greenTrees < _totalWoods) _iconWood.color = Color.red;                    
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _iconWood.gameObject.SetActive(false);
            _iconNail.gameObject.SetActive(false);
            _iconWood.transform.DOScale(0f, 0.5f);
            _iconNail.transform.DOScale(0f, 0.5f);
            _iconWood.color = Color.white;
            _iconNail.color = Color.white;
        }
    }
}