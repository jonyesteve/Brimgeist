using UnityEngine;

[System.Serializable]
public class CharacterStats : BaseStats
{
    [Header("CharacterStats")]
    public Stat maxRayFuel,
                maxBullets,
                maxFuel,
                maxExp,
                fuelMultiplier,
                xpMultiplier;

    public int currBullets;
    public float currExp,
                 currFuel;
}
