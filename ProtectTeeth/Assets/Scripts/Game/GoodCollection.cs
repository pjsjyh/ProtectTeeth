using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.ToothInfos;
namespace MyGame.GoodList
{
    [CreateAssetMenu(fileName = "NewGoodList", menuName = "Game/Good List")]

    public class GoodCollection : ScriptableObject
    {
        public List<GameObject> goodPrefab;  // 여러 라운드를 저장
    }
}
