using UnityEngine;
using System.Collections;

/// <summary>
/// For any GameObjects that can deal damage to the player, or vice versa
/// </summary>
public interface IDoesDamage
{
    int Damage { get; }
}
