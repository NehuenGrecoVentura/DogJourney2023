using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPark : MonoBehaviour
{
    [SerializeField] Transform[] _wp;
    [SerializeField] float _speed = 2f;
    private int _index = 0;

    void Update()
    {
        // Comprueba si hay puntos de referencia
        if (_wp.Length == 0)
            return;

        // Mueve el objeto hacia el punto de referencia actual
        transform.position = Vector3.MoveTowards(transform.position, _wp[_index].position, _speed * Time.deltaTime);

        // Mira hacia el punto de referencia actual
        Quaternion targetRotation = Quaternion.LookRotation(_wp[_index].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);

        // Comprueba si el objeto ha alcanzado el punto de referencia actual
        if (Vector3.Distance(transform.position, _wp[_index].position) < 0.1f)
        {
            // Avanza al siguiente punto de referencia
            _index = (_index + 1) % _wp.Length;
        }
    }
}