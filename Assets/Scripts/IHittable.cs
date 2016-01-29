using UnityEngine;
using System.Collections;

/// <summary>
/// Applied to objects that stop projectiles and objects but not take damage
/// </summary>
public interface IHittable
{
    void Hit();
}
