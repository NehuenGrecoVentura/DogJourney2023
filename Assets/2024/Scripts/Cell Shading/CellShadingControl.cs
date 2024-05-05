using UnityEngine;

public class CellShadingControl : MonoBehaviour
{
    private Outline[] _outline;
    private bool _isActive = false;

    private void Awake()
    {
        _outline = FindObjectsOfType<Outline>();
    }

    void Start()
    {
        foreach (var item in _outline)
            item.enabled = true;
    }

    void Update()
    {
        //if (Input.GetKey(KeyCode.L) && Input.GetKeyDown(KeyCode.S))
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!_isActive)
            {
                foreach (var item in _outline)
                    item.enabled = true;

                _isActive = true;
            }

            else
            {
                foreach (var item in _outline)
                    item.enabled = false;

                _isActive = false;
            }
        }
    }
}