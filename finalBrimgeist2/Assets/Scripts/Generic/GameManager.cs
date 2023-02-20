using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Pool;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Scene CurrentScene { get => SceneManager.GetActiveScene(); private set { } }

    public ObjectPool<GameObject> _bulletPool;
    public ObjectPool<GameObject> explosionPool;
    public ObjectPool<GameObject> healFxPool;
    public ObjectPool<GameObject> impactPool;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject expPrefab;
    [SerializeField] private GameObject healFxPrefab;
    [SerializeField] private GameObject impactPrefab;
    public bool usePool;
    public bool playerDied;


    private void Awake()
    {
        usePool = true;
        if (instance == null) instance = this;
        else Destroy(gameObject);
        if(CurrentScene.name != "Menu" && _bulletPool == null)
        {
            _bulletPool = new ObjectPool<GameObject>(() =>
            {
                return Instantiate(bulletPrefab);
            }, bullet =>
            {
                bullet.GetComponent<BulletScript>().enabled = true;
                bullet.SetActive(true);
            }, bullet =>
            {
                bullet.GetComponent<BulletScript>().enabled = false;
                bullet.SetActive(false);
            }, bullet =>
            {
                Destroy(bullet);
            }, false, 500, 500
        );
            explosionPool = new ObjectPool<GameObject>(() =>
            {
                return Instantiate(expPrefab);
            }, explosion =>
            {
                explosion.SetActive(true);
            }, explosion =>
            {
                
                explosion.SetActive(false);
            }, explosion =>
            {
                Destroy(explosion);
            }, false, 10, 20
        );
            healFxPool = new ObjectPool<GameObject>(() =>
            {
                return Instantiate(healFxPrefab);
            }, heal =>
            {
                heal.SetActive(true);
            }, heal =>
            {

                heal.SetActive(false);
            }, heal =>
            {
                Destroy(heal);
            }, false, 20, 20
        );
            impactPool = new ObjectPool<GameObject>(() =>
            {
                return Instantiate(impactPrefab);
            }, impact =>
            {
                impact.SetActive(true);
            }, impact =>
            {

                impact.SetActive(false);
            }, impact =>
            {
                Destroy(impact);
            }, false, 40, 40
        );
        }
        DontDestroyOnLoad(gameObject);

        GameEvents.PlayerDeath += PlayerDied;
    }

    void PlayerDied()
    {
        //restart scene / show menu
        Time.timeScale = 0;
        playerDied = true;
    }
}
