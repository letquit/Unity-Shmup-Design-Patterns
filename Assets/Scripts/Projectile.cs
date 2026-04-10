using System;
using UnityEngine;

namespace Shmup
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private GameObject muzzlePrefab;
        [SerializeField] private GameObject hitPrefab;

        private Transform parent;
        
        public void SetSpeed(float speed) => this.speed = speed;
        public void SetParent(Transform parent) => this.parent = parent;

        public Action Callback;

        private void Start()
        {
            if (muzzlePrefab != null)
            {
                var muzzleVFX = Instantiate(muzzlePrefab, transform.position, Quaternion.identity);
                muzzleVFX.transform.forward = gameObject.transform.forward;
                muzzleVFX.transform.SetParent(parent);

                DestroyParticleSystem(muzzleVFX);
            }
        }

        private void Update()
        {
            transform.SetParent(null);
            transform.position += transform.forward * (speed * Time.deltaTime);
            
            Callback?.Invoke();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (hitPrefab != null)
            {
                ContactPoint contact = collision.contacts[0];
                var hitVFX = Instantiate(hitPrefab, contact.point, Quaternion.identity);
                
                DestroyParticleSystem(hitVFX);
            }
            
            Destroy(gameObject);
        }

        private void DestroyParticleSystem(GameObject vfx)
        {
            var ps = vfx.GetComponent<ParticleSystem>();
            if (ps == null)
            {
                ps = vfx.GetComponentInChildren<ParticleSystem>();
            }
            Destroy(vfx, ps.main.duration);
        }
    }
}