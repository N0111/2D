using UnityEngine;

namespace PlatformerMVC
{
    public class PlayerController
    {
        private SpriteAnimatorController _playerAnimator;
        private AnimationConfig _config;
        private LevelObjectView _playerView;
        private ContactPooler _contactPooler;

        private Transform _playerT;
        private Rigidbody2D _rb;

        private float _xAxisInput;
        private bool _isJump;

        private float _walkSpeed = 200f;
        private float _animationSpeed = 10f;
        private float _movingTreshold = 0.1f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _righttScale = new Vector3(1, 1, 1);

        private bool _isMoving;

        private float _jumpForce = 10f;
        private float _jumpTreshold = 1f;
        private float _yVelocity;
        private float _xVelocity;

        public PlayerController(LevelObjectView player)
        {
            _config = Resources.Load<AnimationConfig>("SpriteAnimCfg");
            _playerAnimator = new SpriteAnimatorController(_config);
            _playerAnimator.StartAnimation(player._renderer, AnimState.Run, true, _animationSpeed);

            _contactPooler = new ContactPooler(player._collider);

            _playerView = player;
            _playerT = player._transform;
            _rb = player._rb;
        }

        private void MoveTowards()
        {
            _xVelocity = Time.fixedDeltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1);
            _rb.velocity = new Vector2(_xVelocity, _rb.velocity.y);
            _playerT.localScale = _xAxisInput < 0 ? _leftScale : _righttScale;
        } 

        public void Update()
        {
            _playerAnimator.Update();
            _contactPooler.Update();

            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") > 0;

            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshold;
            _yVelocity = _rb.velocity.y;

            if(_isMoving)
            {
                MoveTowards();
            }
            else
            {
                _xVelocity = 0;
            }

            if (_contactPooler.IsGrounded)
            {
                _playerAnimator.StartAnimation(_playerView._renderer, _isMoving ? AnimState.Run : AnimState.Idle, true, _animationSpeed);

                if (_isJump && _yVelocity <= _jumpTreshold)
                {
                    _rb.AddForce(Vector2.up *_jumpForce, ForceMode2D.Impulse);
                }
            }
            else
            {
                if (Mathf.Abs(_yVelocity) > _jumpTreshold)
                {
                    _playerAnimator.StartAnimation(_playerView._renderer, AnimState.Jump, true, _animationSpeed);
                }
            }
            
        }
    }
}
