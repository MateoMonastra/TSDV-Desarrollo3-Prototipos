using UnityEngine;

namespace Player.FSM.States.Vacuum
{
    [CreateAssetMenu(fileName = "VacuumModel", menuName = "Player/FSM/States/VacuumModel")]
    public class VacuumModel : ScriptableObject
    {
        [SerializeField] private float speed;
        [SerializeField] private float maxAngle = 45.0f;
        [SerializeField] private float renderDistance = 5.0f;
        [SerializeField] private LayerMask wallLayer;

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public float MaxAngle
        {
            get => maxAngle;
            set => maxAngle = value;
        }

        public float RenderDistance
        {
            get => renderDistance;
            set => renderDistance = value;
        }

        public LayerMask WallLayer
        {
            get => wallLayer;
            set => wallLayer = value;
        }
    }
}