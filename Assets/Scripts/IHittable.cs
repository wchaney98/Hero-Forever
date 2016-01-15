using UnityEngine;
using System.Collections;

public interface IHittable
{
    // Interface for objects that are hittable but not damageable (i.e. they only make sound on impact)

    void Hit();
}
