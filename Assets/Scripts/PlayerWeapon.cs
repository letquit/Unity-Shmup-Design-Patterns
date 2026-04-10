using UnityEngine;

namespace Shmup
{
    public class PlayerWeapon : Weapon
    {
        private InputReader input;
        private float fireTimer;

        private void Awake() => input = GetComponent<InputReader>();

        private void Update()
        {
            fireTimer += Time.deltaTime;

            if (input.Fire && fireTimer >= weaponStrategy.FireRate)
            {
                weaponStrategy.Fire(firePoint, layer);
                fireTimer = 0f;
            }
        }
    }
}