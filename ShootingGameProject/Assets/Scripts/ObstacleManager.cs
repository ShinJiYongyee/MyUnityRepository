using UnityEditor.Purchasing;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    float currentTime;
    public float createTime = 10;
    public float createTimeOffset=0;
    float selector;                 //생성할 장애물을 선택
    GameObject obstacle;
    public GameObject obstacle0;
    public GameObject obstacle1;
    public GameObject obstacle2;
    public float scrollSpeed = 1.0f;

    float min = 5;
    float max = 20;

    private void Start()
    {
        createTime = UnityEngine.Random.Range(min, max)+createTimeOffset;
        selector = Random.Range(0, 3);
    }
    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > createTime)
        {
            if (selector % 3 == 0)
            {
                obstacle = Instantiate(obstacle0);
            }
            else if (selector % 3 == 1)
            {
                obstacle = Instantiate(obstacle1);
            }
            else
            {
                obstacle = Instantiate(obstacle2);
            }
            obstacle.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            obstacle.transform.rotation = Quaternion.Euler(0, 0, 0);
            obstacle.GetComponent<Obstacle>().scrollSpeed = scrollSpeed;
            currentTime = 0;
            createTime = UnityEngine.Random.Range(min, max);
            selector = Random.Range(0, 3);

        }



    }

}
