namespace Nexeh.entities.items.inventory
{
    public class HealthPotion : InventoryItem
    {
        public override string SpriteResourceFile => "res://assets/Items/Potions/Icon33.png";

        public override InventoryItemType ItemType => InventoryItemType.HealthPotion;

        public override void UseItem(LivingEntity entity)
        {
            entity.Heal(50);
        }
    }
}
