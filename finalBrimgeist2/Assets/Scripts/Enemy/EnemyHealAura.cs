using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealAura : MonoBehaviour
{
    [SerializeField] Collider2D col;
    IEnumerator Healing()
    {
        while (gameObject.activeSelf)
        {
            col.enabled = false;
            yield return new WaitForSeconds(0.5f);
            col.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(Healing());
    }
    private void OnDisable()
    {
        StopCoroutine(Healing());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(col.gameObject.layer == 7)
        {
            col.GetComponent<EnemyController>().Heal(5);
        }
    }
}
