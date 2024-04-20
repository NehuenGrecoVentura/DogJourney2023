using UnityEngine;

public class Pick : MonoBehaviour
{
    public GameObject _iconInteractive;
    public KeyCode _keyInteractive = KeyCode.F;

    void Start()
    {
        _iconInteractive.SetActive(false);
    }
}
