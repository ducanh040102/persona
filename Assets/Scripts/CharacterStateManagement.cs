// CharacterState.cs

using System.Collections.Generic;
using System.Linq;

public class CharacterState
{
    public CharacterData BaseData { get; private set; }
    private List<PersonaData> equippedPersonas;
    private PersonaData activePersona;
    
    public CharacterState(CharacterData baseData)
    {
        BaseData = baseData;
        equippedPersonas = new List<PersonaData> { baseData.InitialPersona };
        activePersona = baseData.InitialPersona;
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
}