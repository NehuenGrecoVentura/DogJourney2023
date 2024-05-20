using UnityEngine;
using System.Collections;

public class TreeFall : MonoBehaviour
{
    [SerializeField] float _timeToOff = 2.5f;
    private float _initialTime;

    [SerializeField] Trunks _trunks;
    [SerializeField] ParticleSystem _smokeParticle;
    [SerializeField] SaplingTree _sapling;

    //[SerializeField] GameObject _particlesParent;
    private Vector3[] _initialPos;
    private GameObject[] _particles;
    [SerializeField] GameObject _decal;
    public bool _isFall;

    [Header("AUDIOS")]
    [SerializeField] AudioClip[] _sounds;
    private AudioSource _myAudio;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
    }

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
        _trunks.gameObject.SetActive(true);

        int random = Random.Range(0, _sounds.Length);
        AudioClip sound = _sounds[random];
        _myAudio.clip = sound;
        _myAudio.Play();

        yield return new WaitForSeconds(_timeToOff);
        //_treeComplete.gameObject.SetActive(true);

        // Restaura las posiciones iniciales de las partículas
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




    //private void Update()
    //{
    //    if (_isFall)
    //    {
    //        _timeToOff -= Time.deltaTime;

    //        if (_timeToOff <= 0)
    //        {
    //            _timeToOff = 0;
    //            _smokeParticle.Play();
    //            _trunks.gameObject.SetActive(true);
    //            _sapling.gameObject.SetActive(true);
    //            _isFall = false;
    //        }
    //    }

    //    else
    //    {
    //        _timeToOff = _initialTime;
    //        gameObject.SetActive(false);
    //    }
    //}
}