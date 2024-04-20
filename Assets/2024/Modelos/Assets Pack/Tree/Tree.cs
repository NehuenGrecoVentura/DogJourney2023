using UnityEngine;

public class Tree : MonoBehaviour
{
    private Animator _anim;
    public GameObject Block;
    [SerializeField] private Animation talar;
    public GameObject trunk;
    public GameObject treeStatic;
    private float _timeToDestroy = 1.5f;
    [HideInInspector] public bool destroyCol = false;
    private AudioSource _myAudio;
    public AudioClip soundTree;
    Tree treeScript;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _myAudio = GetComponent<AudioSource>();
        trunk.gameObject.SetActive(false);
        treeStatic.gameObject.SetActive(false);
        treeScript = GetComponent<Tree>();
    }

    public void Fall()
    {
        Debug.Log("I'm falling");
        _anim.SetBool("IsChopped", true);
        _myAudio.PlayOneShot(soundTree);
        talar.Play();
        destroyCol = true;
        trunk.gameObject.SetActive(true);
        Destroy(Block.gameObject);
        _timeToDestroy -= Time.deltaTime * 1f;
        if (_timeToDestroy <= 0)
        {
            _timeToDestroy = 0;
            treeStatic.gameObject.SetActive(true);
            Destroy(gameObject, 1.5f);
        }
        Destroy(treeScript);
    }
}
