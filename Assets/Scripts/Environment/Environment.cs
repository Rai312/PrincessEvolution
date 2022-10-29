using System.Collections;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] private EraType _type;
    [SerializeField] [Min(0.01f)] private float _delay;
    [SerializeField] private Color _color;

    [SerializeField] private EnvironmentPart[] _environmentParts;

    private Coroutine _coroutine;
    
    public EraType Type => _type;
    public Color Color => _color;

    private void Awake()
    {
        _environmentParts = null;
        _environmentParts = GetComponentsInChildren<EnvironmentPart>();
    }

    public void Show()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);
        
        _coroutine = StartCoroutine(Appearing());
    }

    public void Hide()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);
        
         _coroutine = StartCoroutine(Disappearing());
    }

    public void SetActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    private IEnumerator Disappearing()
    {
        foreach (var part in _environmentParts)
        {
            part.Hide();
            yield return new WaitForSeconds(_delay);
        }
    }

    private IEnumerator Appearing()
    {
        foreach (var part in _environmentParts)
        {
            part.Show();
            yield return new WaitForSeconds(_delay);
        }
    }
}