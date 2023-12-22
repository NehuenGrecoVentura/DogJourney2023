using UnityEngine;

public class testanim : MonoBehaviour
{
    [SerializeField] Animation anim;

    void Update()
    {
        anim["Crouch idle"].speed = 0.05f;
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.D))
        {
            anim.Play("Crouch walk");
            print("ESTOY EN SIGILO");
        }

        else if (Input.GetKey(KeyCode.LeftControl)) anim.Play("Crouch idle");
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            anim.Stop("Crouch walk");
            anim.Stop("Crouch idle");
        }
    }
}