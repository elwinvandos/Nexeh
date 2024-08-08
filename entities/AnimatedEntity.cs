using Godot;
using System;

namespace GungeonClone.entities
{
    // Should be abstract but see issue:
    // https://github.com/godotengine/godot/issues/79519
    public partial class AnimatedEntitySprite : AnimatedSprite2D
    {
        [Signal] public delegate void OnAnimationFinishedCallEventHandler(AnimatedSprite2D sprite);

        private bool _hasDied = false;

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
