using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEffect : MonoBehaviour
{
    [SerializeField] bool decrease, increase;
    private void Start()
    {
        decrease = false;
    }
    private void FixedUpdate()
    {
        ShieldBehaviour();
    }

    void ShieldBehaviour()
    {

        if (transform.localScale.y < 1.2f && !decrease) increase = true;
        if(transform.localScale.y >= 1.2f)
        {
            increase = false;
            decrease = true;
        }
        if (transform.localScale.y <= 1f && decrease)
        {
            decrease = false;
        }

        if (increase)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1.3f, 1.3f, 1.3f), Time.fixedDeltaTime/5);
        }
        if(decrease)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.9f, 0.9f, 0.9f), Time.fixedDeltaTime/5);
        }
        
    }
}
