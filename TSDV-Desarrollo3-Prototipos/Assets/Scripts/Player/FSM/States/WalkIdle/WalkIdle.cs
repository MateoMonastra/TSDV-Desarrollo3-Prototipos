using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Player.FSM.States.WalIdle
{
    public class WalkIdle : State
    {
        private GameObject _gameObject;
        private WalkIdleModel _model;

        private Vector3 _dir = Vector3.zero;
        private bool _isClickPressed;

        
        private Vector3 _counterMovement;

        public WalkIdle(GameObject gameObject, WalkIdleModel model)
        {
            _gameObject = gameObject;
            _model = model;
        }
        public override void Enter()
        {
        }

        public override void Tick(float delta)
        {
        }

        public override void FixedTick(float delta)
        {
            Move();
        }

        public override void Exit()
        {
        }

        private void Move()
        {
            _counterMovement = new Vector3(-_gameObject.GetComponent<Rigidbody>().velocity.x * _model.CounterMovementForce, 0,
                -_gameObject.GetComponent<Rigidbody>().velocity.z * _model.CounterMovementForce);

            _gameObject.GetComponent<Rigidbody>().AddForce(_dir.normalized * _model.MovementForce + _counterMovement);

            float angle = Vector3.SignedAngle(_gameObject.transform.forward, _dir, _gameObject.transform.up);

            if (!_isClickPressed)
            {
                _gameObject.transform.Rotate(_gameObject.transform.up, angle * Time.deltaTime * _model.RotationSpeed);
            }
        }

        public void SetDir(Vector3 newDir, bool shouldRot)
        {
            _dir = newDir;

            _isClickPressed = shouldRot;
        }
    }
}