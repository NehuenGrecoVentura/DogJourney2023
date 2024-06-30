using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;

public class Chairlift : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] Transform _iconInteract;
    [SerializeField] KeyCode _keyInteract = KeyCode.F;
    [SerializeField] Collider _myCol;
    
    [Header("DOG")]
    [SerializeField] NavMeshAgent _dog;
    [SerializeField] NavMeshAgent _trolley;
    [SerializeField] DogBall _dogBall;
    [SerializeField] LineRenderer _lineDog;
    
    [Header("CAMS")]
    [SerializeField] Camera _camCinematic;
    [SerializeField] CameraOrbit _camPlayer;

    [Header("CINEMATIC")]
    [SerializeField] GameObject _cinematic;
    [SerializeField] Image _fadeOut;

    [Header("MOVE")]
    [SerializeField] GameObject _myLine;
    [SerializeField] GameObject _lineMachine;
    [SerializeField] Transform[] _waypoints;
    [SerializeField] Transform _posExit;
    [SerializeField] Transform _posExitDog;
    [SerializeField] Transform _posSit;
    [SerializeField] Transform _posSitDog;
    [SerializeField] Transform _parentPlayer;
    [SerializeField] float _speed = 5f;
    private int _index = 0;
    private bool _isActive = false;
    private Vector3 _initialPos;
    private bool _firstContact =  true;

    void Start()
    {
        _camCinematic.gameObject.SetActive(false);
        _cinematic.SetActive(false);
        _fadeOut.DOColor(Color.clear, 0f);
        _iconInteract.DOScale(0f, 0f);
        _initialPos = transform.position;
        _myLine.SetActive(false);
        _lineMachine.SetActive(true);
    }

    private void Update()
    {
        if (_isActive)
        {
            if (_waypoints.Length == 0) return;
            Vector3 direction = _waypoints[_index].position - transform.position;
            direction.Normalize();
            transform.Translate(direction * _speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, _waypoints[_index].position) < 0.1f)
            {
                _index++;
                if (_index >= _waypoints.Length) _index = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled) _iconInteract.DOScale(0.1f, 0.5f);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(_keyInteract) && _myCol.enabled)  
            StartCoroutine(ActiveChair(player));
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled) _iconInteract.DOScale(0f, 0.5f);
    }

    private IEnumerator ActiveChair(Character player)
    {
        NPCZone3 npcZone3 = FindObjectOfType<NPCZone3>();
        LocationQuest radar = FindObjectOfType<LocationQuest>();
        FirstCharilift first = FindObjectOfType<FirstCharilift>();
        if(gameObject.name == "Chairlift Zone 1" && _firstContact)
        {
            radar.StatusRadar(true);
            radar.target = npcZone3.gameObject.transform;
            Destroy(first);
            _firstContact = false;
        }

        _fadeOut.DOColor(Color.black, 1.5f);
        _iconInteract.DOScale(0, 0.5f);
        _myCol.enabled = false;

        _dog.enabled = false;
        _trolley.enabled = false;
        _lineDog.enabled = false;
        
        yield return new WaitForSeconds(2f);
        _fadeOut.DOColor(Color.clear, 1.5f);
        _myLine.SetActive(true);
        _lineMachine.SetActive(false);
        _camPlayer.gameObject.SetActive(false);
        _camCinematic.gameObject.SetActive(true);
        _cinematic.SetActive(true);
        
        player.transform.parent = _posSit.parent;
        player.transform.position = _posSit.position;
        player.transform.localRotation = Quaternion.Euler(90,0, 0);
        player.SitChair();

        _dogBall.transform.parent = _posSitDog.parent;
        _dogBall.transform.position = _posSitDog.position;
        _dog.transform.parent = _posSitDog.parent;
        _dog.gameObject.transform.position = _posSitDog.position;
        _dog.transform.localRotation = Quaternion.Euler(90, 360, 0);
        _trolley.gameObject.SetActive(false);

        _isActive = true;

        yield return new WaitForSeconds(5f);
        _fadeOut.DOColor(Color.black, 1.5f);

        yield return new WaitForSeconds(2f);
        _isActive = false;
        _myLine.SetActive(false);
        _lineMachine.SetActive(true);
        transform.position = _initialPos;
        _fadeOut.DOColor(Color.clear, 1.5f);
        _camPlayer.gameObject.SetActive(true);
        _camCinematic.gameObject.SetActive(false);
        _cinematic.SetActive(false);
        
        player.transform.parent = _parentPlayer;
        player.transform.position = _posExit.position;
        player.isConstruct = false;
        player.DeFreezePlayer();

        _dogBall.transform.parent = _parentPlayer;
        _dogBall.transform.position = _posExitDog.position;
        _dog.transform.parent = _parentPlayer;
        _dog.gameObject.transform.position = _posExitDog.position;

        _trolley.gameObject.SetActive(true);
        _trolley.gameObject.transform.position = _posExitDog.position;

        _lineDog.enabled = true;
        _dog.enabled = true;
        _trolley.enabled = true;
        _myCol.enabled = true;
    }
}