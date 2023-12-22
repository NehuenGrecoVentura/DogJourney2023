using UnityEngine;

public class Quest : MonoBehaviour
{
    public Mailbox _mailbox;
    Pause _pause;

    private void Awake()
    {
        _pause = FindObjectOfType<Pause>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _mailbox.Accept();
            _pause.Defrize();
            Destroy(gameObject);
        }
    }
}