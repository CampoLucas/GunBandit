using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    bool IsAlive();
    void TakeDamage(int damage);
    void AddHealth(int amount);
}
