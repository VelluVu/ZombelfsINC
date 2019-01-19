
using UnityEngine;

[CreateAssetMenu(fileName = "New Power", menuName = "Inventory/Power")]
public class Power : ScriptableObject {

    new public string name = "New Power";
    public Sprite icon = null;
    public GameObject pickUpEffect;
    public GameObject pickUpSound;
    public GameObject pickUpText;
    public float boostDuration;

    public bool multiShooter;
    public int shotMultiply;
    public float shotSpread;

}
