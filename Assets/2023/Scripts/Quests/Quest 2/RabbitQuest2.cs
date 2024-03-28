using UnityEngine;

public class RabbitQuest2 : MonoBehaviour
{
    private QuestUI _questUI;

    private void Awake()
    {
        _questUI = FindObjectOfType<QuestUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var wolf = other.GetComponent<WolfStatic>();
        if (wolf != null)
        {
            _questUI.TaskCompleted(2);
            Destroy(this);
        }
    }
}