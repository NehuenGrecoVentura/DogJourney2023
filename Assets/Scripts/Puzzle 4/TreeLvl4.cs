using UnityEngine;

public class TreeLvl4 : MonoBehaviour
{
    private Animator _anim;
    public GameObject Block;
    [SerializeField] private Animation talar;
    public GameObject treeStatic;
    private float _timeToDestroy = 1.5f;
    [HideInInspector] public bool destroyCol = false;
    private AudioSource _myAudio;
    public AudioClip soundTree;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _myAudio = GetComponent<AudioSource>();
        treeStatic.gameObject.SetActive(false);
    }

    public void Fall()
    {
        _anim.SetBool("IsChopped", true);
        _myAudio.PlayOneShot(soundTree);
        talar.Play();
        destroyCol = true;
        Destroy(Block.gameObject);
        _timeToDestroy -= Time.deltaTime * 1f;
        if (_timeToDestroy <= 0)
        {
            _timeToDestroy = 0;
            treeStatic.gameObject.SetActive(true);
            Destroy(gameObject, 1.5f);
        }
    }
}

