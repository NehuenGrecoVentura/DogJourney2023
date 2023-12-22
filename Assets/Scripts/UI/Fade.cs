using UnityEngine;

public class Fade : MonoBehaviour
{
    public Animator myAnim;

    private void Start()
    {
        Invoke("FadeOut", 2);
    }

    public void FadeOut()
    {
        myAnim.Play("Fade");
    }
}