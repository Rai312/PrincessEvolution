using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Finisher : MonoBehaviour
{
    [SerializeField] private LadderBuilder _ladderBuilder;
    [SerializeField] private Animator _cameraAnimator;
    [SerializeField] private EnvironmentView _finisherEnvironmentView;
    [SerializeField][Min(0)] private float _firstStepAppearDelay;
    [SerializeField] [Min(0)] private float _finalAnimationDuration;
    [SerializeField] [Min(0)] private float _environmentChangeDelay;

    private Parameter _finisherEvolution;
    
    private void Awake()
    {
        _finisherEvolution = new Parameter((int) EraType.Primeval);
    }

    public void ShowFinisher(Player player, Action ended)
    {
        _ladderBuilder.CreateLadderBy(player);
        StartCoroutine(Showing());
        StartCoroutine(Moving(player, _ladderBuilder.WayPoints,  ended));
    }

    public void ShowFail()
    {
        _ladderBuilder.CreateFail();
        _ladderBuilder.Sectors[0].Show();
    }

    private IEnumerator Moving(Player player, IReadOnlyList<Vector3> wayPoints, Action stopped)
    {
        player.FinisherMover.Enable();
        var isReachedThePoint = false;

        player.AnimatorController.Finisher();

        foreach (var point in wayPoints)
        {
            isReachedThePoint = false;
            player.MoveTo(point, () => isReachedThePoint = true);

            yield return new WaitWhile(() => isReachedThePoint == false);
        }
        
        player.FinisherMover.Disable();
        player.AnimatorController.EndLevel();
        _cameraAnimator.enabled = true;
        _ladderBuilder.FinalSectorContent.ShowPrinceKiss();
        
        yield return new WaitForSeconds(_finalAnimationDuration);
        stopped?.Invoke();
    }

    private IEnumerator Showing()
    {
        _finisherEnvironmentView.Initialize(_finisherEvolution);
        yield return new WaitForSeconds(_firstStepAppearDelay);
        foreach (var sector in _ladderBuilder.Sectors)
        {
            sector.Show();
            if (sector is SeparatingSector)
                StartCoroutine(ShowEnvironmentChangeWith(_environmentChangeDelay));
            
            yield return new WaitForSeconds(sector.AppearDelay);
        }
    }

    private IEnumerator ShowEnvironmentChangeWith(float delay)
    {
        yield return new WaitForSeconds(delay);
        _finisherEvolution.Add(1);
    }
}