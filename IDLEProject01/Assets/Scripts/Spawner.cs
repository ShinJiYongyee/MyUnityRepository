using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //몬스터를 맵에 일정 수 만큼 수 초 간격으로 소환
    public GameObject monster_prefab;
    public int monster_count;
    public float monster_spawn_time;
    public float summon_radius = 5.0f;    //소환 반경
    public float min_spawn_distance = 3f;     //최소 소환 거리

    //생성된 몬스터/플레이어
    public static List<Monster> monster_list = new List<Monster>();
    public static List<Player> player_list = new List<Player>();

    private void Start()
    {
        StartCoroutine("SpawnMonsterPooling");
    }

    /// <summary>
    /// 일반적인 생성 방법
    /// </summary>
    IEnumerator SpawnMonster()
    {
        Vector3 pos;        //생성 좌표

        for (int i = 0; i < monster_count; i++)
        {
            //영점에서 원형으로 일정 거리 떨어진 위치에 몬스터 스폰 장소 설정
            pos = Vector3.zero + Random.insideUnitSphere * summon_radius;

            pos.y = 0.0f;   //공중/지하 생성 방지

            //소환 거리가 최소치 이하일 경우 재생성
            while (Vector3.Distance(pos, Vector3.zero)<=min_spawn_distance)
            {
                //영점에서 원형으로 일정 거리 떨어진 위치에 몬스터 스폰 장소 설정
                pos = Vector3.zero + Random.insideUnitSphere * summon_radius;

                pos.y = 0.0f;   //공중/지하 생성 방지

            }
            //몬스터 프리팹을 정해진 위치에(pos) 회전 없이 생성
            GameObject go = Instantiate(monster_prefab,pos,Quaternion.identity);
        }
        //스폰 타임만큼 기다린 다음 다시 코루틴 실행
        yield return new WaitForSeconds(monster_spawn_time);
        StartCoroutine("SpawnMonster");
    }  

    /// <summary>
    /// 오브젝트 풀링 기법으로 만드는 방법
    /// </summary>
    IEnumerator SpawnMonsterPooling()
    {
        Vector3 pos;        //생성 좌표

        for (int i = 0; i < monster_count; i++)
        {
            //영점에서 원형으로 일정 거리 떨어진 위치에 몬스터 스폰 장소 설정
            pos = Vector3.zero + Random.insideUnitSphere * summon_radius;

            pos.y = 0.0f;   //공중/지하 생성 방지

            //소환 거리가 최소치 이하일 경우 재생성
            while (Vector3.Distance(pos, Vector3.zero) <= min_spawn_distance)
            {
                //영점에서 원형으로 일정 거리 떨어진 위치에 몬스터 스폰 장소 설정
                pos = Vector3.zero + Random.insideUnitSphere * summon_radius;

                pos.y = 0.0f;   //공중/지하 생성 방지

            }

            //전달할 함수가 없는 경우(일반 생성)
            //var go = BaseManager.POOL.PoolObject("Monster").GetGameObject();

            //전달할 함수가 있는 경우(Action<GameObject>)
            //액션을 통해 기능 처리
            var go = BaseManager.POOL.PoolObject("Monster").GetGameObject((value) =>
            {
                //value.GetComponent<Monster>().MonsterSample();
                value.transform.position = pos;
                value.transform.LookAt(Vector3.zero);

                monster_list.Add(value.GetComponent<Monster>());

            });

            //StartCoroutine(ReturnMonsterPooling(go));

        }


        //스폰 타임만큼 기다린 다음 다시 코루틴 실행
        yield return new WaitForSeconds(monster_spawn_time);
        StartCoroutine("SpawnMonsterPooling");
    }

    //풀링한 값 반납, 스폰한 몬스터 파괴
    IEnumerator ReturnMonsterPooling(GameObject go)
    {
        yield return new WaitForSeconds(monster_spawn_time);
        BaseManager.POOL.pool_dict["Monster"].ObjectReturn(go);
    }

}
