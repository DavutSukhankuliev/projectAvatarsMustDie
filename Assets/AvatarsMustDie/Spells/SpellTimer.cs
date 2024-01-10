using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpellCountDownTimer : IDisposable
{
    public readonly float CountDown;
    public readonly Action OnEnd;
    public float CurrentValue = 0f;

    public SpellCountDownTimer(float countDown, Action onEnd)
    {
        CountDown = countDown;
        OnEnd = onEnd;
    }

    public void Dispose()
    {
        
    }
}

public class SpellTimer : ITickable
{
    private readonly TickableManager _tickableManager;
    private List<SpellCountDownTimer> _countDowns = new List<SpellCountDownTimer>();
    private List<SpellCountDownTimer> _cdsToRemove = new List<SpellCountDownTimer>(); 

    public SpellTimer(TickableManager tickableManager)
    {
        _tickableManager = tickableManager;
        _tickableManager.Add(this);
    }

    public void AddSpell(float countdown, Action action)
    {
        _countDowns.Add(new SpellCountDownTimer(countdown, action));
    }

    public void Tick()
    {
        var delta = Time.deltaTime;
        foreach (var timer in _countDowns)
        {
            timer.CurrentValue += delta;
            if (timer.CurrentValue >= timer.CountDown)
            {
                timer.OnEnd?.Invoke();
                _cdsToRemove.Add(timer);
            }
        }
        if (_cdsToRemove.Count > 0)
        {
            foreach (var timer in _cdsToRemove)
            {
                _countDowns.Remove(timer);
                timer.Dispose();
            }
            _cdsToRemove.Clear();
        }
    }
}
