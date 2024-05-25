using System.Collections.Generic;
using UnityEngine;

public class PoolApple : MonoBehaviour
{
    [SerializeField] GameObject _applePrefab;
    [SerializeField] int _poolSize = 10;
    [SerializeField] List<GameObject> _appleList;

    private static PoolApple instance;
    public static PoolApple Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        AddApplesToPool(_poolSize);
    }

    private void AddApplesToPool(int amount)
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject apple = Instantiate(_applePrefab);
            apple.SetActive(false);
            _appleList.Add(apple);
            apple.transform.parent = transform;
        }
    }

    public GameObject RequestApple()
    {
        for (int i = 0; i < _appleList.Count; i++)
        {
            if (!_appleList[i].activeSelf)
            {
                _appleList[i].SetActive(true);
                return _appleList[i];
            }
        }

        AddApplesToPool(1);
        _appleList[_appleList.Count - 1].SetActive(true);
        return _appleList[_appleList.Count - 1];

    }
}