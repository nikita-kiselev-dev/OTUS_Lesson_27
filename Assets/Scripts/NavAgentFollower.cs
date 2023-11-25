using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace
{
    public class NavAgentFollower : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent m_NavMeshAgent;
        [SerializeField] private List<Transform> m_Patrollers;
        
        private Transform _currentTarget;
        private Vector3 _currentTargetPosition;
        private int _currentPointIndex = 0;
        private float _speed = 0.05f;
        
        private void Start()
        {
            StartMoving();
            StartCoroutine(TryChangeTarget());
        }

        private void StartMoving()
        {
            _currentPointIndex = 0;
            _currentTarget = m_Patrollers[_currentPointIndex];
        }

        private void Update()
        {
            _currentTargetPosition = _currentTarget.position;
            Move();
        }

        private void Move()
        {
            m_NavMeshAgent.destination = _currentTargetPosition;
        }

        private IEnumerator TryChangeTarget()
        {
            while (true)
            {
                yield return new WaitForSeconds(5.0f);
                if (_currentTarget == m_Patrollers[^1])
                {
                    StartMoving();
                }
                else
                {
                    _currentPointIndex++;
                    _currentTarget = m_Patrollers[_currentPointIndex];
                }
            }
        }
    }
}