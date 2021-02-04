using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robin
{
    public class EmitParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;


        public void EmitDustCloud()
        {
            _particleSystem.Emit(9);
        }
        
        private void Update()
        {
            if (GameStateManager.instance.CurrentGameState == GameStateManager.GameState.GameOver)
            {
                _particleSystem.GetComponent<ParticleSystemRenderer>().enabled = false;

            }
        }
    }
}

