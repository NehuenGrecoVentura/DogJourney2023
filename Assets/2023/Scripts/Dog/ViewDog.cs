using UnityEngine;

public class ViewDog 
{
    private Animator _anim;
    private AudioSource _trolleyAudio;

    public ViewDog(Animator anim, AudioSource trolleyAudio)
    {
        _anim = anim;
        _trolleyAudio = trolleyAudio;
    }

    private void AnimState(bool idle, bool walk)
    {
        _anim.SetBool("Idle", idle);
        _anim.SetBool("Walk", walk);
    }

    public void IdleAnim()
    {
        //_trolleyAudio.Stop();
        AnimState(true, false);
    }

    public void WalkAnim()
    {
        AnimState(false, true);
    }
}