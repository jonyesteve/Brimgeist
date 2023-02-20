using UnityEngine;
using System.Reflection;
using System;

public class GameEvents : MonoBehaviour
{
    public delegate void PlayerHpChange(int hp, int maxHp);
    public static event PlayerHpChange OnPlayerHpChanged;

    public delegate void PlayerFuelChange(float fuel, float maxFuel);
    public static event PlayerFuelChange OnPlayerFuelChanged;

    public delegate void PlayerShieldChange(int shield, int maxShield);
    public static event PlayerShieldChange OnPlayerShieldChanged;

    public delegate void EnemyDeath(GameObject enemy, int type);
    public static event EnemyDeath OnEnemyDeath;

    public static event Action PlayerDeath;
    

    private void Awake()
    {
        ClearAllEvents();
    }
    public static void PlayerHpChanged(int hp, int maxHp)
    {
        OnPlayerHpChanged(hp, maxHp);
    }
    
    public static void PlayerFuelChanged(float fuel, float maxFuel)
    {
        OnPlayerFuelChanged(fuel, maxFuel);
    }

    public static void PlayerShieldChanged(int shield, int maxShield)
    {
        OnPlayerShieldChanged(shield, maxShield);
    }

    public static void ClearAllEvents()
    {
        var events = typeof(GameEvents).GetEvents(BindingFlags.Static);
        for(int i = 0; i < events.Length; i++)
        {
            events[i] = null;
        }
    }

    public static void EnemyDied(GameObject enemy, int type)
    {
        OnEnemyDeath(enemy, type);
    }

    public static void PlayerDied()
    {
        PlayerDeath();
    }


}
