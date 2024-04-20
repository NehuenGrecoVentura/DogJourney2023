using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MailQuest : MonoBehaviour
{
    public KeyCode _keyInteractive = KeyCode.Return;
    //public GameObject iconQuest;
    public TMP_Text[] questsTexts;
    public Image[] imageStatusPhase;
    public Image iconQuestActive;
    public GameObject[] _phasesQuests;
    public string nameQuest;
    public string secondText;

    public QuestUI _questUI;
    public string[] tasks;


    public TMP_FontAsset styleNormal, styleSelect;
    public TMP_Text textConfirm;

    public void ShowTasks()
    {
        //Destroy(iconQuest);
        _questUI.ActiveUIQuest(nameQuest, tasks[0], tasks[1], tasks[2]);
    }

    public void Confirm(GameObject letterQuest)
    {
        Character player = FindObjectOfType<Character>();
        player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Destroy(letterQuest);
        //Destroy(iconQuest);
    }

    public void EventEnter()
    {
        textConfirm.fontMaterial = styleSelect.material;
        textConfirm.transform.DOScale(0.9f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void EventExit()
    {
        textConfirm.fontMaterial = styleNormal.material;
        textConfirm.transform.DOScale(0.9f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }
}