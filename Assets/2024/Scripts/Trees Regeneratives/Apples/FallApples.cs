using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallApples : MonoBehaviour
{
    [SerializeField] float _timeToOff = 2.5f;
    [SerializeField] GameObject _decal;
    [SerializeField] Apple _apples;
    [SerializeField] ParticleSystem _smokeParticle;
    [SerializeField] SaplingApple _sapling;

    private Vector3[] _initialPos;
    private GameObject[] _particles;
    public bool _isFall;

    void Start()
    {
        //_initialTime = _timeToOff;

        // Obtén el número de hijos del GameObject padre
        int childCount = transform.childCount;

        // Inicializa el array de partículas con el tamaño del número de hijos
        _particles = new GameObject[childCount];

        // Inicializa el array de posiciones iniciales con el tamaño del número de hijos
        _initialPos = new Vector3[childCount];

        // Itera sobre los hijos del GameObject padre
        for (int i = 0; i < childCount; i++)
        {
            // Obtiene cada hijo y lo almacena en el array de partículas
            _particles[i] = transform.GetChild(i).gameObject;

            // Almacena la posición inicial de cada hijo
            _initialPos[i] = _particles[i].transform.position;
        }
    }


    private IEnumerator Timer()
    {
        _decal.SetActive(false);
        _apples.gameObject.SetActive(true);

        yield return new WaitForSeconds(_timeToOff);

        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].transform.position = _initialPos[i];
        }

        _sapling.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(Timer());
    }
}
