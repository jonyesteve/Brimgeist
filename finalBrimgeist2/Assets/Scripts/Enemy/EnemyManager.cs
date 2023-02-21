using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using static Types;
using System;

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
    float _spawnTimer, waveTimerOffset;
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

        }, enemy =>
        {
            enemy.SetActive(false);
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
        float spawnTimer = 0;
        var enemy = waves[currentWave].enemies.Find(enemy => enemy.type == EnemyType.Striker);
        while (_spawnEnemies == true)
        {
            spawnTimer += Time.fixedDeltaTime;
            if(spawnTimer > UnityEngine.Random.Range(0.5f, 2f))
            {
                spawnTimer = 0;
                if(enemy.quantity > 0)
                {
                    enemyCreator.SpawnEnemy(enemy.type);
                    enemy.quantity--;
                }              
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


