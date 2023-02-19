using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    public int Hp { get; private set; }
    public int ShieldHp { get; private set; }
    public abstract void Heal(int amount);

    public abstract void TakeDamage(int damage);
}

public interface IDamageable
{
    public int Hp { get; }
    public int ShieldHp { get; }
    public void TakeDamage(int damage);

    public void Heal(int amount);
}
