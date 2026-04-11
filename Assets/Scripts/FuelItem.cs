using UnityEngine;

namespace Shmup
{
    public class FuelItem : Item
    {
        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<Player>().AddFuel(amount);
            Destroy(gameObject);
        }
    }
}