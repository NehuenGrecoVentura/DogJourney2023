using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigTreasure : MonoBehaviour
{
    private List<DigTreasure> _allTreasures = new List<DigTreasure>();
    private SpawnRandom _random;
    private CharacterInventory _inventory;
    private ArchaeologistQuest1 _npc;
    private AudioSource _myAudio;
    private Collider _myCol;
    private MeshRenderer _myMesh;
    private float _initialHit;
    private bool _isLast = false;

    public float amountHit = 200f;
    [SerializeField] KeyCode _keyInteractive = KeyCode.Mouse0;
    [SerializeField] HealthBarTreasure _healthBar;
    [SerializeField] float _timeToRespawn = 5f;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _myCol = GetComponent<Collider>();
        _myMesh = GetComponent<MeshRenderer>();

        _inventory = FindObjectOfType<CharacterInventory>();
        _npc = FindObjectOfType<ArchaeologistQuest1>();
        _random = FindObjectOfType<SpawnRandom>();

        // Obtener todos los componentes DigTreasure en la escena y agregarlos a la lista
        var treasures = FindObjectsOfType<DigTreasure>();
        foreach (var treasure in treasures)
        {
            if (treasure != this) // Evitar agregar el objeto actual a la lista
            {
                _allTreasures.Add(treasure);
            }
        }
    }

    private void Start()
    {
        StartCoroutine(VerificarUnicoActivoPeriodicamente());
        _healthBar.gameObject.SetActive(false);
        _initialHit = amountHit;
    }

    private IEnumerator VerificarUnicoActivoPeriodicamente()
    {
        while (true)
        {
            VerificarSiUnicoActivo();
            yield return new WaitForSeconds(1f); // Esperar 1 segundo antes de la próxima verificación
        }
    }

    private void VerificarSiUnicoActivo()
    {
        //bool soyUnicoActivo = true;
        _isLast = true;
        

        foreach (var treasure in _allTreasures.ToArray()) // Convertimos la lista a un arreglo para evitar modificaciones mientras iteramos
        {
            if (treasure != null && treasure.gameObject.activeSelf)
            {
                //soyUnicoActivo = false;
                _isLast = false;
                break;
            }
            else
            {
                _allTreasures.Remove(treasure); // Remover objetos destruidos de la lista
            }
        }

        //if (soyUnicoActivo)
        if (_isLast)
        {
            Debug.Log(gameObject.name + "ES EL ULTIMO ACTIVO");
        }
    }

    private void TreasureLoot()
    {
        print("TESORO ENCONTRADO");
        _npc.MessageFound();
    }

    private void RandomLoot()
    {
        int randomIndex = Random.Range(0, 2);

        switch (randomIndex)
        {
            case 0:
                _inventory.seeds++;
                break;

            case 1:
                _inventory.money += 2;
                break;

            default:
                Debug.LogError("Index fuera de rango.");
                break;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _healthBar.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKey(_keyInteractive) && _inventory.shovelSelected)
        {
            if (!_myAudio.isPlaying) _myAudio.Play();

            Vector3 pos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            player.gameObject.transform.LookAt(pos);
            player.HitDig();
            amountHit--;
            _healthBar.Bar();

            if (amountHit <= 0)
            {
                amountHit = 0;
                _myAudio.Stop();
                player.isConstruct = false;
                player.DeFreezePlayer();
                if (_isLast) TreasureLoot();
                else RandomLoot();
                StartCoroutine(Respawn());
            }
        }

        else if (player != null && !Input.GetKey(_keyInteractive))
        {
            _myAudio.Stop();
            player.isConstruct = false;
            player.DeFreezePlayer();
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _myAudio.Stop();
            _healthBar.gameObject.SetActive(false);
            player.isConstruct = false;
            player.DeFreezePlayer();
        }
    }

    private IEnumerator Respawn()
    {
        _healthBar.gameObject.SetActive(false);
        _myMesh.enabled = false;
        _myCol.enabled = false;
        yield return new WaitForSeconds(_timeToRespawn);
        _random.SpawnObject(transform);
        _myMesh.enabled = true;
        _myCol.enabled = true;
        amountHit = _initialHit;
        _healthBar.Bar();
    }
}