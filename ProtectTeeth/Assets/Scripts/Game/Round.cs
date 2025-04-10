using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.ZombiesScript;
namespace MyGame.Rounds
{

    [System.Serializable]
    public class ZombieSpawnInfo
    {
        public Zombie zombie;            // 좀비 종류
        public int count;                        // 등장할 수량
    }
    [CreateAssetMenu(fileName = "NewRoundList", menuName = "Game/Round List")]
    public class Round : ScriptableObject
    {
        public List<ZombieSpawnInfo> zombiesToSpawn;

    }
  
}
