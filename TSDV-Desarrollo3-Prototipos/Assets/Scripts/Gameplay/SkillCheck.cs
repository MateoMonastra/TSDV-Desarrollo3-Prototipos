using System;
using UnityEngine;
using UnityEngine.UI;
using Random = Unity.Mathematics.Random;

namespace Gameplay
{
    using UnityEngine;
    using UnityEngine.UI;

    public class SkillCheck : MonoBehaviour
    {
        [SerializeField] private float needleSpeed = 200f;
        [SerializeField] private float SkillCheckToWin = 5f;

        private float skillcheckCounter = 0f;
        
        public RectTransform needle;
        public RectTransform safeZone;

        private bool _isGameActive = false;

        private readonly float _maxZone = 150.0f;
        private readonly float _minZone = -150.0f;

        public event Action OnWin;
        public event Action OnLose;

        void Update()
        {
            if (_isGameActive)
            {
                MoveNeedle();
                CheckSkillCheck();
            }
        }

        private void OnEnable()
        {
            StartGame();
        }

        void StartGame()
        {
            RandomizeSafeZone();
            skillcheckCounter = 0f;
            _isGameActive = true;
        }

        void MoveNeedle()
        {
            var transformLocalPosition = needle.transform.localPosition;

            transformLocalPosition.x += needleSpeed * Time.deltaTime;

            if (transformLocalPosition.x > _maxZone)
            {
                needleSpeed = -needleSpeed;
            }
            else if (transformLocalPosition.x < _minZone)
            {
                needleSpeed = -needleSpeed;
            }
            else
            {
                needle.transform.localPosition = transformLocalPosition;
            }
        }

        void CheckSkillCheck()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RectTransform needleRect = needle;
                RectTransform safeZoneRect = safeZone;

                if (IsOverlapping(needleRect, safeZoneRect))
                {
                    RandomizeSafeZone();
                    skillcheckCounter++;

                    if(skillcheckCounter >= SkillCheckToWin)
                    {
                        OnWin?.Invoke();
                    }

                    Debug.Log("acertado");
                }
                else
                {
                    EndGame();
                    Debug.Log("mal");
                }
            }
        }

        void RandomizeSafeZone()
        {
            float newX = Random.Range(_minZone, _maxZone);
            safeZone.transform.localPosition = new Vector2(newX, safeZone.transform.localPosition.y);
        }

        bool IsOverlapping(RectTransform rectA, RectTransform rectB)
        {
            Vector3[] cornersA = new Vector3[4];
            Vector3[] cornersB = new Vector3[4];

            rectA.GetWorldCorners(cornersA);
            rectB.GetWorldCorners(cornersB);

            Rect rect1 = new Rect(cornersA[0], cornersA[2] - cornersA[0]);
            Rect rect2 = new Rect(cornersB[0], cornersB[2] - cornersB[0]);

            return rect1.Overlaps(rect2);
        }

        void EndGame()
        {
            OnLose?.Invoke();
            _isGameActive = false;
        }
    }
}