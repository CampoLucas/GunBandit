using System;

public interface IDamageable
{
    bool IsAlive();
    void TakeDamage(int damage);
    void AddHealth(int amount);
}
