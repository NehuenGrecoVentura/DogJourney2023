using System.Collections;
using UnityEngine;

public class TreeStump : MonoBehaviour
{
    [SerializeField] float _timeActive = 2.5f;
    [SerializeField] TreeChopping _treeComplete;
    [SerializeField] GameObject _particlesParent;
    [SerializeField] Trunks _trunks;
    [SerializeField] GameObject _decal;
    [SerializeField] SaplingTree _sapling;

    private Vector3[] _initialPos;
    private GameObject[] _particles;

    private void Start()
    {
        // Obt�n el n�mero de hijos del GameObject padre
        int childCount = _particlesParent.transform.childCount;

        // Inicializa el array de part�culas con el tama�o del n�mero de hijos
        _particles = new GameObject[childCount];

        // Inicializa el array de posiciones iniciales con el tama�o del n�mero de hijos
        _initialPos = new Vector3[childCount];

        // Itera sobre los hijos del GameObject padre
        for (int i = 0; i < childCount; i++)
        {
            // Obtiene cada hijo y lo almacena en el array de part�culas
            _particles[i] = _particlesParent.transform.GetChild(i).gameObject;

            // Almacena la posici�n inicial de cada hijo
            _initialPos[i] = _particles[i].transform.position;
        }
    }

    private IEnumerator Timer()
    {
        _decal.SetActive(false);

        yield return new WaitForSeconds(_timeActive);
        //_treeComplete.gameObject.SetActive(true);

        // Restaura las posiciones iniciales de las part�culas
        for (int i = 0; i < _particles.Length; i++)
        {
            _particles[i].transform.position = _initialPos[i];
        }

        _sapling.gameObject.SetActive(true);
        _trunks.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(Timer());
    }
}