using UnityEngine;

public class FirstMessage : MonoBehaviour
{
    public float count;
    public Animator anim;

    void Update()
    {
        count -= Time.deltaTime;
        if(count <= 0)
        {
            count = 0;
            anim.SetBool("In", false);
            anim.SetBool("Out", true);
            Destroy(gameObject, 2f);
        }
    }
}