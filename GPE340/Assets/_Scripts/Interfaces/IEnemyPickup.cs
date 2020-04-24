using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyPickup : IPickupable
{
    void OnEnemyPickup(PawnData enemyPawnData);
    void OnPlayerPickup(Collider collider);
}
