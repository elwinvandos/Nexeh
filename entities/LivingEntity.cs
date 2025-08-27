using Godot;
using Nexeh.entities.items.inventory;
using System.Collections.Generic;

namespace Nexeh.entities
{
    public abstract partial class LivingEntity : CharacterBody2D
    {
        [Signal]
        public delegate void HealthChangedEventHandler(int oldValue, int newValue);

        public abstract int Health { get; set; }
        public abstract int MaxHealth { get; set; }
        public virtual List<InventoryItem> Inventory { get; set; } = [];

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

        public virtual void AddToInventory(InventoryItem item)
        {
            Inventory.Add(item);
        }

        public virtual void UseItem(InventoryItem item)
        {
            if (Inventory.Contains(item))
            {
                item.UseItem(this);
                Inventory.Remove(item);
            }
        }
    }
}
