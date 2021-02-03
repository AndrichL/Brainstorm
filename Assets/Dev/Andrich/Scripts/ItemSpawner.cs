using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Andrich
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> m_Spawnpoints = null;
        [SerializeField] private int m_MaxIncrement = 3;
        private int m_CurrentSpawnpoint; 
        private int m_PreviousSpawnpoint;

        [SerializeField] private float m_SpawnDelay = 0.5f;
        private bool m_FirstSpawn;


        private void Start()
        {
            if(m_Spawnpoints == null)
            {
                Debug.LogError("No gameobjects have been added to the list!");
            }
            else
            {
                m_FirstSpawn = true;
                StartCoroutine(SpawnTimer());
            }
        }

        private IEnumerator SpawnTimer()
        {
            foreach (GameObject spawnpoint in m_Spawnpoints)
            {
                spawnpoint.SetActive(true); // Set each gameobject in the m_Spawnpoints list active
            }

            yield return new WaitForSeconds(m_SpawnDelay); // Wait x amount of time

            SpawnBrain();
        }

        private void SpawnBrain()
        {
            int side = Random.Range(-4, 5);
            int increment = Random.Range(1, m_MaxIncrement);
            Transform spawnPos = null;

            if(m_FirstSpawn) // Set a random beginning spawn
            {
                m_CurrentSpawnpoint = Random.Range(0, m_Spawnpoints.Count - 1);
                spawnPos = m_Spawnpoints[m_CurrentSpawnpoint].transform;

                m_FirstSpawn = false;
            }
            else
            {
                // If side value is bigger than 1 add increment to current spawnpoint value, else remove increment from spawnpoint value
                side = side >= 1 ? m_CurrentSpawnpoint + increment : m_CurrentSpawnpoint - increment;
                m_CurrentSpawnpoint = Mathf.Clamp(side, 0, m_Spawnpoints.Count - 1);

                while(m_PreviousSpawnpoint == m_CurrentSpawnpoint) // While previous spawn point is the same as the current spawnpoint
                {
                    side = Random.Range(-4, 5);
                    side = side >= 1 ? m_CurrentSpawnpoint + increment : m_CurrentSpawnpoint - increment;
                    m_CurrentSpawnpoint = Mathf.Clamp(side, 0, m_Spawnpoints.Count - 1);
                }

                spawnPos = m_Spawnpoints[m_CurrentSpawnpoint].transform; // Set spawnpoint
            }

            if (m_Spawnpoints[m_CurrentSpawnpoint].activeSelf) // If the spawnpoint is active
            {
                m_Spawnpoints[m_CurrentSpawnpoint].SetActive(false); // Turn off the spawnpoint in the hierarchy
                ObjectPooler.m_Instance.SetActiveFromPool(WhichPrefab.brain, spawnPos.position, Quaternion.identity); // Set the chosen prefab active in the hierarchy
            }

            m_PreviousSpawnpoint = m_CurrentSpawnpoint; // Set previous spawnpoint
            StartCoroutine(SpawnTimer());
        }

        private void SpawnItem()
        {
            int randomNumber = UnityEngine.Random.Range(0, 20);
            if (randomNumber > 12)
            {
                if (randomNumber >= 18 && !m_FirstSpawn)
                {
                    ObjectPooler.m_Instance.SetActiveFromPool(WhichPrefab.anvil, transform.position, Quaternion.identity);
                }
                else if (randomNumber == 17 && GameManager.m_Instance.GetCurrentPlayer().GetIfPlayerIsHurt() && !m_FirstSpawn)
                {
                    ObjectPooler.m_Instance.SetActiveFromPool(WhichPrefab.heart, transform.position, Quaternion.identity);
                }
                else
                {
                    ObjectPooler.m_Instance.SetActiveFromPool(WhichPrefab.brain, transform.position, Quaternion.identity);
                }
            }
        }
    }
}

