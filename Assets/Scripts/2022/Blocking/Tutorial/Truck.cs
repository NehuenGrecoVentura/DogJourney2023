using UnityEngine;
using UnityEngine.UI;

public class Truck : MonoBehaviour
{
    [SerializeField] Text _winText;
    float _timeWinText = 2f;
    [Header("SPEED OPTIONS")]
    [SerializeField] float _timeToDestroy;


    [Header("WHEELS")]
    [SerializeField] GameObject[] _wheels;
    [SerializeField] int _rotZ = -4;

    Animator __myAnim;

    private void Start()
    {
        _winText.gameObject.SetActive(false);
        __myAnim = GetComponent<Animator>();
        __myAnim.enabled = false;
    }

    void Update()
    {
        TimeWinTextInScreen();
        RotateWheels(0, 0, _rotZ);
        _timeToDestroy -= Time.deltaTime;
        if (_timeToDestroy <= 0)
        {
            _timeToDestroy = 0;
            _winText.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void RotateWheels(int x, int y, int z)
    {
        foreach (var wheel in _wheels) wheel.transform.Rotate(x, y, z);
    }

    void TimeWinTextInScreen()
    {
        _timeWinText -= Time.deltaTime * 2f;
        if (_timeWinText <= 0)
        {
            _timeWinText = 0;
            _winText.gameObject.SetActive(false);
        }
    }
}