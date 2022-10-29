using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FailSector : Sector
{
    [SerializeField] private GameObject _platform;

    private void Move()
    {
        transform.DOMoveY(transform.position.y + 10, 2f);
    }
}
