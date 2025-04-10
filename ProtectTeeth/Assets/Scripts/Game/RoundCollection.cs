using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame.Rounds;

namespace MyGame.RoundCollection
{
    [CreateAssetMenu(fileName = "NewRoundCollection", menuName = "Game/Round Collection")]

    public class RoundCollection : ScriptableObject
    {
        public List<Round> rounds;  // 여러 라운드를 저장
    }
}

