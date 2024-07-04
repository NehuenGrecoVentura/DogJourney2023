using UnityEngine;

public class ChairliftFinish : MonoBehaviour
{
    [SerializeField] Dog _dog;
    [SerializeField] Chairlift _chairliftToHide;
    [SerializeField] Transform[] _waypoints;
    [SerializeField] Transform _posPlayer;
    [SerializeField] Transform _posDog;
    [SerializeField] float _speed;
    private Vector3 _initialPos;
    private int _index = 0;
    private Character _player;

    private void Awake()
    {
        _player = FindObjectOfType<Character>();
    }

    private void Start()
    {
        _initialPos = transform.position;
        transform.position = _waypoints[0].position;
    }

    void Update()
    {
        _chairliftToHide.gameObject.SetActive(false);
        if (_index < _waypoints.Length)
        {
            Vector3 targetPosition = _waypoints[_index].position;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
            if (transform.position == targetPosition) _index++;
        }
    }

    public void Off()
    {
        transform.position = _initialPos;
        //_index = 0;
        _chairliftToHide.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _player.transform.parent = _posPlayer.parent;
        _player.transform.position = _posPlayer.position;
        _player.transform.localRotation = Quaternion.Euler(90, 0, 0);
        _dog.transform.parent = _posDog.parent;
        _dog.gameObject.transform.position = _posDog.position;
        _dog.transform.localRotation = Quaternion.Euler(90, 360, 0);
    }
}