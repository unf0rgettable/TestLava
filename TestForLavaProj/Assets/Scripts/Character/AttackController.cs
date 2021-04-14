using System;
using Guns;
using Settings;
using UnityEngine;

namespace Character
{
    public class AttackController : MonoBehaviour
    {
        [SerializeField] private GameObject shootEffect;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private Transform spawnPoint;
        private BulletHeap _bulletHeap;

        private void Awake()
        {
            _bulletHeap = FindObjectOfType<BulletHeap>();
        }

        public void Attack(TypeBulletForce typeBulletForce, Vector3 targetPosition)
        {
            Bullet bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity, _bulletHeap.transform);
            Vector3 dir = (targetPosition - transform.position).normalized;
            GameObject effect = Instantiate(shootEffect, spawnPoint.position, Quaternion.LookRotation(dir), _bulletHeap.transform);
            Destroy(effect, 3);
            bullet.Impulse = ConvertTypeBulletForceToForce(typeBulletForce);
            bullet.SetTargetPoint(targetPosition);
        }

        private float ConvertTypeBulletForceToForce(TypeBulletForce typeBulletForce)
        {
            switch (typeBulletForce)
            {
                case TypeBulletForce.Low:
                    return 10;
                case TypeBulletForce.Medium:
                    return 20;
                case TypeBulletForce.High:
                    return 30;
                default: return 0;
            }
        }
    }
}