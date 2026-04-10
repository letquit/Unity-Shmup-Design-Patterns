using UnityEngine;
using Utilities;

namespace Shmup
{
    [CreateAssetMenu(fileName = "PlayerTrackingShot", menuName = "Shmup/WeaponStrategy/PlayerTrackingShot")]
    public class TrackingShot : WeaponStrategy
    {
        [SerializeField] private float trackingSpeed = 1f;

        private Transform target;

        public override void Initialize()
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        
        public override void Fire(Transform firePoint, LayerMask layer)
        {
            var projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            projectile.transform.SetParent(firePoint);
            projectile.layer = layer;
            
            var projectileComponent = projectile.GetComponent<Projectile>();
            projectileComponent.SetSpeed(projectileSpeed);
            projectile.GetComponent<Projectile>().Callback = () =>
            {
                Vector3 direction = (target.position - projectile.transform.position).With(z: firePoint.position.z)
                    .normalized;
                
                Quaternion rotation = Quaternion.LookRotation(direction, Vector3.forward);
                projectile.transform.rotation = Quaternion.Slerp(projectile.transform.rotation, rotation,
                    trackingSpeed * Time.deltaTime);
            };
            
            Destroy(projectile, projectileLifetime);
        }
    }
}