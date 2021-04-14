using System;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        private CharacterController _characterController;
        private Camera _camera;
        private int _ignoredLayers;
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _camera = Camera.main;
            _ignoredLayers = ~(1<<LayerMask.NameToLayer("Bullet"));
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit hit;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000, _ignoredLayers))
                {
                    _characterController.SetTargetPoint(hit.point);
                }
            }
        }
    }
}