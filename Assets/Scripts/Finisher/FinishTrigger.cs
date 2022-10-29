using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private Finisher _finisher;
    [SerializeField] private Collider _finishTriggerCollider;

    public Finisher Finisher => _finisher;

    public void Disable()
    {
        _finishTriggerCollider.gameObject.SetActive(false);
    }
}