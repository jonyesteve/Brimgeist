
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class AttackBehaviour
{
    public readonly GameObject obj;
    public readonly Transform[] bulletSpawnPos;
    public readonly GameObject bulletPrefab;
    protected bool exchange;
    [SerializeField]protected float attackTimer;

    public AttackBehaviour(GameObject obj, Transform[] bulletSpawnPos, GameObject bulletPrefab)
    {
        this.bulletPrefab = bulletPrefab;
        this.obj = obj;
        this.bulletSpawnPos = bulletSpawnPos;
    }

    public void CreateBullet(int pos, CharacterStats stats)
    {
        var bullet = GameManager.instance.usePool ? GameManager.instance._bulletPool.Get().GetComponent<BulletScript>() :
            GameObject.Instantiate(bulletPrefab).GetComponent<BulletScript>();
        bullet.transform.SetPositionAndRotation(bulletSpawnPos[pos].position, obj.transform.rotation);
        bullet.damage = GetDamage(stats);
        bullet.gameObject.layer = obj.layer;
        bullet.speed = 1000;
        bullet.SelectSprite();
    }
    public void CreateBullet(int pos, BaseStats stats)
    {
        var bullet = GameManager.instance.usePool ? GameManager.instance._bulletPool.Get().GetComponent<BulletScript>() :
            GameObject.Instantiate(bulletPrefab).GetComponent<BulletScript>();
        bullet.transform.SetPositionAndRotation(bulletSpawnPos[pos].position, obj.transform.rotation);
        bullet.damage = GetDamage(stats);
        bullet.gameObject.layer = obj.layer;
        bullet.speed = 1000;
        bullet.SelectSprite();
    }

    public int GetDamage(BaseStats stats)
    {
        return stats.damage;
    }

    public void FireBehaviour(PlayerData stats)
    {
        if (attackTimer > (1 / (float)stats.attackSpeed))
        {
            if (Mouse.current.rightButton.ReadValue() == 1)
            {
                attackTimer = 0f;

                if (exchange == true)
                {
                    exchange = false;
                    CreateBullet(0, stats);
                }
                else
                {
                    exchange = true;
                    CreateBullet(1, stats);
                }
                stats.currFuel -= 0.15f;
                GameEvents.PlayerFuelChanged(stats.currFuel, stats.maxFuel);
            }
        }
        else attackTimer += Time.deltaTime;
    }

    
}

