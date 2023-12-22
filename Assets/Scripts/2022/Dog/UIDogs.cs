using UnityEngine;

public class UIDogs : MonoBehaviour
{
    [SerializeField] Material _dog1;
    [SerializeField] Material _dog2;
    MeshRenderer _icon;
    private void Awake()
    {
        _icon = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        SelectDog();
    }

    void SelectDog()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _icon.material = _dog1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _icon.material = _dog2;
        }
    }
}