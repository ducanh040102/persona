using System.Collections.Generic;
using UnityEngine;

// EnemyData.cs
[CreateAssetMenu(fileName = "New Enemy", menuName = "RPG/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string enemyID;
    [SerializeField] private string enemyName;
    [SerializeField] [TextArea] private string description;
    [SerializeField] private Sprite enemySprite;
    
    [Header("Enemy Type")]
    [SerializeField] private bool isShadow;  // Is this a Shadow or a Persona user?
    
    [Header("Stats")]
    [SerializeField] private BaseStats baseStats;
    
    [Header("Persona/Shadow Data")]
    [SerializeField] private PersonaData persona;  // Can be either their Persona or Shadow form
    
    [Header("Battle")]
    [SerializeField] private List<SkillData> additionalSkills;  // Skills beyond what their Persona provides
    
    [Header("Rewards")]
    [SerializeField] private BattleRewards rewards;
    
    // For Shadow-type enemies only
    [Header("Shadow Specific")]
    [SerializeField] private bool canBeRecruited;
    [SerializeField] private string[] recruitDialogue;
    
    public string ID => enemyID;
    public string EnemyName => enemyName;
    public BaseStats Stats => baseStats;
    public PersonaData Persona => persona;
    public List<SkillData> AdditionalSkills => additionalSkills;
    public BattleRewards Rewards => rewards;


    [System.Serializable]
    public struct BattleRewards
    {
        public int experiencePoints;
        public int money;
        public List<DropItem> possibleDrops;
        public float personaCardDropChance;  // Chance to drop their Persona as a card
    }

    [System.Serializable]
    public struct DropItem
    {
        public ItemData item;
        [Range(0f, 1f)] public float dropRate;
    }
}