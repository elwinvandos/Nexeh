namespace Nexeh.entities.items.inventory
{
    public abstract class InventoryItem
    {
        public abstract string SpriteResourceFile { get; }
        public abstract InventoryItemType ItemType { get; }
        public abstract void UseItem(LivingEntity entity);
    }
}

public enum InventoryItemType
{
    HealthPotion = 1
}