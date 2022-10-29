using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HabudiController : MonoBehaviour
{
    private Vector3 _startPosition;
    private void Start()
    {
        _startPosition = transform.position;
        Patrol();
    }

    private void Patrol()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOLocalMoveZ(transform.localPosition.z - 10f, 4f));
        sequence.Append(transform.DORotate(new Vector3(transform.rotation.x, 330f, transform.rotation.z), 3));
        sequence.Append(transform.DOLocalMoveZ(_startPosition.z, 4f));
        sequence.Append(transform.DORotate(new Vector3(transform.rotation.x, 155f, transform.rotation.z), 3));

        sequence.SetLoops(5, LoopType.Restart);
    }
}
