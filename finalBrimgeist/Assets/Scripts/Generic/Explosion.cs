using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] ParticleSystem particles;

    private void FixedUpdate()
    {
        if (!particles.IsAlive()) GameManager.instance.explosionPool.Release(gameObject);
    }
}