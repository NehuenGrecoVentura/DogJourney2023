using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public string nameNPC;
    public Dialogue _dialogue;

    //private void Awake()
    //{
    //    _dialogue = FindObjectOfType<Dialogue>();
    //}

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _dialogue.playerInRange = true;
            _dialogue.Set(nameNPC);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _dialogue.playerInRange = false;
    }
}