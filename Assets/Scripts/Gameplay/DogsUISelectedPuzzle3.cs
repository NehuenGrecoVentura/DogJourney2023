using UnityEngine;

public class DogsUISelectedPuzzle3 : MonoBehaviour
{
    public GameObject dog1Selected;
    public GameObject dog2Selected;
    public GameObject dog3Selected;

    void Start()
    {
        Dog1Selected();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Dog1Selected();
        if (Input.GetKeyDown(KeyCode.Alpha2)) Dog2Selected();
        if (Input.GetKeyDown(KeyCode.Alpha3)) Dog3Selected();
    }

    void Dog1Selected()
    {
        dog1Selected.gameObject.SetActive(true);
        dog2Selected.gameObject.SetActive(false);
        dog3Selected.gameObject.SetActive(false);
    }

    void Dog2Selected()
    {
        dog1Selected.gameObject.SetActive(false);
        dog2Selected.gameObject.SetActive(true);
        dog3Selected.gameObject.SetActive(false);
    }

    void Dog3Selected()
    {
        dog1Selected.gameObject.SetActive(false);
        dog2Selected.gameObject.SetActive(false);
        dog3Selected.gameObject.SetActive(true);
    }
}