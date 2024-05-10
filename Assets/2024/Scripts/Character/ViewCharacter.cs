using UnityEngine;

public class ViewCharacter
{
    private Animator _anim;
    private AudioSource _audio;
    private AudioClip[] _soundsCallDog;

    public ViewCharacter(Animator anim, AudioSource audio, AudioClip[] soundsCall)
    {
        _anim = anim;
        _audio = audio;
        _soundsCallDog = soundsCall;
    }

    void AnimState(bool walk, bool run, bool crouch, bool crouchWalk)
    {
        _anim.SetBool("isWalk", walk);
        _anim.SetBool("isRun", run);
        _anim.SetBool("isCrouch", crouch);
        _anim.SetBool("isWalkCrouch", crouchWalk);
    }

    public void IdleAnim()
    {
        AnimState(false, false, false, false);
    }

    public void WalkAnim()
    {
        AnimState(true, false, false, false);
    }

    public void RunAnim()
    {
        AnimState(false, true, false, false);
    }

    public void IdleCrouch()
    {
        AnimState(false, false, true, false);
    }

    public void CrouchWalk()
    {
        AnimState(false, false, false, true);
    }

    public void ClimbAnim()
    {
        _anim.Play("Up Stairs New");
    }

    public void PickIdleAnim()
    {
        _anim.Play("Pick Idle");
    }

    public void PickWalkAnim()
    {
        _anim.Play("Pick Walk");
    }

    public void CallIdleDogAnim()
    {
        _anim.Play("Silbido Idle");
        int random = Random.Range(0, _soundsCallDog.Length);
        _audio.PlayOneShot(_soundsCallDog[random]);
    }

    public void CallMoveDogAnim()
    {
        _anim.Play("Silbido Run");
        int random = Random.Range(0, _soundsCallDog.Length);
        _audio.PlayOneShot(_soundsCallDog[random]);
    }

    public void HitAnim()
    {
        _anim.Play("Hit");
    }

    public void HitDig()
    {
        _anim.Play("Cavar");
    }

    public void Win(AudioClip audioWin)
    {
        _anim.Play("Win");
        _audio.PlayOneShot(audioWin);
    }
}