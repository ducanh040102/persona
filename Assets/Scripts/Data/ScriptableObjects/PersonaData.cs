using UnityEngine;

[CreateAssetMenu(fileName = "New Persona", menuName = "RPG/Persona Data")]
public class PersonaData : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string personaID;
    [SerializeField] private string personaName;
    [SerializeField] [TextArea] private string description;
    [SerializeField] private Sprite personaSprite;
    [SerializeField] private ArcanaType arcana; // The Fool, The Magician, etc.
    
    [Header("Level Requirements")]
    [SerializeField] private int baseLevel;
    
    [Header("Base Stats")]
    [SerializeField] private BaseStats baseStats;
    
    [Header("Affinities")]
    [SerializeField] private ElementAffinity[] elementAffinities;
    
    [Header("Skills")]
    [SerializeField] private PersonaSkillLearnData[] learnableSkills;
    
    public string ID => personaID;
    public string Name => personaName;
    public ArcanaType Arcana => arcana;
    public BaseStats Stats => baseStats;
    
    public ElementAffinity[] ElementAffinities => elementAffinities;
    public PersonaSkillLearnData[] LearnableSkills => learnableSkills;
}
