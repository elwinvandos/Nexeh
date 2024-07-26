using Godot;

namespace Nexeh.entities
{
    public abstract partial class LivingEntity : CharacterBody2D
    {
        [Signal]
        public delegate void HitEventHandler();

        [Signal]
        public delegate void HealthDepletedEventHandler(int oldValue, int newValue);

        public abstract int Health { get; set; }

        public virtual void TakeDamage(int amount)
        {
            int oldHealth = Health;
            Health -= amount;

            if (Health <= 0)
            {
                EmitSignal(SignalName.HealthDepleted, oldHealth, Health);
            }
        }
    }
}
