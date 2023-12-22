using UnityEngine;

public class AreaRabbit : MonoBehaviour
{
    [SerializeField] Rabbit _myRabbit;

    [Header("MESSAGE SLIDE")]
    [SerializeField] Sprite _iconMessage;
    private MessageSlide _messageSlide;


    private void Awake()
    {
        _messageSlide = FindObjectOfType<MessageSlide>();
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
            //if (Input.GetKeyDown(KeyCode.G))
            //{

            //    _myRabbit.GoToCarrot(carrot.gameObject.transform);
            //    carrot.GetComponent<Collider>().enabled = false;
            //    Destroy(carrot.gameObject, 2f);
            //    Destroy(gameObject);
            //}

            if (!carrot.objectPicked)
            {
                _myRabbit.GoToCarrot(carrot.gameObject.transform);
                carrot.GetComponent<Collider>().enabled = false;
                Destroy(carrot.gameObject, 2f);
                Destroy(gameObject);
            }

            else _messageSlide.ShowMessage("TO DROP THE CARROT", _iconMessage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _myRabbit.Out();
    }
}