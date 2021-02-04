using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrich
{ 

    public class Item : MonoBehaviour
    {
        [SerializeField] private Animator m_Animator;
        [SerializeField] private CollectibleSettings m_Settings;

        private void Awake()
        {
            m_Animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            m_Animator.SetBool("HitGround", false);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Player player = col.GetComponent<Player>();
            float animationLength = 0;

            if (player)
            {
                switch (m_Settings.m_WhichItem)
                {
                    case WhichCollectible.brain:
                        // Play player collected brian effect
                        // Play player collected brian sound

                        break;
                    case WhichCollectible.heart:
                        //Play player collected heart effect
                        // Play player collected heart sound

                        player.ChangePlayerVitality(m_Settings.m_HealAmount);

                        break;
                    case WhichCollectible.anvil:
                        HurtPlayer(player);

                        break;
                    default:
                        break;
                }

                Robin.ScoreManager.instance.AddToScore(m_Settings.m_PointsAmount);

            }
            else // Ground has been hit
            {
                m_Animator.SetBool("HitGround", true);

                switch (m_Settings.m_WhichItem)
                {
                    case WhichCollectible.brain:
                        // Play brain hits ground sound
                        // Deactivate at the end of the clip length

                        HurtPlayer(player);

                        break;
                    case WhichCollectible.heart:

                        // Play heart hits ground sound
                        // Deactivate at the end of the clip length

                        break;
                    case WhichCollectible.anvil:

                        // Play anvil hits ground sound
                        // Deactivate at the end of the clip length

                        break;
                    default:
                        break;
                }

                animationLength = m_Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length * Time.unscaledDeltaTime;
            }

            GameManager.m_Instance.DeactivateItem(gameObject, animationLength);
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

            // Play player hurt sound

            player.ChangePlayerVitality(-m_Settings.m_DamageAmount);
        }
    }
}
