using UnityEngine;

namespace Gameplay.GhostMechanics
{
    public class Ghost : MonoBehaviour
    {
        public float hp = 100.0f;
        private Rigidbody _rb;

        public bool IsBeingVacuumed {  get; set; }

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            IsBeingVacuumed = false;
        }
    }
}
