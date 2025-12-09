using System.Collections.Generic;
using UnityEngine;

namespace EntityUtils.PlayerUtils
{
    public class PlayerSpawnPointListScript : MonoBehaviour
    {

        [SerializeField] private List<GameObject> points;

        public GameObject GetObjectByID(int id) { return points[id]; }

    }
}