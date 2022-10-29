using TMPro;
using UnityEngine;

public class ChangerTextTypeAge : MonoBehaviour
{
    private const string _textMonkeyAge = "Monkey";
    private const string _textFirstAge = "Primeval";
    private const string _textSecondAge = "Medieval";
    private const string _textThirdAge = "Modernity";
    private const string _textFourthAge = "Future";
    [SerializeField] private TMP_Text _textTypeAge;

    private void Awake()
    {
        _textTypeAge.text = _textFirstAge;
    }

    public void TryChange(EraType eraType)
    {
        _textTypeAge.text = eraType switch
        {
            EraType.Monkey => _textMonkeyAge,
            EraType.Primeval => _textFirstAge,
            EraType.Medieval => _textSecondAge,
            EraType.Modernity => _textThirdAge,
            EraType.Future => _textFourthAge,
            _ => _textTypeAge.text
        };
    }
}