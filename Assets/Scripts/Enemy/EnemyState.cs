
using System.Collections.Generic;
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
        float oldSp = currentSp;
        currentSp = Mathf.Clamp(currentSp + amount, 0, currentStats.maxSp);
        Debug.Log($"[EnemyState] {baseData?.name ?? "Unknown"} SP changed: {oldSp} -> {currentSp} (delta: {amount})");
    }

    public void TakeDamage(int amount)
    {
        float oldHp = currentHp;
        currentHp = Mathf.Max(0, currentHp - amount);
        Debug.Log($"[EnemyState] {baseData?.name ?? "Unknown"} took {amount} damage: {oldHp} -> {currentHp}");
    }

    public bool IsDead => currentHp <= 0;

    // Optional: Expose name and skills for UI/AI
    public string GetName() => baseData != null ? baseData.name : "Unknown";
    public List<SkillData> GetAvailableSkills() => baseData != null ? baseData.AdditionalSkills : null;
}