
using UnityEngine;
using UnityEngine.InputSystem;
using static Utilities.General;

public class LaserEffect : MonoBehaviour
{
    [SerializeField] InputActionReference qRef;
    [SerializeField] ParticleSystem mainLaser;
    [SerializeField] ParticleSystem bubble;
    [SerializeField] Collider2D laserCol;
    public bool isActive;
    [SerializeField] Animator ani;
    float laserTimer;
    readonly InputActions input;

    void Laser(InputAction.CallbackContext ctx)
    {
        if (!isActive)
        {
            isActive = true;
            ani.SetTrigger(0);
            laserTimer = 0;
        }
        
    }
    private void OnEnable()
    {
        input.Spells.CastSpell.started += Laser;
    }
    private void OnDisable()
    {
        input.Spells.CastSpell.started -= Laser;
    }

    void LaserBehaviour()
    {
        laserTimer += Time.fixedDeltaTime;
        if (isActive)
        {
            if (laserTimer > 3)
            {
                ani.SetTrigger(1);
            }
            if(IsAnimationPlaying(ani, "idle"))
            {
                isActive = false;
                laserTimer = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        LaserBehaviour();
    }

    
}
