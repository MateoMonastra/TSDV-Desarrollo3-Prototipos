using UnityEngine;

namespace Gameplay.GhostMechanics
{
    public class Ghost : MonoBehaviour
    {
        public float timeToRotate = 2f; 
        private float _timer = 0f;
        public float angleRange = 90f;
        public float hp = 100.0f;
        private Rigidbody _rb;

        private Transform _holder;
                Rigidbody rigidbody1 ;
    
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void SetPoint(Transform holder)
        {
           _holder = holder;
           if(holder != null)
            {
                rigidbody1 =holder.GetComponent<Rigidbody>();
                GetComponent<SpringJoint>().connectedBody = rigidbody1;
            }
           else {
                GetComponent<SpringJoint>().connectedBody = null;
                rigidbody1 = null;
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
