using UnityEngine;
using UnityEngine.UI;

public class ObjetivesUI : MonoBehaviour
{
    [SerializeField] Text _textBeginMission;

    private void Start()
    {
        _textBeginMission.gameObject.SetActive(false);
    }

    public void BeginMission()
    {
        _textBeginMission.gameObject.SetActive(true);
    }

}
