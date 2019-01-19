using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SwordPower", menuName = "Inventory/Power/SwordPower")]
public class SwordPower : Power {

    public float speedzBoost;
    public float speedyBoost;
    public float rotationSpeedBoost;
    public float damageBoost;
    public float areaDamageBoost;
    public float shotIntervalBoost;
    public float spellCostBoost;
    public float areaRadiusBoost;
    public float meleeDamageBoost;
    public float criticalChanceBoost;

    public float[] GetSwordBoostArray()
    {
        float[] boostMods = new float[10];

        boostMods[0] = speedzBoost;
        boostMods[1] = speedyBoost;
        boostMods[2] = rotationSpeedBoost;
        boostMods[3] = damageBoost;
        boostMods[4] = areaDamageBoost;
        boostMods[5] = shotIntervalBoost;
        boostMods[6] = spellCostBoost;
        boostMods[7] = areaRadiusBoost;
        boostMods[8] = meleeDamageBoost;
        boostMods[9] = criticalChanceBoost;

        return boostMods;
    }
}