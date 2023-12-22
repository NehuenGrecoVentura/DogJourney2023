using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepWolfPuzzle3 : MonoBehaviour
{
    public GameObject dogAlert;
    public Transform player;
    [HideInInspector]
    public bool _detected = false;
    public SkinnedMeshRenderer skin;
    private SleepWolfPuzzle3[] _wolfs;
    public GameObject iconZzz;
    public GameObject[] wolfsleeping;
    public GameObject[] wolfAlerted;
    public GameObject iconAlert;
    public SleepWolf areaWolf;
    private AudioSource _myAudio;
    public AudioClip soundDetected;
    public GameManager gManager;

    void Start()
    {
        _myAudio = GetComponent<AudioSource>();
        dogAlert.gameObject.SetActive(false);
        _wolfs = FindObjectsOfType<SleepWolfPuzzle3>();
        foreach (var alerted in wolfAlerted) alerted.gameObject.SetActive(false);
        iconAlert.gameObject.SetActive(false);
    }

    void Update()
    {
        if (_detected || areaWolf.detected)
        {

            foreach (var wolfSleep in _wolfs)
            {
                if (wolfSleep.gameObject.tag == "Sleep Wolf")
                {
                    wolfSleep.skin.enabled = false;
                    wolfSleep.iconZzz.gameObject.SetActive(false);
                    gManager.GameOver();
                }
            }

            foreach (var wolfAlert in wolfAlerted)
            {
                wolfAlert.gameObject.SetActive(true);
                wolfAlert.transform.LookAt(player);
            }            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.tag == "Sleep Wolf")
        {
            _detected = true;
            skin.enabled = false;
            _myAudio.PlayOneShot(soundDetected);
        }
    }
}
