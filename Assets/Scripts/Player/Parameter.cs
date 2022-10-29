using System;

public class Parameter
{
    private int _value;

    public Parameter(int value)
    {
        _value = value;
    }

    public int Value
    {
        get => _value;
        private set
        {
            _value = value;
            Changed?.Invoke(value);
        }
    }

    public event Action<int> Changed;

    public void Add(int value)
    {
        if (_value - value < 0)
            Value = 0;
        Value += value;
    }
}