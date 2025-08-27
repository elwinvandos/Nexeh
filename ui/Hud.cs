using Godot;
using Nexeh.entities.items.inventory;
using System.Diagnostics;

public partial class Hud : CanvasLayer
{
    private Label _healthLabel;
    private Node2D _inventory;

    public override void _Ready()
    {
        AddToGroup("HUD");
        _healthLabel = GetNode<Label>("HealthLabel");
        _inventory = GetNode<Node2D>("InventoryNode");
    }

    public void UpdatePlayerHealth(int health)
    {
        _healthLabel.Text = $"Health: {health}";
    }

    public void AddNewItemToInventory(InventoryItem item)
    {
        var texture = ResourceLoader.Load<Texture2D>(item.SpriteResourceFile);

        var sprite = new Sprite2D
        {
            Texture = texture,
            Position = _inventory.Position + new Vector2(2, 0)
        };

        var itemLabel = new Label
        {
            Name = $"Label{item.ItemType}",
            Text = 1.ToString(),
            Position = sprite.Position,
            OffsetTop = 2,
            OffsetLeft = 2
        };

        sprite.AddChild(itemLabel);
        _inventory.AddChild(sprite);
    }

    public void UpdateItemQuantityInInventory(InventoryItem item, int quantity)
    {
        foreach (var child in _inventory.GetChildren())
        {
            if (child is Sprite2D sprite && sprite.Texture != null && sprite.Texture.ResourcePath == item.SpriteResourceFile)
            {
                var label = sprite.GetNode<Label>($"Label{item.ItemType}");
                var newQuantity = int.Parse(label.Text) + quantity;

                if (newQuantity <= 0)
                {
                    _inventory.RemoveChild(sprite);
                    return;
                }

                label.Text = newQuantity.ToString();
                break;
            }
        }
    }
}
