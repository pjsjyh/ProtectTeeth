using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.Rounds;
using MyGame.RoundCollection;
using MyGame.ZombiesScript;
public class GameInfo : MonoBehaviour
{
    public static GameInfo Instance { get; private set; }
    public List<Round> rounds;                // ��� ���� ������ �����ϴ� ����Ʈ
    public RoundCollection roundCollection;
    public float timeBetweenRounds = 5f;      // ���� �� ��� �ð�


    public Vector3[] spawnPoints;
    public ObjectPool poolManager;
    void Awake()
    {
        // Singleton ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
            SetSpqwnPosition();
        }
        else
        {
            Destroy(gameObject); // �ߺ��� �ν��Ͻ� ����
        }
    }
    public void StartGame()
    {
        // �ڵ� ����
        StartCoroutine(StartGameRutine());
    }
    IEnumerator StartGameRutine()
    {
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(GameInfo.Instance.StartRounds(0, 0));

    }
    public IEnumerator StartRounds(int biground, int smallround)
    {

        if (roundCollection != null)
        {
            Round currentRoundData = roundCollection.rounds[smallround]; // �迭 �ε����� 0���� �����ϹǷ� -1

           // Debug.Log($"Current Round {smallround} has {currentRoundData.zombiesToSpawn.Count} zombie spawn infos:");

            foreach (var zombieInfo in currentRoundData.zombiesToSpawn)
            {
                for(int i = 0; i < zombieInfo.count; i++)
                {
                    if (GameManager.Instance.CurrentState==GameManager.GameState.GameOver)
                    {
                        yield break; // �ڷ�ƾ ����!
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
            // ������ ���� ����Ʈ ����
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Vector3 selectedSpawnPoint = spawnPoints[randomIndex];

            // ���� �������� ���� ��ġ�� �ν��Ͻ�ȭ
            //Debug.Log(selectedSpawnPoint + " " + randomIndex+" "+ spawnInfo.zombie.tag);
            GameObject zombie = poolManager.GetFromPool(spawnInfo.zombie.tag, selectedSpawnPoint, Quaternion.identity);

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
}
