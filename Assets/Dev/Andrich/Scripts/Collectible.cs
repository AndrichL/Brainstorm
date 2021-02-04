using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Andrich
{ 

    public class Collectible : MonoBehaviour
    {
        [SerializeField] private CollectibleSettings m_Settings;

        private void OnTriggerEnter2D(Collider2D col)
        {
            Player player = col.GetComponent<Player>();

            if(player)
            {
                switch (m_Settings.m_WhichCollectible)
                {
                    case WhichCollectible.brain:
                        break;
                    case WhichCollectible.heart:
                        player.ChangePlayerVitality(m_Settings.m_HealAmount);
                        break;
                    case WhichCollectible.anvil:
                        player.ChangePlayerVitality(-m_Settings.m_DamageAmount);
                        break;
                    default:
                        break;
                }

                //Debug.Log("Player interacted with collectible");
                Robin.ScoreManager.instance.AddToScore(m_Settings.m_PointsAmount);
            }
            else
            {
                if(m_Settings.m_WhichCollectible == WhichCollectible.brain)
                {
                    if(GameManager.m_Instance.GetCurrentPlayer() == null)
                    {
                        Debug.LogError("No current player");
                        return;
                    }
                    GameManager.m_Instance.GetCurrentPlayer().ChangePlayerVitality(-m_Settings.m_DamageAmount);

                    //Debug.Log("Brain hit the gorund");
                }
            }

            gameObject.SetActive(false);
        }
    }
}
