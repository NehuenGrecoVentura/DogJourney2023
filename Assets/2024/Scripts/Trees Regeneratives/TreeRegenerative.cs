using UnityEngine;

public class TreeRegenerative : MonoBehaviour
{    
    //private HouseQuest4 _quest4;
    
    [Header("INTERACT")]
    [SerializeField] KeyCode _inputInteractive = KeyCode.Mouse0;
    [SerializeField] GameObject _decal;
    private CharacterInventory _inventory;
    private BoxCollider _myCol;

    [Header("ATRIBUTES")]
    public float amountHit = 100f;
    [HideInInspector] public float initialAmount;

    [Header("HEALTHBAR")]
    [SerializeField] HitBar _hitBar;

    [Header("AUDIO")]
    [SerializeField] AudioClip _soundHit;
    private AudioSource _myAudio;

    [Header("REFS")]
    [SerializeField] TreeFall _treeFall;
    [SerializeField] Trunks _trunks;
    [SerializeField] SaplingTree _sapling;
    private Character _player;
    private DoTweenManager _doTween;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _myCol = GetComponent<BoxCollider>();

        _player = FindObjectOfType<Character>();
        //_quest4 = FindObjectOfType<HouseQuest4>();
        _doTween = FindObjectOfType<DoTweenManager>();
        _inventory = FindObjectOfType<CharacterInventory>();
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
        if (player != null && _myCol.enabled) _hitBar.gameObject.SetActive(true);  
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();

        if (player != null && _myCol.enabled)
        {
            //FocusToTree();

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

            //if (amountHit <= 0 && _quest4.quest4Active) _quest4.amount++;
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