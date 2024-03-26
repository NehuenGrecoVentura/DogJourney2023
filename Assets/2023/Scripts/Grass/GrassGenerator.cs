using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassGenerator : MonoBehaviour
{
    [SerializeField] GameObject _grassPrefab;
    [SerializeField] int _size = 20;

    void Start()
    {
        for (int z = -_size; z <= _size; z++)
        {
            for (int x = 0; x < _size; x++)
            {
                Vector3 pos = new Vector3(x / 4.0f + Random.Range(-0.25f, 0.25f), z / 4.0f);
                GameObject grass = Instantiate(_grassPrefab, transform.position + pos, Quaternion.identity);
                //grass.transform.localScale = new Vector3(1, Random.Range(0.8f, 1.2f), 1);
                grass.transform.parent = gameObject.transform.parent;
            }
        }   
    }
}
