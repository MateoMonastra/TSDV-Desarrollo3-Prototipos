using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.GhostMechanics
{
    public class Ghost : MonoBehaviour
    {
        public float hp = 100.0f;
        private Rigidbody _rb;

        public bool IsBeingVacuumed {  get; set; }
        public bool stunned;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            IsBeingVacuumed = false;
            stunned = false;
        }
    }
}
