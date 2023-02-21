using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using static Types;
using System;
using JetBrains.Annotations;

public class EnemyManager : MonoBehaviour
{
    
    public BaseStats[] defaultStats;
    public static EnemyManager current;
    public ObjectPool<GameObject> enemyPool;
    List<GameObject> loadedEnemies = new();
    public List<EnemyPack> enemyPacks = new();
    public Transform enemySpawn;
    private bool _usePool;
    public GameObject enemyPrefab;
    public Sprite[] _sprites;
    [SerializeField] bool _spawnEnemies;
    public EnemyCreator enemyCreator;

    //spawn
    bool _spawnHealer, _spawnTank;
    [SerializeField] float _spawnTimer, waveTimerOffset;
    int currentWave;
    public static event Action WaveChange;

    [Serializable]
    public class Enemy
    {
        public int quantity;
        public EnemyType type;
    }
    [Serializable]
    public class EnemyWave
    {
        public List<Enemy> enemies;
        public List<EnemyPack> enemyPacks;
    }
    public List<EnemyWave> waves;

    private void Awake()
    {
        _usePool = true;
        if (current == null) current = this;
        else Destroy(gameObject);

        enemyPool = new ObjectPool<GameObject>(() =>
        {
            return Instantiate(enemyPrefab);
        }, enemy =>
        {
            enemy.SetActive(true);
            enemy.GetComponent<EnemyController>().enabled = true;

        }, enemy =>
        {
            enemy.SetActive(false);
            enemy.GetComponent<EnemyController>().enabled = false;
        }, enemy =>
        {
            Destroy(enemy);
        }, false, 50, 100);

        enemyCreator = new EnemyCreator(_usePool, enemyPrefab);
        currentWave = 0;
        for(int i=0; i < 15; i++)
        {
            loadedEnemies.Add(enemyPool.Get());
        }
        for (int i = 14; i >= 0; i--)
        {
            enemyPool.Release(loadedEnemies[i]);
            loadedEnemies.Remove(loadedEnemies[i]);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(SpawningRoutine());
    }

    public IEnumerator SpawningRoutine()
    {
        _spawnTimer = 0;
        float tankSpawnTimer = 0;
        var enemies = waves[currentWave].enemies;
        var striker = enemies.Find(enemy => enemy.type == EnemyType.Striker);
        var tanks = enemies.Find(enemy => enemy.type == EnemyType.Tank);
        var warriors = enemies.Find(enemy => enemy.type == EnemyType.Warrior);
        while (_spawnEnemies == true)
        {
            _spawnTimer += Time.fixedDeltaTime;
            tankSpawnTimer += Time.fixedDeltaTime;
            if(_spawnTimer > UnityEngine.Random.Range(1f, 2f))
            {
                if(UnityEngine.Random.Range(0, 1f) > 0.75f)
                {
                    enemyCreator.CreateNewEnemy(warriors.type);
                    warriors.quantity--;
                }
                if(striker.quantity > 0)
                {
                    enemyCreator.CreateNewEnemy(striker.type);
                    striker.quantity--;
                }
                _spawnTimer = 0;
            }
            if(tankSpawnTimer > UnityEngine.Random.Range(5f, 7f))
            {
                enemyCreator.CreateNewEnemy(tanks.type);
                tanks.quantity--;
                tankSpawnTimer = 0;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    Sprite GetSpriteFromType(EnemyType t) => _sprites[(int)t];

    void WaveChanged()
    {
        currentWave++;
        WaveChange();
    }
}


