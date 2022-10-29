using UnityEngine;

public class Mammoth : Enemy
{
    [SerializeField] private MeshRenderer _meshLeftTusk;
    [SerializeField] private MeshRenderer _meshRightTusk;
    [SerializeField] private Rigidbody _rigidbodyLeftTusk;
    [SerializeField] private Rigidbody _rigidbodyRightTusk;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Destroy()
    {
        ParticleSystem.Play();
        SkinnedMeshRenderer.enabled = false;

        _animator.enabled = false;

        _meshLeftTusk.enabled = true;
        _meshRightTusk.enabled = true;

        _rigidbodyLeftTusk.isKinematic = false;
        _rigidbodyRightTusk.isKinematic = false;
    }
}
