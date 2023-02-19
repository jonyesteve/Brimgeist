using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PlayerStatsHandler  : MonoBehaviour
{
    [SerializeField] PlayerData[] characterStats;
    [SerializeField] PlayerController player;

    [Header("UI")]
    [SerializeField] Image panel;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
    }
    private void Start()
    {
        AssignPlayerStats(0);
    }

    public void AssignPlayerStats(int type)
    {
        player.stats = characterStats[type];
    }


}
