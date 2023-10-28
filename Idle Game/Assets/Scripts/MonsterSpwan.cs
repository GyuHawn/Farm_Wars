using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterSpwan : MonoBehaviour
{
    private StageManger smanager;

    public GameObject[] spwaner;
    public GameObject[] monsters;

    public int spwanMonster;

    public bool isSpawning = false; // 현재 소환 중인지 표시하는 변수
    public int activeMonsters = 0; // 활성화된 몬스터 개수

    void Start()
    {
        smanager = GetComponent<StageManger>();
        spwanMonster = 0;
        StartCoroutine(SpawnMonsters());
    }

    void Update()
    {
        if (spwanMonster > 0 && !isSpawning)
        {
            StartCoroutine(SpawnMonsters());
        }
    }

    IEnumerator SpawnMonsters()
    {
        isSpawning = true;

        int spawnCount = Mathf.Min(spwaner.Length, spwanMonster);
        for (int i = 0; i < spawnCount; i++)
        {
            GameObject monsterObj = Instantiate(monsters[smanager.stage % monsters.Length], spwaner[i].transform.position, Quaternion.identity);
            MonsterScript monsterScript = monsterObj.GetComponent<MonsterScript>();
            monsterScript.spawner = this;

            activeMonsters++;
            spwanMonster--;

            yield return new WaitForSeconds(0.5f);
        }

        isSpawning = false;
    }
    public void ResetStage()
    {
        // 모든 활성 몬스터를 파괴합니다.
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject monster in monsters)
        {
            Destroy(monster);
        }

        // 스테이지 및 spwanMonster 카운트를 재설정
        //smanager.stage = smanager.stage;
        spwanMonster = 10;
    }

}