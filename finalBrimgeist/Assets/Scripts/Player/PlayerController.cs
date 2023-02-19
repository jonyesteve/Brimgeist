
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class PlayerController : Entity
{
    public static PlayerController current;

    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Transform[] bulletSpawnPos;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject playerObject;
    public PlayerData stats;
    public PlayerInput _inputSystem;
    
    [SerializeField] private GameObject shield;

    Vector2 smoothVelocity;

    public AttackBehaviour attackBehav;


    public new int ShieldHp { get => stats.currShieldHp; private set => stats.currShieldHp = value; }
    public new int Hp { get => stats.currHp; private set => stats.currHp = value; }

    private void Awake()
    {
        _inputSystem = new PlayerInput();
        attackBehav = new AttackBehaviour(playerObject, bulletSpawnPos, bulletPrefab);
        if (current == null) current = this;
        else Destroy(gameObject);
    }
    private void OnEnable()
    {
        _inputSystem.OnEnable();
    }

    private void OnDisable()
    {
        _inputSystem.OnDisable();
    }
    private void Start()
    {
        stats.currHp = stats.maxHp;
        stats.currBullets = stats.maxBullets;
        stats.currFuel = stats.maxFuel;
        stats.currShieldHp = stats.maxShieldHp/2;
        GameEvents.PlayerShieldChanged(ShieldHp, stats.maxShieldHp);
    }

    private void FixedUpdate()
    {
        MoveBehaviour();
        attackBehav.FireBehaviour(stats);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer != gameObject.layer)
        {
            if (col.CompareTag("Bul"))
            {
                TakeDamage(col.gameObject.GetComponent<BulletScript>().damage);
                if (GameManager.instance.usePool)
                {
                    GameManager.instance._bulletPool.Release(col.gameObject);
                }
                else Destroy(col.gameObject);
            }
        }
    }
    void MoveBehaviour()
    {
        var currentVelocity = _rb.velocity;
        

        var velocity = Vector2.SmoothDamp(currentVelocity, _inputSystem.MoveInput * stats.moveSpeed, ref smoothVelocity, .125f);
        _rb.velocity = velocity;
        

        RotateToMouse();
        Boundaries();
    }

    void RotateToMouse()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        var dir = mousePos - playerObject.transform.position;
        var angle = Vector2.SignedAngle(Vector2.right, dir);
        playerObject.transform.eulerAngles = new Vector3(0, 0, angle);
    }


    void Boundaries()
    {
        Vector3 char_pos = transform.position;

        if (char_pos.x > 9.2f) char_pos.x = 9.2f;
        transform.position = char_pos;

        if (char_pos.x < -9.2f) char_pos.x = -9.2f;
        transform.position = char_pos;

        if (char_pos.y > 4.4) char_pos.y = 4.4f;
        transform.position = char_pos;

        if (char_pos.y < -4.4) char_pos.y = -4.4f;
        transform.position = char_pos;
    }
    //void ShieldBoundaries()
    //{
    //    Vector3 shieldPos = shield.transform.position;
    //
    //    if (shieldPos.x > 9.2f) shieldPos.x = 9.2f;
    //    _rb.transform.position = shieldPos;
    //
    //    if (shieldPos.x < -9.2f) shieldPos.x = -9.2f;
    //    _rb.transform.position = shieldPos;
    //
    //    if (shieldPos.y > 4.4) shieldPos.y = 4.4f;
    //    _rb.transform.position = shieldPos;
    //
    //    if (shieldPos.y < -4.4) shieldPos.y = -4.4f;
    //    _rb.transform.position = shieldPos;
    //}


    public override void Heal(int amount)
    {
        Hp += amount;
        if (Hp > stats.maxHp.FinalValue) Hp = stats.maxHp;
        ShieldBehaviour();
        GameEvents.PlayerHpChanged(Hp, stats.maxHp);
    }

    public override void TakeDamage(int damage)
    {
        if (ShieldHp > 0)
        {
            ShieldHp -= damage;
            GameEvents.PlayerShieldChanged(stats.currShieldHp, stats.maxShieldHp);
        }
        if(ShieldHp < 0)
        {
            Hp -= ShieldHp;
            damage -= (damage - ShieldHp);
            ShieldHp = 0;
            GameEvents.PlayerHpChanged(Hp, stats.maxHp);
        }
        if (ShieldHp == 0) Hp -= damage;
        ShieldBehaviour();
        GameEvents.PlayerHpChanged(Hp, stats.maxHp);
        if (Hp <= 0) Die();
    }

    public void ShieldBehaviour()
    {
        if (ShieldHp == 0)
        {
            shield.GetComponent<Collider2D>().enabled = false;
            shield.GetComponent<VisualEffect>().Stop();
        }
        
        else if (!shield.GetComponent<VisualEffect>().HasAnySystemAwake())
        {
            shield.GetComponent<Collider2D>().enabled = true;
            shield.GetComponent<VisualEffect>().Play();
        }
    }


    void Die()
    {
        var exp = GameManager.instance.explosionPool.Get();
        exp.transform.position = transform.position;
        GameEvents.PlayerDied();
        gameObject.SetActive(false);
    }
}



