using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UIElements;
using static Types;

public class EnemyCreator
{
    readonly bool _usePool;
    readonly GameObject _enemyPrefab;
    int _lastSpawnPosY1, _lastSpawnPosY2;
    bool _exchange;

    public EnemyController CreateNewEnemy()
    {
        Vector2 spawnVector = new(UnityEngine.Random.Range(10f, 11), UnityEngine.Random.Range(4, -4));
        while (spawnVector.y == _lastSpawnPosY1 || spawnVector.y == _lastSpawnPosY2) spawnVector.y = UnityEngine.Random.Range(4, -4);
        if (_exchange)
        {
            _exchange = false;
            _lastSpawnPosY1 = (int) spawnVector.y;
        }
        else
        {
            _exchange = true;
            _lastSpawnPosY2 = (int) spawnVector.y;
        }
        EnemyController enemyObj = _usePool ?
            EnemyManager.current.enemyPool.Get().GetComponent<EnemyController>() 
            : GameObject.Instantiate(_enemyPrefab).GetComponent<EnemyController>();
        enemyObj.transform.SetPositionAndRotation(spawnVector, _enemyPrefab.transform.rotation);
        return enemyObj;
    }
    public EnemyController CreateNewEnemy(Vector3 spawnVector)
    {
        Debug.Log(spawnVector);
        EnemyController enemyObj = _usePool ?
            EnemyManager.current.enemyPool.Get().GetComponent<EnemyController>()
            : GameObject.Instantiate(_enemyPrefab).GetComponent<EnemyController>();
        enemyObj.transform.SetPositionAndRotation(spawnVector, _enemyPrefab.transform.rotation);
        return enemyObj;
    }

    public EnemyCreator(bool usePool, GameObject enemyPrefab)
    {
        _exchange = false;
        _lastSpawnPosY1 = int.MinValue;
        _lastSpawnPosY2 = int.MinValue;
        _usePool = usePool;
        _enemyPrefab = enemyPrefab;
    }

    public Sprite GetSpriteFromType(EnemyType t) => EnemyManager.current._sprites[(int)t];
    public EnemyController SpawnEnemy(EnemyType type, Vector3 position)
    {
        var manager = EnemyManager.current;
        var enemyObj = manager.enemyCreator.CreateNewEnemy(position);
        enemyObj.AssignValues(manager.defaultStats[(int)type], type, type == EnemyType.Boss1 || type == EnemyType.Boss2, GetSpriteFromType(type));
        return enemyObj;
    }
    public EnemyController SpawnEnemy(EnemyType type)
    {
        var manager = EnemyManager.current;
        var enemyObj = manager.enemyCreator.CreateNewEnemy();
        enemyObj.AssignValues(manager.defaultStats[(int)type], type, type == EnemyType.Boss1 || type == EnemyType.Boss2, GetSpriteFromType(type));
        return enemyObj;
    }
}

