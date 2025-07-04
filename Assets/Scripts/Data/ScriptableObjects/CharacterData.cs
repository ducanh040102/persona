using System.Collections.Generic;
using UnityEngine;

// CharacterData.cs
[CreateAssetMenu(fileName = "New Character", menuName = "RPG/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string characterId;
    [SerializeField] private string characterName;
    [SerializeField] private string characterDescription;
    [SerializeField] private Sprite characterPortrait;
    
    [Header("Base Stats")]
    [SerializeField] private BaseStats baseStats;
    
    [Header("Persona")]
    [SerializeField] private PersonaData initialPersona;
    [SerializeField] private List<ArcanaType> compatibleArcana;
    [SerializeField] private int maxPersonaSlots = 1;
    
    [Header("Growth")]
    [SerializeField] private GrowthRates growthRates;
    
    // Properties
    public string Id => characterId;
    public string Name => characterName;
    public BaseStats Stats => baseStats;
    public PersonaData InitialPersona => initialPersona;
    public int MaxPersonaSlots => maxPersonaSlots;
    public IReadOnlyList<ArcanaType> CompatibleArcana => compatibleArcana;

    [System.Serializable]
    private struct GrowthRates
    {
        [Range(0f, 1f)] public float hpGrowth;
        [Range(0f, 1f)] public float spGrowth;
        [Range(0f, 1f)] public float strengthGrowth;
        [Range(0f, 1f)] public float magicGrowth;
        [Range(0f, 1f)] public float enduranceGrowth;
        [Range(0f, 1f)] public float agilityGrowth;
        [Range(0f, 1f)] public float luckGrowth;
    }
}