using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.Rounds;
using MyGame.RoundCollection;
using MyGame.ZombiesScript;
public class GameInfo : MonoBehaviour
{
    public static GameInfo Instance { get; private set; }
    public List<Round> rounds;                // 모든 라운드 정보를 저장하는 리스트
    public RoundCollection roundCollection;
    public float timeBetweenRounds = 5f;      // 라운드 간 대기 시간


    public Vector3[] spawnPoints;
    public ObjectPool poolManager;

    public List<GameObject> aliveZombies = new();
    void Awake()
    {
        // Singleton 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
            SetSpqwnPosition();
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 제거
        }
    }
    public void StartGame()
    {
        // 자동 시작
        StartCoroutine(StartGameRutine());
    }
    IEnumerator StartGameRutine()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(GameInfo.Instance.StartRounds(PlayerSetting.bigRound, PlayerSetting.smallRound));

    }
    public IEnumerator StartRounds(int biground, int smallround)
    {

        if (roundCollection != null)
        {
            Round currentRoundData = roundCollection.rounds[smallround]; // 배열 인덱스는 0부터 시작하므로 -1

           // Debug.Log($"Current Round {smallround} has {currentRoundData.zombiesToSpawn.Count} zombie spawn infos:");

            foreach (var zombieInfo in currentRoundData.zombiesToSpawn)
            {
                for(int i = 0; i < zombieInfo.count; i++)
                {
                    if (GameManager.Instance.CurrentState==GameManager.GameState.GameOver)
                    {
                        yield break; // 코루틴 종료!
                    }
                    SpawnZombie(zombieInfo);
                    yield return new WaitForSeconds(5.0f);
                }
                Debug.Log($"- Zombie: {zombieInfo.zombie}, Count: {zombieInfo.count}");
            }
        }
        yield return new WaitForSeconds(0.5f);
    }

    void SpawnZombie(ZombieSpawnInfo spawnInfo)
    {
        if (spawnInfo.zombie.prefab != null && spawnPoints.Length > 0)
        {
            // 랜덤한 스폰 포인트 선택
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Vector3 selectedSpawnPoint = spawnPoints[randomIndex];

            // 좀비 프리팹을 스폰 위치에 인스턴스화
            //Debug.Log(selectedSpawnPoint + " " + randomIndex+" "+ spawnInfo.zombie.tag);
            GameObject zombie = poolManager.GetFromPool(spawnInfo.zombie.tag, selectedSpawnPoint, Quaternion.identity);
            GameInfo.Instance.aliveZombies.Add(zombie);

            zombie.GetComponent<MonsterSetting>().onDeath = () =>
            {
                GameInfo.Instance.aliveZombies.Remove(zombie);
                TryCheckRoundClear();
            };
            // GameObject spawnedZombie = Instantiate(spawnInfo.zombie.prefab, selectedSpawnPoint, Quaternion.identity);
            // spawnedZombie.name = "CustomZombieName";
        }
        else
        {
            Debug.LogError("ZombieType prefab is not set or no spawn points available.");
        }
    }
    void SetSpqwnPosition()
    {
        spawnPoints = new Vector3[6];
        spawnPoints[0] = new Vector3(10f, 3.5f, 0);
        spawnPoints[1] = new Vector3(10f, 1.92f, 0);
        spawnPoints[2] = new Vector3(10f, 0.44f, 0);
        spawnPoints[3] = new Vector3(10f, -1f, 0);
        spawnPoints[4] = new Vector3(10f, -2.5f, 0);
        spawnPoints[5] = new Vector3(10f, -4.0f, 0);
    }
    void TryCheckRoundClear()
    {
        if (GameInfo.Instance.aliveZombies.Count == 0)
        {
            Debug.Log("🎉 라운드 클리어!");
            GameManager.Instance.ChangeState(GameManager.GameState.RoundClear);
        }
    }
}
