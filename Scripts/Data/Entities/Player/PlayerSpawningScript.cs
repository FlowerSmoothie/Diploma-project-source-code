using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace EntityUtils.PlayerUtils
{
    public class PlayerSpawningScript : MonoBehaviour
    {
        [SerializeField] GameObject player;

        private void Start()
        {
            int ID = FindAnyObjectByType<DataHolderScript>().GetSpawnPoint();


            player.transform.position = FindAnyObjectByType<PlayerSpawnPointListScript>().GetObjectByID(ID).transform.position;
        }
    }
}