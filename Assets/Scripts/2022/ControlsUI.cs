using UnityEngine;
using UnityEngine.UI;

public class ControlsUI : MonoBehaviour
{
    [SerializeField] Text w;
    [SerializeField] Text a;
    [SerializeField] Text s;
    [SerializeField] Text d;

    void Start()
    {
        w.gameObject.SetActive(true);
        a.gameObject.SetActive(true);
        s.gameObject.SetActive(true);
        d.gameObject.SetActive(true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)) Destroy(w);
        if(Input.GetKeyDown(KeyCode.A)) Destroy(a);
        if(Input.GetKeyDown(KeyCode.S)) Destroy(s);
        if(Input.GetKeyDown(KeyCode.D)) Destroy(d);
    }
}
