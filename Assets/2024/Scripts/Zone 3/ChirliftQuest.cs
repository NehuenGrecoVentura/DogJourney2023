using UnityEngine;
using DG.Tweening;

public class ChirliftQuest : MonoBehaviour
{
    [SerializeField] QuestUI _questUI;
    [SerializeField] NPCZone3 _quest;
    [SerializeField] Transform _iconInteract;

    private void Start()
    {
        _iconInteract.DOScale(0f, 0f);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(KeyCode.F) && _quest._questActive) 
        {
            _questUI.TaskCompleted(1);
            Destroy(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteract.DOScale(0f, 0f);
    }
}