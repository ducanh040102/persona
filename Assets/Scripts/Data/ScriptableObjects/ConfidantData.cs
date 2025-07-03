using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Confidant", menuName = "RPG/Confidant Data")]
public class ConfidantData : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string confidantId;
    [SerializeField] private string confidantName;
    [SerializeField] private string arcana;
    [SerializeField] private Sprite confidantPortrait;
    
    [Header("Rank")]
    [SerializeField] private int currentRank;
    [SerializeField] private int maxRank;
    
    [Header("Benefits")]
    [SerializeField] private List<RankBenefit> rankBenefits;
    
    public string ConfidantId => confidantId;
    public string ConfidantName => confidantName;
    public string Arcana => arcana;
    public Sprite ConfidantPortrait => confidantPortrait;
    public int CurrentRank => currentRank;
    public int MaxRank => maxRank;
    public List<RankBenefit> RankBenefits => rankBenefits;

    [System.Serializable]
    public struct RankBenefit
    {
        public int rank;
        [TextArea] public string benefitDescription;
        public List<SkillData> unlockedSkills;
    }
}