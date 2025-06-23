using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Social Stats", menuName = "RPG/Social Stats Data")]
public class SocialStatsData : ScriptableObject
{
    [SerializeField] public List<SocialStat> socialStats;

    [System.Serializable]
    public struct SocialStat
    {
        public string name;
        public int level;
        public int maxLevel;
        public float currentPoints;
        public float pointsToNextLevel;
    }
}