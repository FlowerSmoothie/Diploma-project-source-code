using System;
using System.Collections.Generic;
using Overworld.Interfaces;
using UnityEngine;
using Utils.Classes;

namespace Overworld.Clues
{
    [Serializable]
    public class ClueInDiaryInfo : MonoBehaviour
    {
        ClueInDiaryInfoContainer info;

        public ClueInDiaryInfoContainer GetInfo() { return info; }

    }
}
