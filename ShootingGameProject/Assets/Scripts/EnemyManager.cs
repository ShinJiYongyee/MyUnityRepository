using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float currentTime;
    float createTime = 1;
    public GameObject enemyFactory;

    float min = 4;
    float max = 64;

    private void Start()
    {
        if(ScoreManager.currentStage <= 4)
        {
            max -= ScoreManager.currentStage * 4;
        }
        createTime = UnityEngine.Random.Range(min, max);
    }
    private void Update()
    {
        if (!GameManager.instance.isGameOver && !GameManager.instance.isGameClear)
        {
            RandomCreateEnemy();
        }

    }
    void RandomCreateEnemy()
    {
        currentTime += Time.deltaTime;

        if (currentTime > createTime)
        {
            GameObject enemy = Instantiate(enemyFactory);
            enemy.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
            currentTime = 0;
            createTime = UnityEngine.Random.Range(min, max);

        }
    }
}
