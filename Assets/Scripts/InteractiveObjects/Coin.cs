using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private ParticleSystem _disableFX;
    [SerializeField] private int _value;

    public int Value => _value;

    public void Disable()
    {
        _disableFX.transform.parent = null;
        _disableFX.Play();
        gameObject.SetActive(false);
    }
}