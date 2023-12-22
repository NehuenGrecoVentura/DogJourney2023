using UnityEngine;

public class SleepWolf2022 : MonoBehaviour
{
    SleepWolf2022[] _sleepWolfs;
    GameManager _gm;
    Animator _myAnim;
    bool _detected = false;
    public GameObject player;

    void Start()
    {
        _myAnim = GetComponent<Animator>();
        _gm = FindObjectOfType<GameManager>();
        _sleepWolfs = FindObjectsOfType<SleepWolf2022>();
    }

    private void Update()
    {
        if (_detected)
        {
            foreach (var wolf in _sleepWolfs) wolf.Stand();
        }
    }

    public void Stand()
    {
        _myAnim.SetTrigger("Stand");
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _detected = true;
            _gm.GameOver();
        }       
    }
}
