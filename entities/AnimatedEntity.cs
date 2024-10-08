using Godot;
using Nexeh.entities;
using System;

namespace GungeonClone.entities
{
	// Should be abstract but see issue:
	// https://github.com/godotengine/godot/issues/79519
	public partial class AnimatedEntitySprite : AnimatedSprite2D
	{
		[Signal] public delegate void OnAnimationFinishedCallEventHandler(AnimatedSprite2D sprite);

		private bool _hasDied = false;

		public LivingEntity _entity;

		public override void _Ready()
		{
			_entity = GetParent() as LivingEntity;

			base._Ready();
		}

		public override void _Process(double delta)
		{
			_entity.DamageTaken += (oldValue, newValue) =>
			{
				// make sprite flash white
				var tween = CreateTween();
				tween.TweenProperty(this, "modulate:v", 1, 0.25).From(15);
			};

			base._Process(delta);
		}

		public void AnimateWalking(Vector2 velocity)
		{
			var angle = Math.Atan2(velocity.Y, velocity.X);

			if (Math.Abs(angle) < 0.25 * Math.PI)
			{
				Play("walk_right");
			}
			else if (Math.Abs(angle) > 0.75 * Math.PI)
			{
				Play("walk_left");
			}
			else if (angle > 0.0)
			{
				Play("walk_down");
			}
			else
			{
				Play("walk_up");
			}
		}

		public void AnimateDeath()
		{
			if (!_hasDied)
			{
				_hasDied = true;

				Play("death");

				AnimationFinished += () =>
				{
					SetFrameAndProgress(6, 0);
				};
			}
		}

		public override void _ExitTree()
		{
			AnimationFinished -= OnAnimationFinished;

			base._ExitTree();
		}


		private void OnAnimationFinished() => EmitSignal(SignalName.OnAnimationFinishedCall, this);
	}
}
