using System;
using Character;
using UnityEngine;

namespace Guns
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        public float Impulse;
        public float SpeedBullet;
        private Rigidbody _rigidbody;
        [SerializeField] private GameObject bulletMesh;
        [SerializeField] private GameObject explosionMesh;
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void SetTargetPoint(Vector3 pos)
        {
            Vector3 direction = pos - transform.position;
            _rigidbody.velocity = direction.normalized * SpeedBullet;
        }

        private void OnCollisionEnter(Collision other)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 3);
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out RagDollController ragDollController))
                {
                    ragDollController.RagDollEnable(true);
                    ragDollController.GetComponent<Collider>().enabled = false;
                    ragDollController.GetComponent<Rigidbody>().isKinematic = true;
                    //ragDollController.GetComponent<Rigidbody>().AddExplosionForce(Impulse, transform.position, 6);
                }

                if (collider.TryGetComponent(out Rigidbody rigidBody))
                {
                    rigidBody.AddExplosionForce(Impulse, other.contacts[0].point, 3);
                }
            }
            
            explosionMesh.transform.SetParent(transform.parent);
            explosionMesh.SetActive(true);
            Destroy(explosionMesh, 4);
            Destroy(gameObject);

        }
    }
}