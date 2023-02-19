using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using static Types;
using System;
using System.Linq;

public class EnemyManager : MonoBehaviour
{
    
    public BaseStats[] defaultStats;
    public static EnemyManager current;
    public ObjectPool<GameObject> enemyPool;
    private bool _usePool;
    public GameObject enemyPrefab;
    [SerializeField] Sprite[] _sprites;
    [SerializeField] bool _spawnEnemies;
    EnemyCreator enemySpawner;

    //spawn
    bool _spawnHealer, _spawnTank;
    float _spawnTimer, waveTimerOffset;
    int currentWave;
    public static event Action WaveChange;

    [Serializable]
    public class Enemy
    {
        public int count;
        public EnemyType enemyType;
    }
    [Serializable]
    public class EnemyWave
    {
        public int wave;
        public List<Enemy> enemies;
    }
    [SerializeField] List<EnemyWave> enemyWaves;
    //enemies.Find(enemy => enemy.wave == 0).enemies.Find(i => i.enemyType == EnemyType.Striker).count--;

    

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

        enemySpawner = new EnemyCreator(_usePool, enemyPrefab);
        currentWave = 0;
        WaveChange += SpawnBehaviour;
    }

    private void FixedUpdate()
    {
        _spawnTimer += Time.fixedDeltaTime;
        if (_spawnTimer > 2 + waveTimerOffset)
        {
            _spawnTimer = 0;
            WaveChanged();
        }
    }

    void SpawnEnemy(EnemyType type, bool isBoss)
    {
        var enemyObj = enemySpawner.CreateNewEnemy();
        enemyObj.AssignValues(defaultStats[(int)type], type, isBoss, GetSpriteFromType(type));
    }

    void SpawnBehaviour()
    {
        foreach(var enemy in enemyWaves.Find(enemyWave => enemyWave.wave == currentWave).enemies)
        {
            
            if(enemy.count != 0)
            {
                if (enemy.enemyType == EnemyType.Boss1 || enemy.enemyType == EnemyType.Boss2)
                {
                    SpawnEnemy(enemy.enemyType, true);
                    enemy.count--;
                }
                else
                {
                    SpawnEnemy(enemy.enemyType, false);
                    enemy.count--;
                }
            }
        }
    }

    Sprite GetSpriteFromType(EnemyType t) => _sprites[(int)t];

    void WaveChanged()
    {
        currentWave++;
        WaveChange();
    }
}
