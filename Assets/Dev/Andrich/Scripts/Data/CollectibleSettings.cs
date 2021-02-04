using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Andrich
{
    public enum WhichCollectible
    {
        brain,
        heart,
        anvil
    }

    [CreateAssetMenu(fileName = "Collectible Settings", menuName = "Create Collectible Settings")]
    public class CollectibleSettings : ScriptableObject
    {
        public WhichCollectible m_WhichItem;
        public int m_PointsAmount = 100;
        public int m_HealAmount = 1;
        public int m_DamageAmount = 1;
    }
}
