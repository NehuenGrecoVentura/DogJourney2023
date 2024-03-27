using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MailQuest : MonoBehaviour
{
    public KeyCode _keyInteractive = KeyCode.Return;
    public GameObject iconQuest;
    public TMP_Text[] questsTexts;
    public Image[] imageStatusPhase;
    public Image iconQuestActive;
    public GameObject[] _phasesQuests;
    public string nameQuest;
    public string secondText;

    public QuestUI _questUI;
    public string[] tasks;

    //public void StatusUI(string nameQuest, string secondtText, Image questActive)
    //{
    //    questsTexts[0].text = nameQuest;
    //    questsTexts[1].text = secondtText;
    //    iconQuestActive = questActive;
    //    //imageStatusPhase[0].gameObject.SetActive(true);
    //    //imageStatusPhase[1].gameObject.SetActive(false);
    //    imageStatusPhase[0].enabled = true;
    //    imageStatusPhase[1].enabled = false;
    //    Destroy(iconQuest);

    //    foreach (var item in _phasesQuests)
    //        item.SetActive(true);
    //}

    public void ShowTasks()
    {
        Destroy(iconQuest);
        _questUI.ActiveUIQuest(nameQuest, tasks[0], tasks[1], tasks[2]);
    }

    public void Confirm(GameObject letterQuest)
    {
        Character player = FindObjectOfType<Character>();
        player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(letterQuest);
        Destroy(iconQuest);
        //StatusUI(nameQuest, secondText, iconQuestActive);
    }
}