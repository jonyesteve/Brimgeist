using UnityEngine;

[System.Serializable]
public class BaseStats 
{
    [Header("BaseStats")]
    public Stat attackSpeed;
    public Stat moveSpeed;
    public Stat maxHp,
                maxShieldHp,
                damage,
                armor;
    public int currHp,
               currShieldHp;


}

