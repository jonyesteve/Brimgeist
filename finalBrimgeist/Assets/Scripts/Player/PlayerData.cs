using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData : BaseStats
{
    public Stat maxRayFuel,
                maxBullets,
                maxFuel,
                maxExp,
                fuelMultiplier,
                xpMultiplier;

    public int currBullets;
    public float currExp,
                 currFuel;

    public PlayerData(PlayerController player)
    {
        maxRayFuel = player.stats.maxRayFuel;
        maxBullets = player.stats.maxBullets;
        maxExp = player.stats.maxExp;
        maxHp = player.stats.maxHp;
        maxShieldHp = player.stats.maxShieldHp;
        currExp = player.stats.currExp;
        attackSpeed = player.stats.attackSpeed;
        damage = player.stats.damage;
        moveSpeed = player.stats.moveSpeed;
        fuelMultiplier = player.stats.fuelMultiplier;
        xpMultiplier = player.stats.xpMultiplier;
        armor = player.stats.armor;
    }
}
