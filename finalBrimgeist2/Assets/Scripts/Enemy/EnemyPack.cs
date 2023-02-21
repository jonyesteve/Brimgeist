using System;
using System.Collections.Generic;
using UnityEngine;
using static Types;
//[Serializable]
//public abstract class EnemyPack 
//{
//    protected Transform positionReference;
//    protected Transform[] positions;
//    public int enemyQuant;
//    public EnemyType enemyType;
//    
//
//    public EnemyPack(int quantity, EnemyType type, Transform[] positions)
//    {
//        this.enemyQuant= quantity;
//        this.enemyType = type;
//        this.positions = positions;
//    }
//
//    public virtual void SpawnPack(int quantity)
//    {
//
//    }
//    protected Sprite GetSpriteFromType(EnemyType t) => EnemyManager.current._sprites[(int)t];
//    public void PackOrder(int quantity)
//    {
//        positions = new Transform[quantity];
//        switch (quantity)
//        {
//            case 1:
//                positions[0].position = positionReference.position + new Vector3(1, 0);
//                break;
//            case 2:
//                positions[0].position = positionReference.position + new Vector3(1, 0.7f);
//                positions[1].position = positionReference.position + new Vector3(1, -0.7f);
//                break;
//            case 3:
//                positions[2].position = positionReference.position + new Vector3(2, 0);
//                goto case 2;
//            case 4:
//                positions[2].position = positionReference.position + new Vector3(0.7f, 1.5f);
//                positions[3].position = positionReference.position + new Vector3(0.7f, -1.5f);
//                goto case 2;
//        }
//    }
//    protected EnemyController SpawnEnemy(EnemyType type, Transform position)
//    {
//        var enemyObj = EnemyManager.current.enemyCreator.CreateNewEnemy(position);
//        enemyObj.AssignValues(EnemyManager.current.defaultStats[(int)type], type, type == EnemyType.Boss1 || type == EnemyType.Boss2, //GetSpriteFromType(type));
//        return enemyObj;
//    }
//    protected EnemyController SpawnEnemy(EnemyType type)
//    {
//        var enemyObj = EnemyManager.current.enemyCreator.CreateNewEnemy();
//        enemyObj.AssignValues(EnemyManager.current.defaultStats[(int)type], type, type == EnemyType.Boss1 || type == EnemyType.Boss2, //GetSpriteFromType(type));
//        return enemyObj;
//    }
//}
////public class HealerPack : EnemyPack
////{
////    public int frontlineEnemies;
////    public EnemyType frontlineEnemiesType;
////
////    public HealerPack(int quantity, int frontlineEnemiesQuant, EnemyType frontlineEnemiesType, Transform[] positions, EnemyType type = ////EnemyType.Healer) : base(quantity, type, positions)
////    {
////        this.frontlineEnemies = frontlineEnemiesQuant;
////        this.frontlineEnemiesType = frontlineEnemiesType;
////    }
////}
//[Serializable]
//public class HealerPack : EnemyPack
//{
//    public EnemyType frontlineType;
//
//    public HealerPack(int frontlineEnemies, EnemyType frontlineType, Transform[] positions, EnemyType type =
//        EnemyType.Healer) : base(frontlineEnemies, type, positions)
//    {
//        this.enemyType = type;
//        this.frontlineType = frontlineType;
//        var healer = SpawnEnemy(this.enemyType);
//        positionReference = healer.gameObject.transform;
//        PackOrder(frontlineEnemies);
//        for(int i=0; i < frontlineEnemies; i++)
//        {
//            SpawnEnemy(frontlineType, positions[i]);
//        }
//    }
//
//}

[CreateAssetMenu]
[Serializable]
public class EnemyPack : ScriptableObject
{
    public int EnemyQuant
    {
        get => enemyQuant;
        set
        {
            if (value > 2) enemyQuant = 2;
            else if (value <= 0) enemyQuant = 1;
            else enemyQuant = value;
        }
    }
    [Range(1,1)]
    [SerializeField] int enemyQuant;
    public EnemyType BaseEnemyPack
    {
        get => enemyType;
        set
        {
            //if(value == EnemyType.Tank || value == EnemyType.Striker || value == EnemyType.Warrior)
            //{
            //    Console.WriteLine("Cant assign tank, striker or warrior to base enemy on the pack, assigned Healer instead");
            //    enemyType = EnemyType.Healer;
            //}
            enemyType = EnemyType.Healer;
        }
    }
    EnemyType enemyType;
    public bool containsHealer;
    public bool containsTank;
    [Range(1,4)]
    public int frontlineQuant;
    public EnemyType frontlineType;

    public Vector3 positionReference;
    public List<Vector3> positions;

    public void PackOrder(int quantity)
    {
        positions = new List<Vector3>();

        switch (quantity)
        {
            case 1:
                var position = positionReference + new Vector3(-1, 0, 0);
                positions.Add(position);
                break;
            case 2:
                var position2 = positionReference + new Vector3(-1, 0.7f, 0);
                var position3 = positionReference + new Vector3(-1, -0.7f, 0);
                positions.Add(position2);
                positions.Add(position3);
                break;
            case 3:
                var position4 = positionReference + new Vector3(-2, 0, 0);
                positions.Add(position4);   
                goto case 2;
            case 4:
                position4 = positionReference + new Vector3(-0.7f, 1.5f, 0);
                var position5 = positionReference + new Vector3(-0.7f, -1.5f, 0);
                positions.Add(position4);
                positions.Add(position5);
                goto case 2;
        }

    }
    public Sprite GetSpriteFromType(EnemyType t) => EnemyManager.current._sprites[(int)t];
}

