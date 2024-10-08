using UnityEngine;

namespace Player
{
    public class PlayerInterface : MonoBehaviour
    {
        private Rigidbody _rb;

        [SerializeField] private LayerMask layerRaycast;

        private Vector3 _originalPosition;
        public float struggleSpeed = 2.0f;
        public float struggleAmplitude = 0.1f;
        public float struggleTime = 0f;

        private bool _mouseClicking;
        private bool _isCapturingGhost;

        private bool _resetDirection;

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _resetDirection = false;
        }

        void Update()
        {
            _mouseClicking = Input.GetMouseButton(0);
            _isCapturingGhost = GetComponentInChildren<VacuumCleaner>().isCapturingGhost;
            
            if (!_isCapturingGhost)
            {
                struggleTime = 0f;

                GetComponent<RandomRotation>().enabled = false;
                GetComponent<Running>().enabled = true;

                if (_resetDirection)
                {
                    GetComponent<Running>().SetDir(Vector3.zero);
                    _resetDirection = false;
                }

                if (_mouseClicking)
                {
                    ChangeRotation();
                }
            }
            else
            {
                GetComponent<Running>().enabled = false;
                _originalPosition = transform.localPosition;
                GetComponent<RandomRotation>().enabled = true;
                _resetDirection = true;
            }
        }

        void ChangeRotation()
        {
            if (!Camera.main) return;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerRaycast))
            {
                Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                Vector3 direction = targetPosition - transform.position;
                Quaternion actualRotation = transform.rotation;
                float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0f, rotationAngle, 0f);

                transform.rotation = targetRotation;
            }
        }


    }
}

