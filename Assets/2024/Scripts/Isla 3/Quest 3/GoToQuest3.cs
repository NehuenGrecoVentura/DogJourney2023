using UnityEngine;
using TMPro;

public class GoToQuest3 : MonoBehaviour
{
    [Header("NEXT POS")]
    [SerializeField] Transform _nextPos;
    [SerializeField] TMP_Text _textGeneralQuest;
    [SerializeField] string _nextMessage;
    private LocationQuest _map;

    private void Awake()
    {
        _map = FindObjectOfType<LocationQuest>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _map.target = _nextPos;
            _textGeneralQuest.text = _nextMessage;
            Destroy(gameObject);
        }
    }
}