using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CombatSystem : MonoBehaviour
{
    private List<CharacterState> characterInBattle = new();
    private List<EnemyState> enemiesInBattle = new();
    private Queue<ICombatant> turnOrder = new();
    private CombatState currentState = CombatState.Initializing;
    
    public CombatSystem Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    public void StartCombat(List<CharacterState> party, List<EnemyState> enemies)
    {
        characterInBattle = party;
        this.enemiesInBattle = enemies;
        
        DetermineTurnOrder();
        currentState = CombatState.PlayerTurn;
    }

    public void ClearCombatData()
    {
        characterInBattle.Clear();
        enemiesInBattle.Clear();
        turnOrder.Clear();
        currentState = CombatState.Initializing;
    }

    private void DetermineTurnOrder()
    {
        var allCombatants = characterInBattle.Cast<ICombatant>()
            .Concat(enemiesInBattle.Cast<ICombatant>())
            .OrderByDescending(c => c.GetCurrentStats().agility);
            
        turnOrder = new Queue<ICombatant>(allCombatants);
    }

    public void ExecuteSkill(ICombatant user, SkillData skill, ICombatant target)
    {
        // if (!CanUseSkill(user, skill))
        //     return;
        //
        // float damage = CalculateDamage(user, target, skill);
        // ApplyDamage(target, damage);
        //
        // if (skill.element != ElementType.Physical)
        //     user.ModifySP(-skill.spCost);

        CheckBattleStatus();
        AdvanceTurn();
    }

    private float CalculateDamage(ICombatant attacker, ICombatant target, SkillData skill)
    {
        float baseDamage = skill.Power;
        float attackStat = skill.Element == ElementType.Physical ? 
            attacker.GetCurrentStats().strength : 
            attacker.GetCurrentStats().magic;
        
        float defenseStat = target.GetCurrentStats().endurance;
        float affinityMultiplier = GetAffinityMultiplier(target, skill.Element);
        
        return (baseDamage * attackStat / defenseStat) * affinityMultiplier;
    }

    private float GetAffinityMultiplier(ICombatant target, ElementType element)
    {
        return target.GetElementalAffinity(element) switch
        {
            AffinityType.Weak => 2.0f,
            AffinityType.Resist => 0.5f,
            AffinityType.Null => 0f,
            AffinityType.Drain => -1f,
            _ => 1f
        };
    }

    private void AdvanceTurn()
    {
        var current = turnOrder.Dequeue();
        turnOrder.Enqueue(current);
        
        currentState = turnOrder.Peek() is CharacterState ? 
            CombatState.PlayerTurn : 
            CombatState.EnemyTurn;
    }

    private void CheckBattleStatus()
    {
        // if (enemies.All(e => e.IsDead))
        //     EndBattle(true);
        // else if (playerParty.All(p => p.IsDead))
        //     EndBattle(false);
    }

    private void EndBattle(bool victory)
    {
        currentState = CombatState.Complete;
        // if (victory)
        //     DistributeRewards();
    }
}


public interface ICombatant
{
    BaseStats GetCurrentStats();
    AffinityType GetElementalAffinity(ElementType element);
    void ModifySP(int amount);
    bool IsDead { get; }
}

public enum CombatState
{
    Initializing,
    PlayerTurn,
    EnemyTurn,
    Complete
}
