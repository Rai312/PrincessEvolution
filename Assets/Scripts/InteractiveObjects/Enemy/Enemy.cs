using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _value;
    [SerializeField] private EraType _type;
    [SerializeField] protected SkinnedMeshRenderer SkinnedMeshRenderer;
    [SerializeField] protected ParticleSystem ParticleSystem;

    public int Value => _value;
    public EraType Type => _type;

    public abstract void Destroy();
    public enum EraType
    {
        Primeval,
        Medieval,
        Modernity,
        Future
    }
}
