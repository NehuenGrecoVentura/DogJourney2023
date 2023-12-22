using UnityEngine;

public class OptionsInGame : MonoBehaviour
{
    private void Awake()
    {
        var dontDestroy = FindObjectsOfType<OptionsInGame>();
        if(dontDestroy.Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}