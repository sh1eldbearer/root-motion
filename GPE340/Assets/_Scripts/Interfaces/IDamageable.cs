﻿using System.Collections;
using System.Collections.Generic;

public interface IDamageable
{
    void TakeDamage<T>(T dmgAmount);
}
