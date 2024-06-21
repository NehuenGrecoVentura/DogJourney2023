using UnityEngine;

public class ChirliftQuest : MonoBehaviour
{
    [SerializeField] QuestUI _questUI;
    [SerializeField] NPCZone3 _quest;

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(KeyCode.F) && _quest._questActive) 
        {
            _questUI.TaskCompleted(1);
            Destroy(this);
        }
    }
}