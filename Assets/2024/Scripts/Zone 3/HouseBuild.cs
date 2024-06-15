using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HouseBuild : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] KeyCode _keyBuild = KeyCode.Space;
    [SerializeField] Collider _myCol;
    [SerializeField] MeshRenderer _myMesh;

    [Header("AMOUNT")]
    [SerializeField] int _woodRequired = 10;
    [SerializeField] int _nailRequired = 10;
    [SerializeField] CharacterInventory _inventory;

    [SerializeField] Transform[] _iconsMaterials;
    private Transform _parentIconsMaterials;
    [SerializeField] GameObject _houseToBuild;

    [Header("AUDIO")]
    [SerializeField] AudioSource _myAudio;



    void Start()
    {
        foreach (Transform item in _iconsMaterials)
        {
            item.DOScale(0f, 0f);
        }

        _parentIconsMaterials = _iconsMaterials[0];
        _houseToBuild.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            foreach (Transform item in _iconsMaterials)
            {
                item.DOScale(1.5f, 0.5f);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKey(KeyCode.Space))
        {
            if (_inventory.nails >= _nailRequired && _inventory.greenTrees >= _woodRequired)
            {
                player.Build();

                foreach (Transform item in _iconsMaterials)
                {
                    item.DOScale(0f, 0.5f);
                }

                StartCoroutine(Build(player));
            }

            else
            {
                if (_inventory.nails < _nailRequired) _iconsMaterials[0].GetComponent<SpriteRenderer>().color = Color.red;
                if (_inventory.greenTrees < _woodRequired) _iconsMaterials[1].GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            foreach (Transform item in _iconsMaterials)
            {
                item.DOScale(0f, 0.5f);
            }
        }
    }

    private IEnumerator Build(Character player)
    {
        _myAudio.Play();
        _myMesh.enabled = false;
        //Destroy(_myCol);
        _myCol.enabled = false;
        Destroy(_parentIconsMaterials.gameObject);

        player.Build();

        float elapsedTime = 0f;

        while (elapsedTime < 3f)
        {
            if (elapsedTime > 0f) player.Build();

            // Incrementa el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            // Espera un corto tiempo antes de la próxima iteración
            yield return null;
        }

        player.isConstruct = false;
        player.DeFreezePlayer();
        _houseToBuild.SetActive(true);

        _inventory.greenTrees -= _woodRequired;
        _inventory.nails -= _nailRequired;
        if (_inventory.greenTrees <= 0) _inventory.greenTrees = 0;
        if (_inventory.nails <= 0) _inventory.nails = 0;

        Destroy(gameObject);
    }
}
