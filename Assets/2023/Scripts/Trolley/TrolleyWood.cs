using UnityEngine;

public class TrolleyWood : MonoBehaviour
{
    [SerializeField] GameObject[] _trunksAmount;
    [SerializeField] GameObject _boxUpgrade;
    private CharacterInventory _inventory;

    private void Awake()
    {
        _inventory = FindObjectOfType<CharacterInventory>();
    }

    void Start()
    {
        foreach (var trunk in _trunksAmount)
            trunk.SetActive(false);
    }

    void Update()
    {
        if (_inventory.upgradeLoot) _boxUpgrade.SetActive(true);
        else _boxUpgrade.SetActive(false);

        if (_inventory.greenTrees > 0 && _inventory.greenTrees < 20 || _inventory.purpleTrees > 0 && _inventory.purpleTrees < 20)
        {
            _trunksAmount[0].SetActive(true);
            _trunksAmount[1].SetActive(false);
            _trunksAmount[2].SetActive(false);
        }

        else if (_inventory.greenTrees >= 20 && _inventory.greenTrees < 30 || _inventory.purpleTrees >= 20 && _inventory.purpleTrees < 30)
        {
            _trunksAmount[0].SetActive(true);
            _trunksAmount[1].SetActive(true);
            _trunksAmount[2].SetActive(false);
        }

        else if (_inventory.greenTrees >= 30 || _inventory.purpleTrees >= 30)
        {
            foreach (var trunk in _trunksAmount)
                trunk.SetActive(true);
        }
    }
}
