using UnityEngine;
using DG.Tweening;

public class AreaRabbit : MonoBehaviour
{
    [SerializeField] Rabbit _myRabbit;

    [Header("MESSAGE SLIDE")]
    [SerializeField] Sprite _iconMessage;

    [Header("MESSAGE")]
    [SerializeField] GameObject _boxMessage;

    private void Start()
    {
        _boxMessage.SetActive(false);
        _boxMessage.transform.DOScale(0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _myRabbit.Hide();
    }

    private void OnTriggerStay(Collider other)
    {
        var carrot = other.GetComponent<CarrotItem>();
        if (carrot != null)
        {
            if (!carrot.objectPicked)
            {
                Destroy(_boxMessage, 1.2f);
                _myRabbit.GoToCarrot(carrot.gameObject.transform);
                carrot.GetComponent<Collider>().enabled = false;
                carrot.DesactiveIcon();
                Destroy(carrot.gameObject, 2f);
                Destroy(gameObject);
            }

            else
            {
                _boxMessage.SetActive(true);
                _boxMessage.transform.DOScale(0.8f, 0.5f);
            }      
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _myRabbit.Out();
            _boxMessage.transform.DOScale(0f, 0.5f);
        } 
    }
}