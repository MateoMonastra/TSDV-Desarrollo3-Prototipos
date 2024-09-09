using Unity.VisualScripting;
using UnityEngine;

namespace Player.FSM.States.Vacuum
{
    public class Vacuum : State
    {
        
        private VacuumModel _vacuumModel;
        
        private Transform _target;
        private Collider _collider;
        
        private Ray _ray;
        
        private bool _isActive;
        

        private Vector3? _collision;
        private Quaternion _left;
        private Quaternion _right;

        private Vector3 _leftBoundary;
        private Vector3 _rightBoundary;

        public Vacuum()
        {
        }
        
        public override void Enter()
        {
            _left = Quaternion.AngleAxis(-_vacuumModel.MaxAngle, Vector3.up);
            _right = Quaternion.AngleAxis(_vacuumModel.MaxAngle, Vector3.up);
            _leftBoundary = _left * _target.forward;
            _rightBoundary = _right * _target.forward;
            _collider.transform.localScale = new Vector3(Mathf.Abs(((_leftBoundary.x * 2) * _vacuumModel.RenderDistance)),
                _collider.transform.localScale.y, _collider.transform.localScale.z);
        }

        public override void Tick(float delta)
        {
        }

        public override void FixedTick(float delta)
        {
            
        }

        public override void Exit()
        {
        }
        
        private void VacuumObject(Collider other)
        {
            var angleToObject = Vector3.Angle(_target.forward, other.transform.position - _target.position);

            if (!(angleToObject <= _vacuumModel.MaxAngle)) return;

            _ray = new Ray(_target.position, other.transform.position - _target.position);

            if (Physics.Raycast(_ray, out var hit, _vacuumModel.RenderDistance, _vacuumModel.WallLayer))
            {
                _collision = hit.point;
                return;
            }

            var rb = other.GetComponent<Rigidbody>();

            var direction = (_target.position - other.transform.position).normalized;
            
                rb.AddForce(direction * _vacuumModel.Speed, ForceMode.VelocityChange);

            _collision = null;
        }
        
#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            _left = Quaternion.AngleAxis(-_vacuumModel.MaxAngle, Vector3.up);
            _right = Quaternion.AngleAxis(_vacuumModel.MaxAngle, Vector3.up);
            _leftBoundary = _left * _target.forward;
            _rightBoundary = _right * _target.forward;

            Gizmos.color = Color.green;
            Gizmos.DrawLine(_target.position, _target.position + _target.forward * _vacuumModel.RenderDistance);

            Gizmos.DrawLine(_target.position, _target.position + _leftBoundary * _vacuumModel.RenderDistance);

            Gizmos.DrawLine(_target.position, _target.position + _rightBoundary * _vacuumModel.RenderDistance);
            Gizmos.DrawRay(_ray);
            if (_collision != null)
            {
                Gizmos.DrawSphere(_collision.Value, 0.5f);
            }
        }
#endif
    }
}