using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StatPower", menuName = "Inventory/Power/StatPower")]
public class StatPower : Power {

    public float healthBoost;
    public float manaBoost;
    public float speedBoost;
    public float manaRegenBoost;
    public float healthRegenBoost;
    public float jumpForceBoost;
}
