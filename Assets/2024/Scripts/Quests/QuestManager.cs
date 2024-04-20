using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [SerializeField] GameObject[] _questStatus;
    [SerializeField] Image[] _iconsPhase1;
    [SerializeField] Image[] _iconsPhase2;
    [SerializeField] Image[] _iconsPhase3;
    [SerializeField] TMP_Text[] _texts;

    private void Start()
    {
        HideHUDQuest();
    }

    public void HideHUDQuest()
    {
        foreach (var quest in _questStatus)
            quest.SetActive(false);
    }

    public void QuestStatus(bool title, bool phase1, bool phase2, bool phase3)
    {
        _questStatus[0].SetActive(title);
        _questStatus[1].SetActive(phase1);
        _questStatus[2].SetActive(phase2);
        _questStatus[3].SetActive(phase3);
    }

    public void FirstSuccess(string text)
    {
        QuestStatus(true, true, true, false);
        _iconsPhase1[0].enabled = false;
        _iconsPhase1[1].enabled = true;
        _iconsPhase2[0].enabled = true;
        _iconsPhase2[1].enabled = false;
        _texts[1].text = text;
    }

    public void InitialSecondPhase(string text)
    {
        _texts[2].text = text;
    }

    public void SecondSuccess(string text)
    {
        QuestStatus(true, true, true, true);
        _iconsPhase1[0].enabled = false;
        _iconsPhase1[1].enabled = true;
        _iconsPhase2[0].enabled = false;
        _iconsPhase2[1].enabled = true;
        _iconsPhase3[0].enabled = true;
        _iconsPhase3[1].enabled = false;
        _texts[3].text = text;
    }
}