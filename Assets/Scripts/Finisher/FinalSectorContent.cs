using UnityEngine;

public class FinalSectorContent : MonoBehaviour
{
    private const string Kiss = nameof(Kiss);

    [SerializeField] private Animator _princeAnimator;
    [SerializeField] private ParticleSystem _heartPoof;

    public void ShowPrinceKiss()
    {
        _princeAnimator.SetTrigger(Kiss);
        _heartPoof.Play();
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}