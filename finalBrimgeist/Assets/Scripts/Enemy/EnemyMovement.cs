using UnityEngine;

public class EnemyMovement
{
    Rigidbody2D rb;
    public float speed;
    public int enemyType;
    public void IntialMovement()
    {
        rb.velocity = new Vector2(-6, 0);
    }

    void MoveBehaviour(char type, params Transform[] locations)
    {
        //transform.Translate(Vector3.right, null);
    }

    public EnemyMovement(Rigidbody2D rb, int type)
    {
        this.rb = rb;
        enemyType = type;
    }
}
