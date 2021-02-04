using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrich
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> m_Spawnpoints = null;
        [SerializeField] private int m_MaxIncrement = 3;
        private int m_CurrentSpawnpoint; 
        private int m_PreviousSpawnpoint;

        [SerializeField] private float m_MinimumSpawnDelay = 0.5f;
        [SerializeField] private float m_MaxSpawnDelay = 10f;
        [SerializeField] private float m_DecreaseAmount = 1f;
        [SerializeField] private float m_TimeUntilIncreaseSpeed = 30f;
        private float m_CurrentSpawnDelay;
        private float m_SpawnSpeedCountdown;
        private float m_SpawnCountdown;
        private bool m_FirstSpawn;

        private void Start()
        {
            m_CurrentSpawnDelay = m_MaxSpawnDelay;

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

        private void Update()
        {
            DecreaseSpawnDelay();
        }

        private void DecreaseSpawnDelay()
        {
            if(!m_FirstSpawn)
            {
                if(m_SpawnSpeedCountdown <= 0)
                {
                    m_SpawnSpeedCountdown = m_TimeUntilIncreaseSpeed;

                    if(m_CurrentSpawnDelay < m_MaxSpawnDelay * 0.4f)
                    {
                        m_DecreaseAmount *= 0.5f;
                        m_TimeUntilIncreaseSpeed *= 1.3f;
                    }

                    m_CurrentSpawnDelay = Mathf.Clamp(m_CurrentSpawnDelay - m_DecreaseAmount, m_MinimumSpawnDelay, m_MaxSpawnDelay);
                }
            }

            m_SpawnSpeedCountdown = Mathf.Clamp(m_SpawnSpeedCountdown - Time.deltaTime, 0, m_TimeUntilIncreaseSpeed); ;
        }

        private IEnumerator SpawnTimer()
        {
            if(m_FirstSpawn)
            {
                m_SpawnSpeedCountdown = m_TimeUntilIncreaseSpeed;
                m_SpawnCountdown = m_MaxSpawnDelay;
            }

            //float newDelay = m_CurrentSpawnDelay;

            foreach (GameObject spawnpoint in m_Spawnpoints)
            {
                spawnpoint.SetActive(true); // Set each gameobject in the m_Spawnpoints list active
            }

            yield return new WaitForSeconds(m_SpawnCountdown); // Wait x amount of time

            m_SpawnCountdown = m_CurrentSpawnDelay;
            SpawnItem();
        }

        private void SpawnItem()
        {

            int side = Random.Range(-4, 5);
            int increment = Random.Range(1, m_MaxIncrement);
            Transform spawnPos = null;

            if(m_FirstSpawn) // Set a random beginning spawn
            {
                m_CurrentSpawnpoint = Random.Range(0, m_Spawnpoints.Count - 1);
                spawnPos = m_Spawnpoints[m_CurrentSpawnpoint].transform;
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
                SpawnItem(spawnPos);
            }

            m_PreviousSpawnpoint = m_CurrentSpawnpoint; // Set previous spawnpoint
            StartCoroutine(SpawnTimer());
        }

        private void SpawnItem(Transform spawnPos)
        {
            int randomNumber = UnityEngine.Random.Range(0, 100);

            if (randomNumber < 15 && !m_FirstSpawn)
            {
                ObjectPooler.m_Instance.SetActiveFromPool(WhichPrefab.anvil, spawnPos.position, Quaternion.identity);
            }
            else if (randomNumber > 95 && GameManager.m_Instance.GetCurrentPlayer().GetIfPlayerIsHurt() && !m_FirstSpawn)
            {
                ObjectPooler.m_Instance.SetActiveFromPool(WhichPrefab.heart, spawnPos.position, Quaternion.identity);
            }
            else
            {
                ObjectPooler.m_Instance.SetActiveFromPool(WhichPrefab.brain, spawnPos.position, Quaternion.identity);
            }

            if(m_FirstSpawn)
            {
                m_FirstSpawn = false;
            }
        }
    }
}

