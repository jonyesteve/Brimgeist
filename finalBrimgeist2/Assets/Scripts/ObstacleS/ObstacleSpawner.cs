using UnityEngine;
using UnityEngine.Pool;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner instance;
    public ObjectPool<GameObject> minePool;
    public ObjectPool<GameObject> stonePool;
    public ObjectPool<GameObject> meteorPool;
    [SerializeField] GameObject minePrefab;
    [SerializeField] GameObject stonePrefab;
    [SerializeField] GameObject meteorPrefab;
    public bool spawnObstacles;

    public void Awake()
    {
        minePool = new ObjectPool<GameObject>(() =>
        {
            return GameObject.Instantiate(minePrefab);
        }, mine =>
        {
            mine.SetActive(true);
        }, mine =>
        {
            mine.SetActive(false);
        }, mine =>
        {
            GameObject.Destroy(mine);
        }, false, 40, 100
        );
        meteorPool = new ObjectPool<GameObject>(() =>
        {
            return GameObject.Instantiate(minePrefab);
        }, meteor =>
        {
            meteor.SetActive(true);
        }, meteor =>
        {
            meteor.SetActive(false);
        }, meteor =>
        {
            GameObject.Destroy(meteor);
        }, false, 10, 20
        ); stonePool = new ObjectPool<GameObject>(() =>
        {
            return GameObject.Instantiate(minePrefab);
        }, stone =>
        {
            stone.SetActive(true);
        }, stone =>
        {
            stone.SetActive(false);
        }, stone =>
        {
            GameObject.Destroy(stone);
        }, false, 8, 15
         );
    }
}
