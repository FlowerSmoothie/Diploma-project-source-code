using System;
using UnityEngine;

namespace EntityUtils.PlayerUtils
{
    [Serializable]
    public class PairOfDoorAndSpawnpoint
    {
        public GameObject point;
        public int doorID;

        public int GetID() { return doorID; }
        public GameObject GetSpawnPoint() { return point; }
    }
}