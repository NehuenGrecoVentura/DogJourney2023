using UnityEngine;

public class CutTree : MonoBehaviour
{
    public KeyCode buttonCutTree = KeyCode.Mouse0;
    [SerializeField] GameObject _iconAxe;
    [SerializeField] GameObject _treeFall;
    public int lifeTree;
    [HideInInspector]
    public int initialLife;
    Character2022 _player;
    GameObject[] _treeDecals;
    public GameObject _myDecal;
    [SerializeField] Animator _animGate1, _animGate2;
    [Header("HIT FEEDBACK CONFIG")]
    [SerializeField] ParticleSystem _particleHit;
    [SerializeField] AudioClip _soundHitTree;
    AudioSource _myAudio;
    [SerializeField] Material _greenTreeMat;
    

    void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _player = FindObjectOfType<Character2022>();
        _treeDecals = GameObject.FindGameObjectsWithTag("Tree Decals");
    }

    void Start()
    {
        foreach (var decal in _treeDecals)
            decal.SetActive(false);

        _myDecal.SetActive(true);

        initialLife = lifeTree;
        _treeFall.SetActive(false);
        _iconAxe.gameObject.SetActive(false);
        _animGate1.enabled = false;
        _animGate2.enabled = false;
        _particleHit.Stop();
    }

    void Cut()
    {
        Vector3 pos = new Vector3(transform.position.x, _player.transform.position.y, transform.position.z);
        _particleHit.Play();
        _myAudio.PlayOneShot(_soundHitTree);
        lifeTree--;
        _player.transform.LookAt(pos);
        _player.EjecuteAnim("Hit");
        _iconAxe.gameObject.SetActive(false);
        if (lifeTree <= 0)
        {
            lifeTree = 0;
            _treeFall.SetActive(true);
            gameObject.SetActive(false);
            OpenGates();
        }
    }

    public void OpenGates()
    {
        foreach (var decal in _treeDecals)
            decal.SetActive(true);

        MeshRenderer[] materials = FindObjectsOfType<MeshRenderer>();
        string leaves = "Green Leaves";

        foreach (MeshRenderer leave in materials)
        {
            if (leave.gameObject.name == leaves && leave.gameObject.CompareTag(leaves))
                leave.GetComponent<MeshRenderer>().material = _greenTreeMat;
        }

        Destroy(_myDecal);
        _animGate1.enabled = true;
        _animGate2.enabled = true;
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Axe" && !Input.GetKeyDown(buttonCutTree)) _iconAxe.gameObject.SetActive(true);
        else if (other.gameObject.tag == "Axe" && Input.GetKeyDown(buttonCutTree)) Cut();
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Axe") _iconAxe.gameObject.SetActive(false);
    }
}