using UnityEngine;

public interface IPickupable
{
    void OnTriggerEnter(Collider collider);
    void OnPickup(Collider collider);
}
