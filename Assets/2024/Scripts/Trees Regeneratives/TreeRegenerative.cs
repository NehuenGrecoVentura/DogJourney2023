using UnityEngine;

public class TreeRegenerative : MonoBehaviour
{
    private Character _player;
    private HouseQuest4 _quest4;
    private DoTweenManager _doTween;

    private AudioSource _myAudio;
    [SerializeField] AudioClip _soundHit;

    public float amountHit = 100f;
    [HideInInspector] public float initialAmount;

    [SerializeField] KeyCode _inputInteractive = KeyCode.Mouse0;

    [SerializeField] TreeFall _treeFall;
    [SerializeField] Trunks _trunks;
    [SerializeField] SaplingTree _sapling;

    [SerializeField] GameObject _decal;
    [SerializeField] HitBar _hitBar;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _player = FindObjectOfType<Character>();
        _quest4 = FindObjectOfType<HouseQuest4>();
        _doTween = FindObjectOfType<DoTweenManager>();
    }

    private void Start()
    {
        _treeFall.gameObject.SetActive(false);
        _trunks.gameObject.SetActive(false);
        _sapling.gameObject.SetActive(false);
        _hitBar.gameObject.SetActive(false);
        initialAmount = amountHit;
    }

    private void FocusToTree()
    {
        _decal.SetActive(true);
        Vector3 pos = new Vector3(transform.position.x, _player.transform.position.y, transform.position.z);
        _player.transform.LookAt(pos);
    }


    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _hitBar.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();

        if (player != null)
        {
            FocusToTree();

            if (!Input.GetKey(_inputInteractive)) 
                _hitBar.gameObject.SetActive(true);

            else if (Input.GetKey(_inputInteractive) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            {
                player.HitTree();
            }

            else
            {
                _doTween.Shake(gameObject.transform);
                player.HitTree();
                amountHit--;
                if (!_myAudio.isPlaying) _myAudio.PlayOneShot(_soundHit);
                _hitBar.Bar();
            }

            if (amountHit <= 0)
            {
                amountHit = 0;
                _hitBar.gameObject.SetActive(false);
                _decal.SetActive(false);
                _treeFall.gameObject.SetActive(true);
                _treeFall._isFall = true;
                gameObject.SetActive(false);
            }

            if (amountHit <= 0 && _quest4.quest4Active) _quest4.amount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _decal.SetActive(false);
            _hitBar.gameObject.SetActive(false);
        }  
    }

    public void RestartAmount()
    {
        amountHit = initialAmount;
        _hitBar.Bar();
    }
}