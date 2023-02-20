using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public int speed = 300;
    [SerializeField] SpriteRenderer spriteR;
    [SerializeField] Sprite[] sprites;
    float aliveTimer;
    public int damage;
    public bool turnOff;

    private void OnEnable()
    {
        aliveTimer = 0;
        turnOff = false;
    }
    private void OnDisable()
    {
        spriteR.sprite = null;
    }
    private void FixedUpdate()
    {
        Move();
        aliveTimer += Time.fixedDeltaTime;
        if (aliveTimer > 5f)
        {
            if (GameManager.instance.usePool)
            {
                GameManager.instance._bulletPool.Release(gameObject);
            }
            else Destroy(gameObject);
        }
        if(turnOff == true)
        {
            GameManager.instance._bulletPool.Release(gameObject);
        }
    }
    void Move()
    {
        rb.velocity = speed * Time.fixedDeltaTime * transform.right;
    }


    public void SelectSprite()
    {
        if (gameObject.layer == 6)
        {
            spriteR.sprite = sprites[0];
        }
        else if (gameObject.layer == 7)
        {
            spriteR.sprite = sprites[1];
        }
    }
}
