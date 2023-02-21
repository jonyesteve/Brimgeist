using UnityEngine;
using static Types;
public class EnemyAttackBehaviour : AttackBehaviour
{
    private bool canAttack;
    private Rigidbody2D rb;
    private EnemyType type;
    public EnemyAttackBehaviour(GameObject obj, Transform[] bulletSpawnPos, GameObject bulletPrefab, Rigidbody2D rb, EnemyType type) 
        : base(obj,bulletSpawnPos,bulletPrefab) 
    {
        this.rb = rb;
        this.type = type;
    }
    public void ShootBehaviour(BaseStats stats)
    {
        if (attackTimer > 1 / (float)stats.attackSpeed)
        {
            switch (type)
            {
                //common
                case < (EnemyType)10:
                    if (canAttack)
                    {
                        if ((int)type < 4)
                        {
                            if(exchange)
                            {
                                exchange = false;
                                CreateBullet(1, stats);
                            }
                            else
                            {
                                exchange = true;
                                CreateBullet(2, stats);
                            }
                        }
                        else
                        {
                            CreateBullet(0, stats);
                        }
                        attackTimer = 0;
                    }
                    if (rb.velocity.magnitude < 1) canAttack = true;
                    break;
                //boss
                case >= (EnemyType)10:
                    if (canAttack)
                    {
                        if (!exchange)
                        {
                            exchange = true;
                            CreateBullet(1,stats);
                        }
                        else
                        {
                            exchange = false;
                            CreateBullet(2,stats);
                        }
                        stats.attackSpeed.AddModifier(new StatModifier(1.2f, 0, this));
                        if (stats.attackSpeed >= 3)
                        {
                            stats.attackSpeed.RemoveAllModifiersFromSource(this);
                        }
                    }
                    break;
            }
        }
        else attackTimer += Time.fixedDeltaTime;
    }
}