using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FailPlatformMover : MonoBehaviour
{
    [SerializeField] private GameObject _platform;

    private void OnTriggerEnter(Collider other)
    {
        _platform.SetActive(true);
        Move();
        //if (other.TryGetComponent(out Player player))
        //{
        //    Debug.Log("FailPlatformMover");

        //}
    }

    private void Move()
    {
        transform.DOMoveY(transform.position.y + 10, 2f);
    }
}
