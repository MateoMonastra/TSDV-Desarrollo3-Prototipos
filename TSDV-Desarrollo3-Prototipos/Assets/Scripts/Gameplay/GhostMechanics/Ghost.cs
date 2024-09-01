using UnityEngine;

namespace Gameplay.GhostMechanics
{
    public class Ghost : MonoBehaviour
    {
        public float hp = 100.0f;
        private Rigidbody _rb;

        private Transform _holder;
    
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void SetPoint(Transform holder)
        {
           _holder = holder;
           
           if(holder != null)
           {
               GetComponent<SpringJoint>().connectedBody = holder.GetComponent<Rigidbody>();
           }
           else {
                GetComponent<SpringJoint>().connectedBody = null;
           }
        }
        public void LateUpdate()
        {
           if(_holder != null)
           {
                _rb.AddForce(Vector3.one *Time.deltaTime, ForceMode.Impulse);
           }
        }
    }
}
