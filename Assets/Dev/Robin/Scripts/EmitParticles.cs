using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robin
{
    public class EmitParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystemLeft;
        [SerializeField] private ParticleSystem _particleSystemRight;


        public void EmitDustCloudLeft()
        {
            _particleSystemLeft.Emit(9);
        }

        public void EmitDustCloudRight()
        {
            _particleSystemRight.Emit(9);
        }

        private void Update()
        {
            if (GameStateManager.instance.CurrentGameState == GameStateManager.GameState.GameOver)
            {
                _particleSystemLeft.GetComponent<ParticleSystemRenderer>().enabled = false;
                _particleSystemRight.GetComponent<ParticleSystemRenderer>().enabled = false;
            }
        }
    }
}

