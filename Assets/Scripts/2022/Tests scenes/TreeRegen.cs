using UnityEngine;

public class TreeRegen : MonoBehaviour
{
    [Header("INTERACTIVE CONFIG")]
    [SerializeField] KeyCode _buttonHitTree = KeyCode.Mouse0;
    [SerializeField] GameObject _iconInteractive;

    [Header("ATRIBUTES TREE")]
    public int hitDown = 5;
    [HideInInspector] public int initialHitDown;
    [SerializeField] GameObject _treeFall;

    [Header("SAPLING CONFIG")]
    [SerializeField] SaplingTest _sapling;
    [SerializeField] GameObject _saplingObject;
    Character2022 _player;
    public ArrowQuest arrow;

    [Header("SOUND CONFIG")]
    [SerializeField] AudioClip _soundHitTree;
    AudioSource _myAudio;

    [Header("PARTICLE FEEDBACK")]
    [SerializeField] ParticleSystem _particleHit;

    void Awake()
    {
        _iconInteractive.SetActive(false);
        _player = FindObjectOfType<Character2022>();
        _treeFall.SetActive(false);
        _saplingObject.SetActive(false);
        _myAudio = GetComponent<AudioSource>();
        _particleHit.Stop();
    }

    void Start()
    {
        initialHitDown = hitDown;
    }

    private void Update()
    {
        TreeDown();
    }

    void TreeDown()
    {
        if (hitDown <= 0)
        {
            hitDown = 0;
            _treeFall.SetActive(true);
            _saplingObject.SetActive(true);
            _sapling.setGrowTrue();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Axe" && Input.GetKeyDown(_buttonHitTree) && hitDown >= 0)
        {
            Vector3 pos = new Vector3(transform.position.x, _player.transform.position.y, transform.position.z);
            _particleHit.Play();
            _myAudio.PlayOneShot(_soundHitTree);
            _iconInteractive.SetActive(false);
            _player.transform.LookAt(pos);
            _player.EjecuteAnim("Hit");
            hitDown--;
        }

        else if (other.gameObject.tag == "Axe" && !Input.GetKeyDown(_buttonHitTree) && hitDown >= 0)
            _iconInteractive.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Axe") _iconInteractive.SetActive(false);
    }
}