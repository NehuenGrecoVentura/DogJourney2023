using UnityEngine;

public class DiaryUI : MonoBehaviour
{
    public GameObject diary;
    private bool _isOpen = false;

    void Start()
    {
        diary.gameObject.SetActive(false);
    }

    void Update()
    {
        OpenCloseDiary();
    }

    void OpenCloseDiary()
    {
        if (Input.GetKeyDown(KeyCode.J) && !_isOpen)
        {
            diary.gameObject.SetActive(true);
            _isOpen = true;
        }

        else if (Input.GetKeyDown(KeyCode.J) && _isOpen || Input.GetKeyDown(KeyCode.Escape) && _isOpen)
        {
            diary.gameObject.SetActive(false);
            _isOpen = false;
        }
    }
}