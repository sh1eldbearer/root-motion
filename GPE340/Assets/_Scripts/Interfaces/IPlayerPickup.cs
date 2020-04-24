﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerPickup : IPickupable
{
    void OnPlayerPickup(PawnData playerPawnData);
    void OnPlayerPickup(Collider collider);
}