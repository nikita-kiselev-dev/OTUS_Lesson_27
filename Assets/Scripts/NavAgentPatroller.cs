using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class NavAgentPatroller : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent m_NavMeshAgent;
        [SerializeField] private List<Transform> m_PatrolPoints;
        
        private Transform _currentTarget;
        private Vector3 _currentTargetPosition;
        private int _currentPointIndex = 0;
        private float _speed = 0.05f;
        
        private void Start()
        {
            StartMoving();
        }

        private void StartMoving()
        {
            _currentPointIndex = 0;
            _currentTarget = m_PatrolPoints[_currentPointIndex];
        }

        private void Update()
        {
            _currentTargetPosition = _currentTarget.position;
            Move();
        }

        private void Move()
        {
            var distance = _currentTarget.position - transform.position;
            m_NavMeshAgent.destination = _currentTargetPosition;
            
            TryChangeTarget(distance);
        }

        private void TryChangeTarget(Vector3 distance)
        {
            var magnitude = distance.magnitude;
            if (1.0f > magnitude)
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