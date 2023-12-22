using UnityEngine;

public class UnlockedNotification : MonoBehaviour
{
    [SerializeField] float _timeInScreen = 3f;

    void Update()
    {
        _timeInScreen -= Time.deltaTime;
        if(_timeInScreen <= 0)
        {
            _timeInScreen = 0;
            gameObject.SetActive(false);
        }
    }
}