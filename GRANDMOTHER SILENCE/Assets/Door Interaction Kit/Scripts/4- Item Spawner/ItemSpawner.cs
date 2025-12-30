using UnityEngine;

namespace DoorInteractionKit
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] itemPrefabs = null;
        [SerializeField] private Transform spawnPoint = null;

        public void SpawnItems()
        {
            foreach (GameObject prefab in itemPrefabs)
            {
                Instantiate(prefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);
            }
        }
    }
}


