using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    public Rigidbody2D rb;
    public int speed;
    public int damage;
    public Sprite sprite;

    public virtual void Behaviour()
    {
        rb.velocity = speed * Time.deltaTime * -Vector2.right;
    }
    public abstract void TakeDamage(int damage);


    public void AssignValues(int damage, int speed, Sprite sprite)
    {
        this.damage = damage;
        this.speed = speed;
        this.sprite = sprite;
    }
}

public enum Obstacles
{
    Meteor,
    Stone,
    Mines
}


