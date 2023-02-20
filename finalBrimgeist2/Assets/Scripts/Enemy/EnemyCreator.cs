using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using static Types;

public class EnemyCreator
{
    bool _usePool;
    GameObject _enemyPrefab;
    int _lastSpawnPosY1, _lastSpawnPosY2;
    bool _exchange;

    public EnemyController CreateNewEnemy()
    {
        Vector2 spawnVector = new Vector2(UnityEngine.Random.Range(10f, 11), UnityEngine.Random.Range(4, -4));
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
    public EnemyController CreateNewEnemy(Transform spawnVector)
    {
        EnemyController enemyObj = _usePool ?
            EnemyManager.current.enemyPool.Get().GetComponent<EnemyController>()
            : GameObject.Instantiate(_enemyPrefab).GetComponent<EnemyController>();
        enemyObj.transform.SetPositionAndRotation(spawnVector.position, _enemyPrefab.transform.rotation);
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
}

[Serializable]
public abstract class EnemyPack : IEnemy
{

    public int quantity { get => enemyQuant; set => enemyQuant = value; }
    public EnemyType type { get => enemyType; set => enemyType = value; }
    [SerializeField] int enemyQuant;
    EnemyType enemyType;
    public Transform[] positions;
    public Transform positionReference;

    public EnemyPack(int quantity, EnemyType type, Transform[] positions)
    {
        this.quantity = quantity;
        this.type = type;
        this.positions = positions;
    }

    public virtual void SpawnPack(int quantity)
    {

    }
    protected Sprite GetSpriteFromType(EnemyType t) => EnemyManager.current._sprites[(int)t];
    public void PackOrder(int quantity)
    {
        positions = new Transform[quantity];
        switch (quantity)
        {
            case 1:
                positions[0].position = positionReference.position + new Vector3(1, 0);
                break;
            case 2:
                positions[0].position = positionReference.position + new Vector3(1, 0.7f);
                positions[1].position = positionReference.position + new Vector3(1, -0.7f);
                break;
            case 3:
                positions[2].position = positionReference.position + new Vector3(2, 0);
                goto case 2;
            case 4:
                positions[2].position = positionReference.position + new Vector3(0.7f, 1.5f);
                positions[3].position = positionReference.position + new Vector3(0.7f, -1.5f);
                goto case 2;
        }
    }
    protected EnemyController SpawnEnemy(EnemyType type, Transform position)
    {
        var enemyObj = EnemyManager.current.enemyCreator.CreateNewEnemy(position);
        enemyObj.AssignValues(EnemyManager.current.defaultStats[(int)type], type, type == EnemyType.Boss1 || type == EnemyType.Boss2, GetSpriteFromType(type));
        return enemyObj;
    }
}
[Serializable]
//public class HealerPack : EnemyPack
//{
//    public int frontlineEnemies;
//    public EnemyType frontlineEnemiesType;
//
//    public HealerPack(int quantity, int frontlineEnemiesQuant, EnemyType frontlineEnemiesType, Transform[] positions, EnemyType type = //EnemyType.Healer) : base(quantity, type, positions)
//    {
//        this.frontlineEnemies = frontlineEnemiesQuant;
//        this.frontlineEnemiesType = frontlineEnemiesType;
//    }
//}
public class HealerPack : EnemyPack
{
    public EnemyController[] frontlineEnemies;
    public EnemyType frontlineType;

    public HealerPack(int quantity, int frontlineEnemies, EnemyType frontlineType, Transform[] positions, EnemyType type =
        EnemyType.Healer) : base(quantity, type, positions)
    {
        this.type = type;
        this.frontlineEnemies = new EnemyController[frontlineEnemies];
        this.frontlineType = frontlineType;
        PackOrder(frontlineEnemies);
        for(int i=0; i < frontlineEnemies; i++)
        {
            this.frontlineEnemies[i] = SpawnEnemy(this.type, positions[i]);
        }
        SpawnEnemy(this.type, positionReference);
    }

}
public interface IEnemy
{
    public int quantity { get; set; }
    public EnemyType type { get; set; }
}

