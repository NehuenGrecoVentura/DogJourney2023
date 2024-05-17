using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Rabbit : MonoBehaviour, IPick
{
    [SerializeField] Transform _cavePos;
    [SerializeField] Transform _handPos;
    [SerializeField] Transform _escapePos;

    private Vector3 _initialPos;
    private NavMeshAgent _nv;
    private Collider _myCol;
    private bool _carrotInArea = false;
    public bool rabbitPicked = false;
    private bool _escape = false;
    private Character _player;
    [SerializeField] Transform _initialPosEscape;
    private Animator _myAnim;
    private Vector3 _myPos;

    [Header("INTERACT")]
    [SerializeField] KeyCode _keyInteractive = KeyCode.F;
    [SerializeField] GameObject _iconInteract;

    private void Awake()
    {
        _nv = GetComponent<NavMeshAgent>();
        _myAnim = GetComponent<Animator>();
        _myCol = GetComponent<Collider>();

        _player = FindObjectOfType<Character>();
    }

    void Start()
    {
        _initialPos = transform.position;
        _myPos = transform.position;
        _iconInteract.SetActive(false);
    }

    private void Update()
    {
        if (rabbitPicked && !_escape)
        {
            StopAllCoroutines();
            transform.position = _handPos.position;
        }

        // Verifica si la posición actual del objeto es diferente a la posición anterior
        if (transform.position != _myPos)
        {
            // Si la posición ha cambiado, significa que el objeto se ha movido
            _myAnim.SetBool("Move", true);
        }
        else
        {
            // Si la posición no ha cambiado, el objeto está en idle
            _myAnim.SetBool("Move", false);
        }

        // Actualiza la posición anterior con la nueva posición del objeto
        _myPos = transform.position;
    }

    public void Pick()
    {
        if (Input.GetKeyDown(_keyInteractive) && _carrotInArea)
        {
            _iconInteract.SetActive(false);
            rabbitPicked = true;
            _player.rabbitPicked = true;
        }
    }

    #region HIDE

    public void Hide()
    {
        if (!_carrotInArea) StartCoroutine(HideCoroutine());
    }

    private IEnumerator HideCoroutine()
    {
        StopCoroutine(OutCoroutine());
        _myCol.enabled = false;
        yield return new WaitForSeconds(0.1f);
        _nv.SetDestination(_cavePos.position);
        _myAnim.SetBool("Move", true);
    }

    #endregion

    #region OUT

    public void Out()
    {
        if (!_carrotInArea) StartCoroutine(OutCoroutine());
    }

    private IEnumerator OutCoroutine()
    {
        StopCoroutine(HideCoroutine());
        _myCol.enabled = true;
        yield return new WaitForSeconds(0.1f);
        _nv.SetDestination(_initialPos);

    }

    public void GoToCarrot(Transform carrotPos)
    {
        StartCoroutine(GoToCarrotCoroutine(carrotPos));
    }

    private IEnumerator GoToCarrotCoroutine(Transform carrotPos)
    {
        _carrotInArea = true;
        _myCol.enabled = true;
        StopCoroutine(OutCoroutine());
        StopCoroutine(HideCoroutine());
        yield return new WaitForSeconds(0.1f);
        _nv.SetDestination(carrotPos.position);
        yield return new WaitForSeconds(1.5f);
        _iconInteract.SetActive(true);
    }

    #endregion
}