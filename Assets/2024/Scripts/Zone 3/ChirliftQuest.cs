using UnityEngine;

public class ChirliftQuest : MonoBehaviour
{
    [SerializeField] QuestUI _questUI;

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(KeyCode.F)) 
        {
            _questUI.TaskCompleted(1);
            Destroy(this);
        }
    }
}