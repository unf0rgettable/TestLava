using System;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private List<PointPath> pointPaths;
        private CharacterController _characterController;
        private PointPath CurrentPointPath => pointPaths[CurrentIndex];
        private int _currentIndex;
        private int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                if (value >= pointPaths.Count)
                {
                    value = 0;
                }
                _currentIndex = value;
            }
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            StartRun();
        }

        private void StartRun()
        {
            CurrentIndex = 0;
            RunToCurrentPoint();
        }
        
        private void RunToCurrentPoint()
        {
            _characterController.SetTargetPoint(CurrentPointPath.transform.position);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PointPath pointPath) && pointPath == CurrentPointPath)
            {
                CurrentIndex++;
                RunToCurrentPoint();
            }
        }
    }
}
