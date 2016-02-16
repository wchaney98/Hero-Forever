using UnityEngine;
using System.Collections;

/// <summary>
/// Applies to classes that can be hit and take damage
/// </summary>
public interface IDamageable
{
    void TakeDamage(int damage);
}
