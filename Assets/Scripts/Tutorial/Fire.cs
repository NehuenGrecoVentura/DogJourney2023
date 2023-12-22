using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject water;
    public Buttons button1;
    public Buttons button2;
    public Buttons button3;
    public Buttons button4;

    void Start()
    {
        water.gameObject.SetActive(false);
    }

    void Update()
    {
        if (button1.button1Acitve && button2.button2Acitve && button3.button3Acitve && button4.button4Acitve)
        {
            water.gameObject.SetActive(true);
            Destroy(gameObject, 1f);
        }
    }
}