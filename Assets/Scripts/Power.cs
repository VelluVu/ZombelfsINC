
using UnityEngine;

[CreateAssetMenu(fileName = "New Power", menuName = "Inventory/Power")]
public class Power : ScriptableObject {

    new public string name = "New Power";
    public Sprite icon = null;
    public bool statBooster;
    public bool axeBoost;
    public bool swordBoost;
    public bool spellBoost;
    public GameObject pickUpEffect;
    public GameObject pickUpSound;

    //boost char stats:
    
    public float healthBoost;
    public float manaBoost;
    public float speedBoost;
    public float manaRegenBoost;
    public float healthRegenBoost;
    public float jumpForceBoost;
    public float boostDuration;

    //boost weapon power:

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
 
}
