using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Andrich
{ 
    public enum WhichPrefab
    { 
        brain,
        anvil,
        heart
    }

    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler m_Instance { get; private set; }

        private Dictionary<WhichPrefab, Queue<GameObject>> m_PoolDictionary;
        [SerializeField] private List<Pool> m_Pools = new List<Pool>();
        private GameObject m_ObjectToSetActive;

        [System.Serializable]
        public class Pool
        {
            [SerializeField] private string m_ElementName = "Name";
            public WhichPrefab m_WhichPrefab = WhichPrefab.brain;
            public GameObject m_Prefab = null;
            public Transform m_Parent = null;
            public int m_CopyAmount = 50;
        }

        private void Awake()
        {
        
            if (m_Instance == null)
            {
                m_Instance = this;
            }
            else if (m_Instance != null)
            {
                Destroy(this);
            }

            m_PoolDictionary = new Dictionary<WhichPrefab, Queue<GameObject>>();

            foreach (Pool pool in m_Pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();
                for (int i = 0; i < pool.m_CopyAmount; i++)
                {
                    GameObject copy = Instantiate(pool.m_Prefab, pool.m_Parent);
                    copy.SetActive(false);

                    objectPool.Enqueue(copy);
                }

                m_PoolDictionary.Add(pool.m_WhichPrefab, objectPool);
            }
        }

        public GameObject SetActiveFromPool(WhichPrefab prefabEnum, Vector2 position, Quaternion rotation)
        {
            if (!m_PoolDictionary.ContainsKey(prefabEnum))
            {
                Debug.LogError("De enum: " + prefabEnum + " staat nog niet in één van de elementen in de Inspector!");
                return null;
            }

            m_ObjectToSetActive = m_PoolDictionary[prefabEnum].Dequeue();

            m_ObjectToSetActive.SetActive(true);
            m_ObjectToSetActive.transform.position = position;
            m_ObjectToSetActive.transform.rotation = rotation;

            m_PoolDictionary[prefabEnum].Enqueue(m_ObjectToSetActive);

            return m_ObjectToSetActive;
        }
    }
}
