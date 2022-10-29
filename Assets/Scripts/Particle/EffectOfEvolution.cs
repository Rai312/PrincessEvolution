using UnityEngine;

public class EffectOfEvolution : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effectOfMonkey;
    [SerializeField] private ParticleSystem _effectOfFirstAge;
    [SerializeField] private ParticleSystem _effectOfSecondAge;
    [SerializeField] private ParticleSystem _effectOfThirdAge;
    [SerializeField] private EffectOfFourthAge _effectOfFourthAge;

    public void PlayEffect(EraType eraType)
    {
        switch (eraType)
        {
            case EraType.Monkey:
                _effectOfMonkey.Play();
                break;
            case EraType.Primeval:
                _effectOfFirstAge.Play();
                break;
            case EraType.Medieval:
                _effectOfSecondAge.Play();
                break;
            case EraType.Modernity:
                _effectOfThirdAge.Play();
                break;
            case EraType.Future:
                _effectOfFourthAge.StartEffect();
                break;
        }
    }
}
