using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Types;

public class EnemyController : Entity
{
    
    public BaseStats stats;
    [SerializeField] Rigidbody2D rb;
    public EnemyType type;
    EnemyMovement moveBehaviour;
    [SerializeField] Sprite sprite;
    bool isBoss;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform[] bulletSpawnPos;
    Vector2 vel;
    private bool canAttack;
    private float attackTimer;
    EnemyAttackBehaviour attackBehav;

    public new int Hp { get => stats.currHp; set => stats.currHp = value; }

    public new int ShieldHp { get => stats.currShieldHp; set => stats.currShieldHp = value; }

    public void AssignValues(BaseStats stats, EnemyType type, bool isBoss, Sprite sprite)
    {
        this.stats = stats;
        this.type =  type;
        this.isBoss = isBoss;
        this.sprite = sprite;
    }
    private void OnEnable()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        if (type == EnemyType.Tank) transform.localScale *= 1.5f;
        if (type == EnemyType.Warrior) transform.localScale *= 1.2f;
        stats.currHp = stats.maxHp;
        moveBehaviour = new EnemyMovement(rb, (int)type);
        attackBehav = new EnemyAttackBehaviour(gameObject, bulletSpawnPos, bulletPrefab, rb, type);
        stats.currHp = stats.maxHp;
        moveBehaviour.IntialMovement();
        print("XD");
    }
    private void FixedUpdate()
    {
        RotateToPlayer();
        attackBehav.ShootBehaviour(stats);
        if(rb.velocity.x < 0)
        {
            rb.velocity = Vector2.SmoothDamp(rb.velocity, new Vector2(0,0), ref vel, 0.7f);
        }
    }


    public override void Heal(int amount)
    {
        Hp += amount;
        var healFx = GameManager.instance.healFxPool.Get();
        healFx.transform.position = transform.position;
    }

    public override void TakeDamage(int damage)
    {
        if (ShieldHp > 0) ShieldHp -= damage;
        if (ShieldHp < 0)
        {
            Hp -= ShieldHp;
            damage -= damage - ShieldHp;
            ShieldHp = 0;
        }
        if (ShieldHp == 0) Hp -= damage;
        //play sonido
        //instanciar hit
        if (Hp <= 0) Die();
    }
    void Die()
    {
        var exp = GameManager.instance.explosionPool.Get();
        exp.transform.position = transform.position;
        EnemyManager.current.enemyPool.Release(gameObject);
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer != gameObject.layer)
        {
            if (col.gameObject.CompareTag("Bul"))
            {
                TakeDamage(col.GetComponent<BulletScript>().damage);
                col.GetComponent<BulletScript>().turnOff = true;
            }
        }
    }
    void RotateToPlayer()
    {
        var target = PlayerController.current.transform;
        transform.right = target.position - transform.position;
    }
}
