using System;

namespace AvatarsMustDie.Enemy
{
    public readonly struct EnemyHealthHandlerProtocol
    {
        public readonly int MaxHealth;

        public EnemyHealthHandlerProtocol(int maxHealth)
        {
            MaxHealth = maxHealth;
        }
    }

    public class EnemyHealthHandler : IHealthHandler
    {
        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _currentHealth;
        public event EventHandler DeathEvent;
        public event EventHandler<int> HealthChangeEvent;
        
        private readonly int _maxHealth;
        private int _currentHealth;
        
        public EnemyHealthHandler(EnemyHealthHandlerProtocol protocol)
        {
            _maxHealth = protocol.MaxHealth;
            _currentHealth = _maxHealth;
        }

        public void ChangeHealth(int delta)
        {
            _currentHealth = Math.Min(_maxHealth, Math.Max(0, _currentHealth + delta));
            HealthChangeEvent?.Invoke(this, _currentHealth);
            if (_currentHealth <= 0)
            {
                DeathEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Dispose()
        {

        }
    }
}