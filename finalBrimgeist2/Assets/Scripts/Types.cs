public static class Types
{
    public enum EnemyType
    {

        Striker = 0,
        Warrior = 1,
        Healer = 4,
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
