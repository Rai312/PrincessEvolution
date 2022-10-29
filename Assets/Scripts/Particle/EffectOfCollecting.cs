using UnityEngine;

[RequireComponent(typeof(Item))]
public class EffectOfCollecting : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ParticleSystem _greenEffect;
    [SerializeField] private ParticleSystem _yellowEffect;
    [SerializeField] private ParticleSystem _redEffect;

    private Item _item;

    private void Awake()
    {
        _item = GetComponent<Item>();
    }
    public void PlayEffect()
    {
        if (_item.Type < _player.EvolutionLevel)
            _redEffect.Play();

        else if (_item.Type > _player.EvolutionLevel)
            _greenEffect.Play();

        else
            _greenEffect.Play();
    }
}
