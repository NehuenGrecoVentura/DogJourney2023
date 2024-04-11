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
    private Manager _gm;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _player = FindObjectOfType<Character>();
        _gm = FindObjectOfType<Manager>();
    }

    private void Start()
    {
        _iconWood.gameObject.SetActive(false);
        _iconNail.gameObject.SetActive(false);
        _tablePrefab.SetActive(false);
        _myCol.enabled = false;
    }

    private void Construct()
    {
        _player.isConstruct = false;
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _tablePrefab.SetActive(true);
        Destroy(_iconNail.gameObject);
        Destroy(_iconWood.gameObject);
        Destroy(_icon);
        _inventory.greenTrees -= _totalWoods;
        _inventory.nails -= _totalNails;
        Destroy(_myCol);
        _gm.QuestCompleted();
        Destroy(this);
    }

    private IEnumerator Build()
    {
        Destroy(_icon);
        _iconWood.gameObject.SetActive(false);
        _iconNail.gameObject.SetActive(false);
        _player.isConstruct = true;
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);

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
        Construct();
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