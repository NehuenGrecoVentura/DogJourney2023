using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    [SerializeField] TMP_Text[] _txtsTasks;
    [SerializeField] GameObject _canvas;
    [SerializeField] AudioClip _soundTaskCompleted;
    private AudioSource _myAudio;
    private DoTweenManager _doTween;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _doTween = FindObjectOfType<DoTweenManager>();
    }

    private void Start()
    {
        ResetUI();
        UIStatus(false);
    }

    public void ActiveUIQuest(string nameQuest, string task1, string task2, string task3)
    {
        ResetUI();
        _txtsTasks[0].text = nameQuest;
        _txtsTasks[1].text = task1;
        _txtsTasks[2].text = task2;
        _txtsTasks[3].text = task3;
    }

    private void ResetUI()
    {
        UIStatus(true);
        foreach (var txt in _txtsTasks)
        {
            txt.text = txt.text;
            txt.color = Color.white;
        }
    }

    public void UIStatus(bool active)
    {
        _canvas.SetActive(active);
    }

    public void TaskCompleted(int numberTask)
    {
        _myAudio.PlayOneShot(_soundTaskCompleted);
        _doTween.EffectScale(_txtsTasks[numberTask].gameObject.transform);
        _txtsTasks[numberTask].text = "<s>" + _txtsTasks[numberTask].text + "</s>";
        _txtsTasks[numberTask].color = Color.green;     
    }

    public void AddNewTask(int numberTask, string newTask)
    {
        _txtsTasks[numberTask].text = newTask;
        _txtsTasks[numberTask].color = Color.white;
    }
}