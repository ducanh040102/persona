using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CharacterState: MonoBehaviour, ICombatant
{
    private CharacterData BaseData;
    private List<PersonaData> equippedPersonas;
    private PersonaData activePersona;
    
    private BaseStats currentStats;
    private float currentHp;
    private float currentSp;
    
    public float GetCurrentHp() => currentHp;
    public float GetCurrentSp() => currentSp;
    
    public void Initialize(CharacterData data)
    {
        BaseData = data;
        equippedPersonas = new List<PersonaData>();
        currentStats = data.Stats;
        currentHp = currentStats.maxHp;
        currentSp = currentStats.maxSp;
        
        Debug.Log($"[CharacterState] Character {BaseData.name} initialized. HP: {currentHp}, SP: {currentSp}");
    }
    
    public bool CanEquipPersona(PersonaData persona)
    {
        return BaseData.CompatibleArcana.Contains(persona.Arcana) && 
               equippedPersonas.Count < BaseData.MaxPersonaSlots;
    }
    
    public bool EquipPersona(PersonaData persona)
    {
        if (!CanEquipPersona(persona))
            return false;
            
        equippedPersonas.Add(persona);
        return true;
    }
    
    public void SetActivePersona(PersonaData persona)
    {
        if (equippedPersonas.Contains(persona))
            activePersona = persona;
    }
    
    // Calculate final stats combining character and active Persona
    public BaseStats GetCurrentStats()
    {
        BaseStats finalStats = BaseData.Stats;
        // Add Persona stats
        if (activePersona != null)
        {
            // Implement stat combination logic here
        }
        return finalStats;
    }
    
    public AffinityType GetElementalAffinity(ElementType element)
    {
        // Implement logic to get current elemental affinity
        // combining character and active Persona affinities
        return activePersona != null ? 
            activePersona.ElementAffinities.First(a => a.element == element).affinity : 
            AffinityType.Normal;
    }

    public void ModifySP(int amount)
    {
        float oldSp = currentSp;
        currentSp = Mathf.Clamp(currentSp + amount, 0, currentStats.maxSp);
        Debug.Log($"[CharacterState] {BaseData?.name ?? "Unknown"} SP changed: {oldSp} -> {currentSp} (delta: {amount})");
    }

    public void TakeDamage(int amount)
    {
        float oldHp = currentHp;
        currentHp = Mathf.Max(0, currentHp - amount);
        Debug.Log($"[CharacterState] {BaseData?.name ?? "Unknown"} took {amount} damage: {oldHp} -> {currentHp}");
    }
    
    public bool IsDead => currentHp <= 0;
}
