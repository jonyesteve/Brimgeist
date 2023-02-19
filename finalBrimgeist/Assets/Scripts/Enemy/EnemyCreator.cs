using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class EnemyCreator
{
    bool _usePool;
    GameObject _enemyPrefab;
    int _lastSpawnPosY1, _lastSpawnPosY2;
    bool exchange;

    public EnemyController CreateNewEnemy()
    {
        Vector2 spawnVector = new Vector2(Random.Range(10f, 11), Random.Range(4, -4));
        while (spawnVector.y == _lastSpawnPosY1 || spawnVector.y == _lastSpawnPosY2) spawnVector.y = Random.Range(4, -4);
        if (exchange)
        {
            exchange = false;
            _lastSpawnPosY1 = (int) spawnVector.y;
        }
        else
        {
            exchange = true;
            _lastSpawnPosY2 = (int) spawnVector.y;
        }
        EnemyController enemyObj = _usePool ?
            EnemyManager.current.enemyPool.Get().GetComponent<EnemyController>() 
            : Object.Instantiate(_enemyPrefab).GetComponent<EnemyController>();
        enemyObj.transform.SetPositionAndRotation(spawnVector, _enemyPrefab.transform.rotation);
        return enemyObj;
    }

    public EnemyCreator(bool usePool, GameObject enemyPrefab)
    {
        exchange = false;
        _lastSpawnPosY1 = int.MinValue;
        _lastSpawnPosY2 = int.MinValue;
        _usePool = usePool;
        _enemyPrefab = enemyPrefab;
    }
}
