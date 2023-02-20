using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;
using static Types;
using System;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;

public class EnemyManager : MonoBehaviour
{
    
    public BaseStats[] defaultStats;
    public static EnemyManager current;
    public ObjectPool<GameObject> enemyPool;
    [SerializeField]public List<GameObject> loadedEnemies = new();
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
    public class Enemy : IEnemy
    {
        public int quantity { get => quant; set=>quant = value; }
        public EnemyType type { get => enemyType; set=> enemyType = value;}

        [SerializeField] int quant;
        [SerializeField] EnemyType enemyType;
        [SerializeField] HealerPack healerpack;
        
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

    //EnemyController SpawnEnemy(EnemyType type)
    //{
    //    var enemyObj = enemyCreator.CreateNewEnemy();
    //    enemyObj.AssignValues(defaultStats[(int)type], type, type == EnemyType.Boss1 || type == EnemyType.Boss2, GetSpriteFromType(type));
    //    return enemyObj;
    //}

    //HealerPack SpawnPack(int quantity, int frontlineQuant,EnemyType frontlineType, Transform[] positions)
    //{
    //    List<EnemyController> frontlineEnemies= new List<EnemyController>();
    //    var enemyPack = new HealerPack(quantity, frontlineQuant, frontlineType, positions);
    //    for(int i =0; i<frontlineQuant; i++)
    //    {
    //        enemyPack.quantity--;
    //        frontlineEnemies.Add(SpawnEnemy(frontlineType));
    //    }
    //    SpawnEnemy(EnemyType.Healer);
    //
    //    return enemyPack;
    //}


    //public IEnumerator SpawningRoutine()
    //{
    //    float spawnTimer = 0;
    //    while(_spawnEnemies == true)
    //    {
    //        spawnTimer += Time.fixedDeltaTime;
    //
    //        var currentEnemies = enemyWaves.Find(enemyWave => enemyWave.wave == currentWave).enemies;
    //        var commonEnemy = currentEnemies.Find(enemy => enemy.type == EnemyType.Striker);
    //        if (commonEnemy.quantity != 0 && spawnTimer >1.5f)
    //        {
    //            spawnTimer = 0;
    //            commonEnemy.quantity--;
    //            SpawnEnemy(commonEnemy.type);
    //        }
    //        if(currentEnemies.Contains(currentEnemies.Find(enemy => enemy.type == EnemyType.Healer)))
    //        {
    //            var enemy = currentEnemies.Find(enemy => enemy.type == EnemyType.Healer);
    //            
    //        }
    //
    //        yield return new WaitForFixedUpdate();
    //    }
    //}

    Sprite GetSpriteFromType(EnemyType t) => _sprites[(int)t];

    void WaveChanged()
    {
        currentWave++;
        WaveChange();
    }
}
