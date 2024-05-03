using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DigTreasure : MonoBehaviour
{
    private List<DigTreasure> _allTreasures = new List<DigTreasure>();
    private CharacterInventory _inventory;
    private ArchaeologistQuest1 _npc;
    private bool _isLast = false;

    [SerializeField] float _amountHit = 200f;
    [SerializeField] KeyCode _keyInteractive = KeyCode.Mouse0;

    private void Awake()
    {
        _inventory = FindObjectOfType<CharacterInventory>();
        _npc = FindObjectOfType<ArchaeologistQuest1>();

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


    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKey(_keyInteractive))
        {
            //if (!_myAudio.isPlaying) _myAudio.Play();

            Vector3 pos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
            player.gameObject.transform.LookAt(pos);
            player.HitTree();
            _amountHit--;

            if (_amountHit <= 0)
            {
                _amountHit = 0;
                if (_isLast) TreasureLoot();
                else RandomLoot();
                Destroy(gameObject);
            }
        }
    }
}