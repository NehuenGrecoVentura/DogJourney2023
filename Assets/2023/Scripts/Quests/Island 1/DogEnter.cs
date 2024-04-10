using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogEnter : MonoBehaviour
{
    [SerializeField] Transform _posSpawn;
    [SerializeField] GameObject _prefab;
    [SerializeField] KeyCode _keyInteract = KeyCode.Q;

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            if (Input.GetKeyDown(_keyInteract))
            {
                Instantiate(_prefab, _posSpawn);
            }
        }
    }
}
