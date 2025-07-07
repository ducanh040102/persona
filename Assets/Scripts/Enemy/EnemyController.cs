using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private CombatSystem combatSystem;

    public void ExecuteEnemyTurn(EnemyState enemy)
    {
        if (enemy == null)
        {
            Debug.LogError("[EnemyController] Cannot execute turn: Enemy is null");
            return;
        }

        // Get random skill from enemy's available skills
        var availableSkills = enemy.GetAvailableSkills();
        if (availableSkills == null || availableSkills.Count == 0)
        {
            Debug.LogError("[EnemyController] Enemy has no available skills");
            return;
        }

        int randomSkillIndex = Random.Range(0, availableSkills.Count);
        SkillData selectedSkill = availableSkills[randomSkillIndex];
        
        var characters = combatSystem.GetAliveCharacters();
        if (characters == null || characters.Count == 0)
        {
            Debug.LogError("[EnemyController] No valid targets available");
            return;
        }

        int randomTargetIndex = Random.Range(0, characters.Count);
        CharacterState target = characters[randomTargetIndex];

        // Execute the skill
        Debug.Log($"[EnemyController] Enemy {enemy.name} using {selectedSkill.SkillName} on {target.name}");
        combatSystem.ExecuteSkill(enemy, selectedSkill, target);
    }
}
