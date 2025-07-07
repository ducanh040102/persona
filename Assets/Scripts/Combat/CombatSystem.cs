using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CombatSystem : MonoBehaviour
{
    private List<CharacterState> characterInBattle = new();
    private List<EnemyState> enemiesInBattle = new();
    private Queue<ICombatant> turnOrder = new();
    private CombatState currentState = CombatState.Initializing;

    [SerializeField] private CharacterController characterController;
    [SerializeField] private EnemyController enemyController;

    public CombatSystem Instance { get; private set; }

    private void Awake()
    {
        Debug.Log("[CombatSystem] Awake called");
        Instance = this;
    }

    public void StartCombat(List<CharacterState> party, List<EnemyState> enemies)
    {
        Debug.Log(
            $"[CombatSystem] StartCombat called with {party?.Count ?? 0} party members and {enemies?.Count ?? 0} enemies");
        characterInBattle = party;
        this.enemiesInBattle = enemies;

        DetermineTurnOrder();
        currentState = CombatState.PlayerTurn;

        if (turnOrder.Peek() is CharacterState character)
        {
            characterController.SelectCharacter(character);
        }
    }

    public void ClearCombatData()
    {
        Debug.Log("[CombatSystem] ClearCombatData called");
        characterInBattle.Clear();
        enemiesInBattle.Clear();
        turnOrder.Clear();
        currentState = CombatState.Initializing;
    }

    private void DetermineTurnOrder()
    {
        Debug.Log("[CombatSystem] DetermineTurnOrder called");
        var allCombatants = characterInBattle.Cast<ICombatant>()
            .Concat(enemiesInBattle.Cast<ICombatant>())
            .OrderByDescending(c => c.GetCurrentStats().agility);

        turnOrder = new Queue<ICombatant>(allCombatants);
    }

    public void ExecuteSkill(ICombatant user, SkillData skill, ICombatant target)
    {
        Debug.Log($"[CombatSystem] ExecuteSkill called by {user} using {skill?.SkillName ?? "Unknown"} on {target}");
        int damage = Mathf.RoundToInt(CalculateDamage(user, target, skill));
        target.TakeDamage(damage);

        if (skill.Element != ElementType.Physical)
            user.ModifySP(-skill.SpCost);

        CheckBattleStatus();
        AdvanceTurn();
    }

    private float CalculateDamage(ICombatant attacker, ICombatant target, SkillData skill)
    {
        Debug.Log(
            $"[CombatSystem] CalculateDamage called: attacker={attacker}, target={target}, skill={skill?.SkillName ?? "Unknown"}");
        float baseDamage = skill.Power;
        float attackStat = skill.Element == ElementType.Physical
            ? attacker.GetCurrentStats().strength
            : attacker.GetCurrentStats().magic;

        float defenseStat = target.GetCurrentStats().endurance;
        float affinityMultiplier = GetAffinityMultiplier(target, skill.Element);

        return (baseDamage * attackStat / defenseStat) * affinityMultiplier;
    }

    private float GetAffinityMultiplier(ICombatant target, ElementType element)
    {
        Debug.Log($"[CombatSystem] GetAffinityMultiplier called: target={target}, element={element}");
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
        Debug.Log("[CombatSystem] AdvanceTurn called");
        var current = turnOrder.Dequeue();
        turnOrder.Enqueue(current);

        currentState = turnOrder.Peek() is CharacterState ? CombatState.PlayerTurn : CombatState.EnemyTurn;
        
        Debug.Log($"[CombatSystem] Turn order: {string.Join(", ", turnOrder)}");
        Debug.Log($"[CombatSystem] Current state: {currentState}");

        if (turnOrder.Peek() is CharacterState character)
        {
            characterController.SelectCharacter(character);
        }
        else if (turnOrder.Peek() is EnemyState enemy)
        {
            // Handle enemy turn
            enemyController.ExecuteEnemyTurn(enemy);
        }
    }

    private void CheckBattleStatus()
    {
        Debug.Log("[CombatSystem] CheckBattleStatus called");
        // if (enemies.All(e => e.IsDead))
        //     EndBattle(true);
        // else if (playerParty.All(p => p.IsDead))
        //     EndBattle(false);
    }

    private void EndBattle(bool victory)
    {
        Debug.Log($"[CombatSystem] EndBattle called. Victory: {victory}");
        currentState = CombatState.Complete;
        // if (victory)
        //     DistributeRewards();
    }

    public EnemyState GetRandomEnemy()
    {
        Debug.Log("[CombatSystem] GetRandomEnemy called");
        // var aliveEnemies = enemiesInBattle.Where(e => !e.IsDead).ToList();
        // if (aliveEnemies.Count == 0)
        //     return null;
        //int index = Random.Range(0, enemiesInBattle.Count);
        return enemiesInBattle[0];
    }
    
    // Add this method to get alive characters
    public List<CharacterState> GetAliveCharacters()
    {
        //return characterInBattle.Where(c => !c.IsDead).ToList();
        return characterInBattle;
    }
}

public interface ICombatant
{
    BaseStats GetCurrentStats();
    AffinityType GetElementalAffinity(ElementType element);
    void ModifySP(int amount);
    void TakeDamage(int amount);
    bool IsDead { get; }
}

public enum CombatState
{
    Initializing,
    PlayerTurn,
    EnemyTurn,
    Complete
}