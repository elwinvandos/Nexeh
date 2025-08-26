using Godot;

namespace Nexeh.entities
{
    public abstract partial class LivingEntity : CharacterBody2D
    {
        [Signal]
        public delegate void HealthChangedEventHandler(int oldValue, int newValue);

        public abstract int Health { get; set; }
        public abstract int MaxHealth { get; set; }

        public virtual void TakeDamage(int amount)
        {
            int oldHealth = Health;
            Health -= amount;

            EmitSignal(SignalName.HealthChanged, oldHealth, Health);
        }

        public virtual void Heal(int amount)
        {
            int oldHealth = Health;
            Health += amount;

            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }

            EmitSignal(SignalName.HealthChanged, oldHealth, Health);
        }
    }
}
