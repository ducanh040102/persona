using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "RPG/Skill Data")]
public class SkillData : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string id;
    [SerializeField] private string skillName;
    [SerializeField] [TextArea] private string description;
    [SerializeField] private Sprite skillIcon;
    
    [Header("Cost & Power")]
    [SerializeField] private int spCost;
    [SerializeField] private float power;
    
    [Header("Properties")]
    [SerializeField] private SkillType type;
    [SerializeField] private ElementType element;
    [SerializeField] private TargetType targetType;
    
    [Header("Effects")]
    [SerializeField] private List<StatusEffect> additionalEffects;
    
    public string Id => id;
    public string SkillName => skillName;
    public string Description => description;
    public Sprite SkillIcon => skillIcon;
    
    public int SpCost => spCost;
    public float Power => power;
    
    public ElementType Element => element;

    [System.Serializable]
    public struct StatusEffect
    {
        public string effectName;
        public int duration;
        [Range(0f, 1f)] public float magnitude;
    }
}