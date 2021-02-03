using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrich
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float m_MaxHealth = 3;
        private float m_Health;
        private bool m_IsDead;

        [SerializeField] private float m_TimeInvincible = 0.6f;
        private bool m_IsInvincible;

        private void Start()
        {
            m_IsDead = false;
            m_Health = m_MaxHealth;
            GameManager.m_Instance.CurrentPlayer(gameObject);
        }

        public void ChangePlayerVitality(float amount)
        {
            if (amount < 0)
            {
                if (m_IsInvincible)
                {
                    // Player can't be damaged
                    return;
                }

                // Player has been hit
                StartCoroutine(GiveInvincibility(m_TimeInvincible));
            }

            m_Health = Mathf.Clamp(m_Health + amount, 0, m_MaxHealth);

            if (m_Health <= 0 && !m_IsDead)
            {
                Debug.Log("Player is dead");
                KillPlayer();
            }
        }

        private void KillPlayer()
        {
            GameManager.m_Instance.GameIsOver(gameObject);
            m_IsDead = true;
        }

        private IEnumerator GiveInvincibility(float timeInvincible)
        {
            m_IsInvincible = true;
            yield return new WaitForSeconds(timeInvincible);
            m_IsInvincible = false;
        }

        public bool GetIfPlayerIsHurt()
        {
            return m_Health < m_MaxHealth;
        }
    }
}
