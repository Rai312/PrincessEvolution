using UnityEngine;
using DG.Tweening;
using TMPro;

[SelectionBase]
public class Gate : MonoBehaviour
{
    [SerializeField] private Gate _pairGate;
    [SerializeField] private Collider _collider;
    [SerializeField] private EraType _type;
    [SerializeField] private int _value;
    [SerializeField] private TMP_Text _textValue;
    [SerializeField] private string _textAfterValue;

    private float _durationSlideDown = 0.25f;
    private float _durationStretching = 0.2f;
    private float _valueScaleY = 1.45f;
    private float _intervalBetweenStretchAndSlide = 0.2f;
    private float _offsetY = 12.5f;
    private const char _plus = '+';

    private void Awake()
    {
        if (_value < 0)
            _textValue.text = _value.ToString() + _textAfterValue;
        else if (_value > 0)
            _textValue.text = _plus + _value.ToString() + _textAfterValue;
    }

    public EraType Type => _type;
    public Collider Collider => _collider;
    public int Value => _value;

    public void Disable()
    {
        StartAnimation();
        _collider.enabled = false;
        if (_pairGate != null)
            _pairGate.Collider.enabled = false;
    }

    private void StartAnimation()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() =>
        {
            Stretch();
        });

        sequence.AppendInterval(_intervalBetweenStretchAndSlide);
        sequence.AppendCallback(() =>
        {
            SlideDown();
        });
    }

    private void SlideDown()
    {
        transform.DOMoveY(transform.position.y - _offsetY, _durationSlideDown);
        _pairGate.gameObject.transform.DOMoveY(_pairGate.gameObject.transform.position.y - _offsetY, _durationSlideDown);
    }

    private void Stretch()
    {
        transform.DOScaleY(_valueScaleY, _durationStretching);
        _pairGate.gameObject.transform.DOScaleY(_valueScaleY, _durationStretching);
    }
}