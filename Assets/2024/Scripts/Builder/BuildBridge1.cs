using UnityEngine;
using System.Collections;

public class BuildBridge1 : BuilderManager
{
    private CharacterInventory _inventory;
    private TestCinematic _cinematic;
    private Character _player;
    [SerializeField] GameObject _cinematicBridge;
    [SerializeField] int _amountNails = 20;
    [SerializeField] int _amountGreenTrees = 20;

    private void Awake()
    {
        _cinematic = GetComponent<TestCinematic>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _player = FindObjectOfType<Character>();
    }

    private void Start()
    {
        _cinematicBridge.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            BuildObj(_inventory.nails, _inventory.greenTrees);
        }
    }
}
