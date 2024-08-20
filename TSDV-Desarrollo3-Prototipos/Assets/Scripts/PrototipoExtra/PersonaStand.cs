using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace PrototipoExtra
{
    public class PersonaStand : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera camera;
        [SerializeField] private GameObject persona;
        public bool personaInvoked = false;
        public void InvokePersona()
        {
            camera.Follow = persona.transform;
            var running = gameObject.GetComponent<Running>();
            running.enabled = false;
            
            persona.transform.position = gameObject.transform.position;
            persona.gameObject.SetActive(true);
            personaInvoked = true;
        }
        
        public void DesInvokePersona()
        {
            var running = gameObject.GetComponent<Running>();
            running.enabled = true;
            camera.Follow = gameObject.transform;
            
            persona.transform.position = gameObject.transform.position;
            persona.gameObject.SetActive(false);
            personaInvoked = false;
        }
    }
}
