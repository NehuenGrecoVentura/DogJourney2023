using UnityEngine;
using UnityEngine.UI;

public class Prompt : MonoBehaviour
{
    private Camera _cam;
    private Image _promptImg;

    private bool _isActive = true;
    private const float OFFSET = 1f;

    private void Start()
    {
        GameObject prefab = Resources.Load("UI/Interact") as GameObject;
        _promptImg = Instantiate(prefab, FindObjectOfType<Canvas>().transform).GetComponent<Image>();
        _cam = Camera.main;
        TooglePrompt();
    }

    private void Update()
    {
        Vector3 position = transform.position + Vector3.up * OFFSET;
        _promptImg.transform.position = _cam.WorldToScreenPoint(position);
    }

    public void TooglePrompt()
    {
        _isActive = !_isActive;
        _promptImg.gameObject.SetActive(_isActive);
    }
}
