using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RunnerMovementSystem;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public class GateSelection : MonoBehaviour
{
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private MovementSystem _movementSystem;
    [SerializeField] private Player _player;
    [SerializeField] private EnergyView _energyView;
    [SerializeField] private ParticleSystem _particleSystem;

    private const string _isChosen = "IsChosen";
    
    public void PlayAnimation()
    {
        Sequence sequance = DOTween.Sequence();

        sequance.AppendCallback(() =>
        {
            _cameraAnimator.enabled = true;
            _energyView.gameObject.SetActive(false);
            _movementSystem.enabled = false;
        });

        sequance.AppendCallback(() =>
        {
            _player.transform.DOMoveX(0.3f, 0.75f);
            //_player.transform.DOMoveZ(transform.position.z + 2f, 0.75f);
            _player.AnimatorController.Choose();
        });

        sequance.AppendInterval(0.75f);

        //sequance.AppendInterval(0.2f);
        sequance.AppendCallback(() =>
        {
            _particleSystem.Play();
        });

        sequance.AppendInterval(1.75f);
        //sequance.AppendInterval(2.75f);
        sequance.AppendCallback(() =>
        {
            _particleSystem.gameObject.SetActive(false);
        });

        //sequance.AppendInterval(0.75f);
        sequance.AppendCallback(() =>
        {
            _cameraAnimator.SetBool(_isChosen, false);
            //_energyView.gameObject.SetActive(true);
            _movementSystem.enabled = true;
            _player.AnimatorController.Run();
            _energyView.gameObject.SetActive(false);
            gameObject.SetActive(false);
        });
    }
}

