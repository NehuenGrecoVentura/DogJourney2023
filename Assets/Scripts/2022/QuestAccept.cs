using UnityEngine;

public class QuestAccept : MonoBehaviour
{
    [SerializeField] MailManager _mailbox;
    Pause _pause;

    private void Awake()
    {
        _pause = FindObjectOfType<Pause>();
    }

    void Update()
    {
        if (Input.GetKeyDown(_mailbox.buttonAccept))
        {
            _mailbox.ConfirmQuest();
            _pause.Defrize();
            Destroy(gameObject);
        }
    }
}