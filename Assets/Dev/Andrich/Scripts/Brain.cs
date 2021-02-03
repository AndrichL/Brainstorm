using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrich
{ 
    public class Brain : MonoBehaviour
    {
        [SerializeField] private int m_Points;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<Player>())
            {
                Debug.Log("Hit Player");
                Robin.ScoreManager.instance.AddToScore(m_Points);
            }
            gameObject.SetActive(false);
        }
    }
}
