using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float currentTime;
    public float createTime = 1;
    public GameObject enemyFactory;

    float min = 1;
    float max = 5;

    private void Start()
    {
        createTime = UnityEngine.Random.Range(min, max);
    }
    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > createTime)
        {
            GameObject enemy = Instantiate(enemyFactory);
            enemy.transform.position = transform.position;
            currentTime = 0;
            createTime = UnityEngine.Random.Range(min, max);

        }



    }
}
