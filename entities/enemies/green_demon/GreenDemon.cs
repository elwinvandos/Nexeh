using Godot;
using Nexeh.entities;
using System.Linq;

public partial class GreenDemon : LivingEntity
{
	private float _speed = 150f;
	private NavigationAgent2D _navigationAgent;
	private Vector2 _movementTargetPosition;
	private Player _player;

	public Vector2 MovementTarget
	{
		get { return _navigationAgent.TargetPosition; }
		set { _navigationAgent.TargetPosition = value; }
	}

	public override int Health { get; set; } = 100;

	public override void _Ready()
	{
		AddToGroup("GreenDemon");

		_player = GetTree().GetNodesInGroup("Player").First() as Player;

		_navigationAgent = GetNode<NavigationAgent2D>("DemonNavAgent");

		_navigationAgent.PathDesiredDistance = 4.0f;
		_navigationAgent.TargetDesiredDistance = 4.0f;

		// Make sure to not await during _Ready.
		Callable.From(ActorSetup).CallDeferred();

		base._Ready();
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		//hook into player position signal
		_player.PlayerPosition += (playerPos) => _movementTargetPosition = playerPos;

		if (_navigationAgent.IsNavigationFinished())
		{
			//attack ?
		}

		Vector2 currentAgentPosition = GlobalTransform.Origin;
		//Vector2 nextPathPosition = _navigationAgent.GetNextPathPosition();

		if (Health <= 0)
		{
			Velocity = new Vector2(0, 0);
		}
		else
		{
			Velocity = currentAgentPosition.DirectionTo(_movementTargetPosition) * _speed;
			MoveAndSlide();
		}

		//var collision = MoveAndCollide(Velocity);

		//if (collision != null)
		//{
		//	Velocity = Velocity.Bounce(collision.GetNormal());
		//}
	}

	private async void ActorSetup()
	{
		// Wait for the first physics frame so the NavigationServer can sync.
		await ToSignal(GetTree(), SceneTree.SignalName.PhysicsFrame);

		// Now that the navigation map is no longer empty, set the movement target.
		MovementTarget = _movementTargetPosition;
	}
}
