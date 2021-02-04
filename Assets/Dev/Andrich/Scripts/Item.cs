using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrich
{ 

    public class Item : MonoBehaviour
    {
        [SerializeField] private CollectibleSettings m_Settings;
        private Animator m_Animator;
        private Rigidbody2D m_Rigidbody;
        private bool m_AllowCollision;

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            ResetItem();
        }

        private void Update()
        {
            if(m_Settings.m_WhichItem == WhichCollectible.anvil)
            {
                if(m_Rigidbody.position.y < -2.9f)
                {
                    Sjoerd.AudioManager.thisAudioManager.Play("Anvil");
                    m_Animator.SetBool("HitGround", true);
                    m_Rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
                    GameManager.m_Instance.DeactivateItem(gameObject, m_Settings.m_AnimationLength);
                }
            }
        }

        private void ResetItem()
        {
            m_Animator.SetBool("HitGround", false);
            m_Rigidbody.constraints = RigidbodyConstraints2D.None;
            m_Rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
            m_Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            m_AllowCollision = true;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(m_AllowCollision)
            {
                Player player = col.GetComponent<Player>();
                Ground ground = col.GetComponent<Ground>();

                if (player)
                {
                    switch (m_Settings.m_WhichItem)
                    {
                        case WhichCollectible.brain:
                            Sjoerd.AudioManager.thisAudioManager.Play("Catch");

                            gameObject.SetActive(false);

                            break;
                        case WhichCollectible.heart:

                            player.ChangePlayerVitality(m_Settings.m_HealAmount);
                            Sjoerd.AudioManager.thisAudioManager.Play("1up");
                            gameObject.SetActive(false);

                            break;
                        case WhichCollectible.anvil:

                            HurtPlayer(player);

                            break;
                        default:
                            break;
                    }

                    Robin.ScoreManager.instance.AddToScore(m_Settings.m_PointsAmount);
                    
                }

                if(ground)
                {
                    m_Animator.SetBool("HitGround", true);

                    switch (m_Settings.m_WhichItem)
                    {
                        case WhichCollectible.brain:
                            Sjoerd.AudioManager.thisAudioManager.Play("Drop");

                            HurtPlayer(player);

                            break;
                        case WhichCollectible.heart:

                            Sjoerd.AudioManager.thisAudioManager.Play("Drop");

                            break;
                        default:
                            break;
                    }

                    m_Rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
                    GameManager.m_Instance.DeactivateItem(gameObject, m_Settings.m_AnimationLength);
                }
            }

            m_AllowCollision = false;
        }

        private void HurtPlayer(Player player)
        {
            if (player == null)
            {
                player = GameManager.m_Instance.GetCurrentPlayer();

                if(player == null)
                {
                    Debug.LogError("Current player isn't available!");
                    return;
                }
            }

            Sjoerd.AudioManager.thisAudioManager.Play("Grunt");

            player.ChangePlayerVitality(-m_Settings.m_DamageAmount);
        }
    }
}
