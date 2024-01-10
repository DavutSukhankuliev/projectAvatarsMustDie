using System;

public interface IHealthHandler : IDisposable
{
    public int MaxHealth { get; }
    public int CurrentHealth { get; }
    public event EventHandler DeathEvent;
    public event EventHandler<int> HealthChangeEvent;
    public void ChangeHealth(int delta);
}
