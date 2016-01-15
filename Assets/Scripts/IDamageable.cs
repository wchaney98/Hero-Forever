using UnityEngine;
using System.Collections;

public interface IDamageable
{
    // Interface for objects that can take damage and die

    void TakeDamage(float damage);
}
