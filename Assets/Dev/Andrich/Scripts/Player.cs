using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrich
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float m_MaxHealth = 3;
        [SerializeField] private List<GameObject> m_Hearts = new List<GameObject>();
        private float m_Health;
        private bool m_IsDead;

        [SerializeField] private float m_TimeInvincible = 0.6f;
        private bool m_IsInvincible;

        private SpriteRenderer m_SpriteRenderer;

        private void Awake()
        {
            m_SpriteRenderer = GetComponent<SpriteRenderer>();
        }

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

                Sjoerd.AudioManager.thisAudioManager.Play("Grunt");

                if(m_Health > 1)
                {
                    StartCoroutine(GiveInvincibility(m_TimeInvincible));
                }
            }

            m_Health = Mathf.Clamp(m_Health + amount, 0, m_MaxHealth);
            SetHearts();

            if (m_Health <= 0 && !m_IsDead)
            {
                KillPlayer();
            }
        }

        private void SetHearts()
        {
            if (m_Health == 3)
            {
                for (int i = 0; i < m_Hearts.Count; i++)
                {
                    m_Hearts[i].SetActive(true);
                }
            }
            else if (m_Health == 2)
            {
                m_Hearts[0].SetActive(true);
                m_Hearts[1].SetActive(true);
                m_Hearts[2].SetActive(false);
            }
            else if (m_Health == 1)
            {
                m_Hearts[0].SetActive(true);
                m_Hearts[1].SetActive(false);
                m_Hearts[2].SetActive(false);
            }
            else if (m_Health <= 0)
            {
                m_Hearts[0].SetActive(false);
                m_Hearts[1].SetActive(false);
                m_Hearts[2].SetActive(false);
            }
        }

        private void KillPlayer()
        {
            GameManager.m_Instance.GameIsOver(gameObject);
            m_IsDead = true;
        }

        private IEnumerator GiveInvincibility(float timeInvincible)
        {
            m_SpriteRenderer.color = new Color(1f, 1f, 1f, .5f);

            m_IsInvincible = true;
            Robin.EventManager.instance.BroadcastOnHurtPlayer();

            yield return new WaitForSeconds(timeInvincible);

            m_SpriteRenderer.color = new Color(1f, 1f, 1f, 1f);

            Robin.EventManager.instance.BroadcastOnHurtAnimationFinished();
            m_IsInvincible = false;
        }

        public bool GetIfPlayerIsHurt()
        {
            return m_Health < m_MaxHealth;
        }
    }
}
