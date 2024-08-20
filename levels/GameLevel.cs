using Godot;

namespace Nexeh.levels
{
    public abstract partial class GameLevel : Node2D
    {
        private CanvasLayer _gameMenu;
        private CharacterBody2D _player;
        private CanvasLayer _hud;

        public abstract Vector2 _startingPosition { get; }

        public override void _Ready()
        {
            _gameMenu = ResourceLoader.Load<PackedScene>("res://ui/game_menu.tscn").Instantiate<CanvasLayer>();
            AddChild(_gameMenu);
            _gameMenu.Hide();

            var _hud = ResourceLoader.Load<PackedScene>("res://ui/hud.tscn").Instantiate<CanvasLayer>();
            AddChild(_hud);

            _player = ResourceLoader.Load<PackedScene>("res://entities/player/player.tscn").Instantiate<CharacterBody2D>();
            _player.Position = _startingPosition;
            AddChild(_player);

            var cursor = ResourceLoader.Load("res://assets/UI/Cursor1.png");
            Input.SetCustomMouseCursor(cursor);

            base._Ready();
        }

        public override void _Process(double delta)
        {
            if (Input.IsActionJustPressed("ui_cancel"))
            {
                _gameMenu.Show();
                GetTree().Paused = true;
            }

            base._Process(delta);
        }

        public override void _ExitTree()
        {
            RemoveChild(_gameMenu);
            RemoveChild(_player);
            RemoveChild(_hud);

            base._ExitTree();
        }
    }
}
