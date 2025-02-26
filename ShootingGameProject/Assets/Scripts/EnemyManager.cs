using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float currentTime;
    public float createTime = 1;
    public GameObject enemyFactory;

    float min = 1;
    float max = 64;

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
            enemy.transform.position = new Vector3(transform.position.x, transform.position.y, 0);   
            enemy.transform.rotation = Quaternion.Euler(0,0,0);
            currentTime = 0;
            createTime = UnityEngine.Random.Range(min, max);

        }



    }
}
