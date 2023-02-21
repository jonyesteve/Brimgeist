using UnityEngine;

public class EnemyPackSpawner : MonoBehaviour 
{
    public static void SpawnPack(EnemyPack pack, Transform transform)
    {
        var frontlineQuant = pack.frontlineQuant;
        if (pack.containsHealer)
        {
            pack.BaseEnemyPack = Types.EnemyType.Healer; ;
        }
        pack.positionReference = transform.position;
        pack.PackOrder(frontlineQuant);
        pack.SpawnEnemy(pack.BaseEnemyPack, pack.positionReference);
        for (int i =0; i<frontlineQuant; i++)
        {
            pack.SpawnEnemy(pack.frontlineType, pack.positions[i]);
        }
    }
}

