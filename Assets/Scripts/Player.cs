using System;
using UnityEngine;

namespace Shmup
{
    public class Player : Plane
    {
        [SerializeField] private float maxFuel;
        [SerializeField] private float fuelConsumptionRate;

        private float fuel;
        
        private void Start() => fuel = maxFuel;
        
        public float GetFuelNormalized() => fuel / maxFuel;

        private void Update()
        {
            fuel -= fuelConsumptionRate * Time.deltaTime;
        }

        public void AddFuel(float amount)
        {
            fuel += amount;
            if (fuel > maxFuel)
            {
                fuel = maxFuel;
            }
        }

        protected override void Die()
        {
        }
    }
}