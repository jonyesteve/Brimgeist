using System.Collections;
using UnityEngine;

public class Meteor : Obstacle
{
    public int hp;
    bool shaking;
    int shakeAmount;
    int shakeHp;
    [SerializeField] SpriteRenderer spriteR;

    private void OnEnable()
    {
        spriteR.sprite = sprite;
        rb.velocity = speed * -Vector3.right;
    }

    private void FixedUpdate()
    {
        Behaviour();
    }

    new void Behaviour()
    {
        rb.velocity = Vector2.Lerp(rb.velocity, speed * -Vector3.right, Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Plr"))
        {
            PlayerController.current.TakeDamage(damage);
        }
        if (col.gameObject.CompareTag("Bul"))
        {
            TakeDamage(col.GetComponent<BulletScript>().damage);
        }
    }

    public sealed override void TakeDamage(int damage)
    {
        hp -= damage;
        shakeHp = 0;
        shakeHp += damage;
        if (shakeHp > 20)
        {
            Shake();
            shakeHp = 0;
        }
        rb.velocity *= 0.3f;
    }

    IEnumerator Shake()
    {
        Vector3 originalPos = transform.position;
        float timer = 0;
        rb.AddForce(Random.insideUnitCircle * 3, ForceMode2D.Impulse);
        rb.velocity *= 0.5f;
        if(shaking == false)
        {
            shaking = true;
            while (timer <= 0.35f)
            {
                Vector3 newPos = originalPos + Random.insideUnitSphere * (Time.fixedDeltaTime * shakeAmount);
                newPos.z = transform.position.z;
                transform.position = newPos;
                timer += Time.fixedDeltaTime;
                yield return new WaitForFixedUpdate();
            }
        }

        while(transform.position != originalPos)
        {
            transform.position = Vector2.Lerp(transform.position, originalPos, Time.fixedDeltaTime * 2);
            yield return new WaitForFixedUpdate();
        }
        shaking = false;
    }
}

public class BlastMine : Obstacle
{
    private void OnEnable()
    {
        speed = 3;
        damage = 20;
    }
    private void FixedUpdate()
    {
        Behaviour();
    }

    public override void TakeDamage(int damage)
    {
        
    }

}


