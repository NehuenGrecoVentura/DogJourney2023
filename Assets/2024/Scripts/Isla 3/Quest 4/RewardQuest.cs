using UnityEngine;

public class RewardQuest : MonoBehaviour
{
    [SerializeField] float _timeInScreen = 3f;
    private Animator _myAnim;

    private void Awake()
    {
        _myAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        _timeInScreen -= Time.deltaTime;
        if(_timeInScreen <= 0)
        {
            _timeInScreen = 0;
            _myAnim.SetBool("In", false);
            _myAnim.SetBool("Out", true);
            Destroy(transform.parent.gameObject, 2f);
        }
    }
}