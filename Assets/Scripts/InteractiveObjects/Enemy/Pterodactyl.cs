using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Pterodactyl : Enemy
{
    private Animator _animator;
    private const string _isFlying = "IsFlying";
    private const string _isFlyingAway = "IsFlyingAway";
    private float _durationBeforeDisableMesh = 2f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Destroy()
    {
        ParticleSystem.Play();
        SwitchAnimation();
        StartCoroutine(DisableMeshAfterWaiting());
    }

    private void SwitchAnimation()
    {
        _animator.SetBool(_isFlying, false);
        _animator.SetBool(_isFlyingAway, true);
    }

    private IEnumerator DisableMeshAfterWaiting()
    {
        yield return new WaitForSeconds(_durationBeforeDisableMesh);

        SkinnedMeshRenderer.enabled = false;
    }
}
