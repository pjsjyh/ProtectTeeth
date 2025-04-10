using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace MyGame.ZombiesScript
{
    [Serializable]
    public enum ZombieEnum { purpleVirus, greenVirus, orangeVirus };


    [Serializable]
    public struct ZombieBody
    {
        public int health;
        public float speed;
        public int defense;
        public int attack;
    }
    [CreateAssetMenu(fileName = "NewZombieType", menuName = "Zombie/Zombie Type")]
    public class Zombie : ScriptableObject
    {
        public ZombieEnum zombieType;
        public int zombieLevel;
        public GameObject prefab;
        public ZombieBody zombieBody;
        public int score;
        public string tag;
    }
}


