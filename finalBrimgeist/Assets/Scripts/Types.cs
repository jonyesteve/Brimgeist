public static class Types
{
    public enum EnemyType
    {

        Striker = 0,
        Healer = 1,
        Warrior = 4,
        Tank = 5,
        Boss1 = 10,
        Boss2 = 11,

    }
    public enum StatModType
    {
        Flat,
        PercentAdd,
        PercentMult
    }
    public enum WeaponType { Ray, Bullet, Missile }
    public enum ShieldType { Forcefield, Magnetic, None }
}
