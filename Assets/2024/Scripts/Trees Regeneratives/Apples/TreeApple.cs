using UnityEngine;

public class TreeApple : MonoBehaviour
{
    private Character _player;
    private DoTweenManager _doTween;

    private AudioSource _myAudio;
    [SerializeField] AudioClip _soundHit;

    public float amountHit = 100f;
    [HideInInspector] public float initialAmount;

    [SerializeField] KeyCode _inputInteractive = KeyCode.Mouse0;

    [SerializeField] FallApples _treeFall;
    [SerializeField] Apple _apple;
    [SerializeField] SaplingApple _sapling;

    [SerializeField] GameObject _decal;
    [SerializeField] HitApple _hitBar;

    private CharacterInventory _inventory;
    private BoxCollider _myCol;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _myCol = GetComponent<BoxCollider>();

        _player = FindObjectOfType<Character>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _doTween = FindObjectOfType<DoTweenManager>();
    }

    private void Start()
    {
        _treeFall.gameObject.SetActive(false);
        _apple.gameObject.SetActive(false);
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
        if (player != null && _myCol.enabled) _hitBar.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();

        if (player != null && _myCol.enabled)
        {
            if (!Input.GetKey(_inputInteractive))
            {
                _hitBar.gameObject.SetActive(true);
                player.enabled = true;
                player.MainAnim();
            }
                
            //else if (Input.GetKey(_inputInteractive) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            //{
            //    if (!_inventory.shovelSelected)
            //        player.HitTree();
            //}

            else
            {
                if (!_inventory.shovelSelected)
                {
                    //_doTween.Shake(gameObject.transform);
                    //player.HitTree();
                    //amountHit--;
                    //if (!_myAudio.isPlaying) _myAudio.PlayOneShot(_soundHit);
                    //_hitBar.Bar();
                    FocusToTree();
                    _doTween.Shake(gameObject.transform);
                    player.HitTree();
                    player.enabled = false;
                    amountHit--;
                    if (!_myAudio.isPlaying) _myAudio.PlayOneShot(_soundHit);
                    _hitBar.Bar();
                }
            }

            if (amountHit <= 0)
            {
                amountHit = 0;
                _hitBar.gameObject.SetActive(false);
                _decal.SetActive(false);
                _treeFall.gameObject.SetActive(true);
                _treeFall._isFall = true;
                player.enabled = true;
                player.MainAnim();
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled)
        {
            _decal.SetActive(false);
            _hitBar.gameObject.SetActive(false);
            player.enabled = true;
            player.MainAnim();
        }
    }

    public void RestartAmount()
    {
        amountHit = initialAmount;
        _hitBar.Bar();
    }
}