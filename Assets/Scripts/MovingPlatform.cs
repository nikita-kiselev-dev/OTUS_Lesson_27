using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private List<Transform> m_PatrolPoints;
        [SerializeField] private float m_Speed = 0.25f;

        private Transform _objectTransform;
        private Transform _currentTarget;
        private int _currentPointIndex = 0;
        
        private void Start()
        {
            _objectTransform = gameObject.GetComponent<Transform>();
            StartMoving();
        }

        private void StartMoving()
        {
            _currentPointIndex = 0;
            _currentTarget = m_PatrolPoints[_currentPointIndex];
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            var distance = _currentTarget.position - transform.position;

            TryChangeTarget(distance);

            var direction = distance.normalized;
            _objectTransform.Translate(direction * m_Speed);
        }

        private void TryChangeTarget(Vector3 distance)
        {
            var magnitude = distance.magnitude;
            if (magnitude <= 0.1f)
            {
                if (_currentTarget == m_PatrolPoints[^1])
                {
                    StartMoving();
                }
                else
                {
                    _currentPointIndex++;
                    _currentTarget = m_PatrolPoints[_currentPointIndex];
                }
            }
        }
    }
}