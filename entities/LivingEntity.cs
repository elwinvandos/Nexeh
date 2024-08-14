using Godot;

namespace Nexeh.entities
{
    public abstract partial class LivingEntity : CharacterBody2D
    {
        [Signal]
        public delegate void DamageTakenEventHandler(int oldValue, int newValue);

        public abstract int Health { get; set; }

        public virtual void TakeDamage(int amount)
        {
            int oldHealth = Health;
            Health -= amount;

            EmitSignal(SignalName.DamageTaken, oldHealth, Health);
        }
    }
}
