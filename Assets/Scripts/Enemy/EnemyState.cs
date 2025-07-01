// File: Assets/Scripts/Managers/EnemyState.cs
using UnityEngine;
using System.Linq;

public class EnemyState : MonoBehaviour, ICombatant
{
    private EnemyData baseData;
    private BaseStats currentStats;
    private float currentHp;
    private float currentSp;

    public void Initialize(EnemyData data)
    {
        baseData = data;
        currentStats = data.Stats;
        
        currentHp = currentStats.maxHp;
        currentSp = currentStats.maxSp;
    }

    public BaseStats GetCurrentStats()
    {
        return currentStats;
    }

    public AffinityType GetElementalAffinity(ElementType element)
    {
        // Assumes PersonaData or EnemyData has ElementAffinities property
        if (baseData.Persona != null && baseData.Persona.ElementAffinities != null)
        {
            var affinity = baseData.Persona.ElementAffinities.FirstOrDefault(a => a.element == element);
            return affinity.affinity;
        }
        return AffinityType.Normal;
    }

    public void ModifySP(int amount)
    {
        currentSp = Mathf.Clamp(currentSp + amount, 0, currentStats.maxSp);
    }

    public void TakeDamage(int amount)
    {
        currentHp = Mathf.Max(0, currentHp - amount);
    }

    public bool IsDead => currentHp <= 0;

    // Optional: Expose name and skills for UI/AI
    public string GetName() => baseData != null ? baseData.name : "Unknown";
    public System.Collections.Generic.List<SkillData> GetAvailableSkills() => baseData != null ? baseData.AdditionalSkills : null;
}