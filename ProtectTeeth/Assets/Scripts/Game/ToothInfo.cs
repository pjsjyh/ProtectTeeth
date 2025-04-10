using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace MyGame.ToothInfos
{
    [Serializable]
    public enum ToothEnum { toothbrush, toothpaste, autotooth, gargle };


    [Serializable]
    public struct ToothBody
    {
        public int health;
        public float speed;
        public int defense;
        public int attack;
    }
    [CreateAssetMenu(fileName = "NewToothType", menuName = "Tooth/Tooth Type")]
    public class ToothInfo : ScriptableObject
    {
        public ToothEnum toothType;
        public int toothLevel;
        public GameObject prefab;
        public ToothBody toothBody;
        public string tag;
        public int coin;
    }
}

