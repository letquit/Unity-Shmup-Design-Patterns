using System;
using UnityEngine;

namespace Shmup
{
    public class EnemyWeapon : Weapon
    {
        private float fireTimer;

        private void Update()
        {
            fireTimer += Time.deltaTime;

            if (fireTimer >= weaponStrategy.FireRate)
            {
                weaponStrategy.Fire(firePoint, layer);
                fireTimer = 0f;
            }
        }
    }
}