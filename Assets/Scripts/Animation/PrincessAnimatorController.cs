using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PrincessAnimatorController : MonoBehaviour
{
    private const string IsRun = nameof(IsRun);
    private const string IsIdle = nameof(IsIdle);
    private const string IsFinisher = nameof(IsFinisher);
    private const string IsEndLevel = nameof(IsEndLevel);
    private const string IsEvolution = nameof(IsEvolution);
    //private const string IsInjuredRun = nameof(IsInjuredRun);
    private const string Speed = nameof(Speed);
    private const string Progress = nameof(Progress);

    private Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        Animator.StopPlayback();
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void SetProgress(float progressValue)
    {
        Animator.SetFloat(Progress, progressValue);
    }
    public void SetSpeedSwipe(float speed)
    {
        Animator.SetFloat(Speed, speed);
    }

    public void Evolution()
    {
        Animator.SetBool(IsRun, false);
        Animator.SetBool(IsIdle, false);
        Animator.SetBool(IsFinisher, false);
        Animator.SetBool(IsEndLevel, false);
        Animator.SetBool(IsEvolution, true);
        //Animator.SetBool(IsInjuredRun, false);
    }

    public void Run()
    {
        Animator.SetBool(IsRun, true);
        Animator.SetBool(IsIdle, false);
        Animator.SetBool(IsFinisher, false);
        Animator.SetBool(IsEndLevel, false);
        Animator.SetBool(IsEvolution, false);
        //Animator.SetBool(IsInjuredRun, false);
    }

    public void Idle()
    {
        Animator.SetBool(IsRun, false);
        Animator.SetBool(IsIdle, true);
        Animator.SetBool(IsFinisher, false);
        Animator.SetBool(IsEndLevel, false);
        Animator.SetBool(IsEvolution, false);
        //Animator.SetBool(IsInjuredRun, false);
    }

    public void Finisher()
    {
        Animator.SetBool(IsRun, false);
        Animator.SetBool(IsIdle, false);
        Animator.SetBool(IsFinisher, true);
        Animator.SetBool(IsEndLevel, false);
        Animator.SetBool(IsEvolution, false);
        //Animator.SetBool(IsInjuredRun, false);
    }

    public void EndLevel()
    {
        Animator.SetBool(IsRun, false);
        Animator.SetBool(IsIdle, false);
        Animator.SetBool(IsFinisher, false);
        Animator.SetBool(IsEndLevel, true);
        Animator.SetBool(IsEvolution, false);
        //Animator.SetBool(IsInjuredRun, false);
    }

    //public void InjuredRun()
    //{
    //    Animator.SetBool(IsRun, false);
    //    Animator.SetBool(IsIdle, false);
    //    Animator.SetBool(IsFinisher, false);
    //    Animator.SetBool(IsEndLevel, false);
    //    Animator.SetBool(IsEvolution, false);
    //    Animator.SetBool(IsInjuredRun, true);
    //}
}