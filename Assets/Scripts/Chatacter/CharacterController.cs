using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private CombatSystem combatSystem;

    private CharacterState selectedCharacter;
    [SerializeField] private SkillData selectedSkill;
    private CombatActionType currentActionType;

    public void SelectCharacter(CharacterState character)
    {
        Debug.Log($"[CharacterController] Selecting character: {character?.name ?? "null"}");
        selectedCharacter = character;
        GameEventManager.Instance.TriggerEvent(EventType.CharacterSelected, "character", character);
        Debug.Log($"[CharacterController] Character selected: {character?.name ?? "null"}");
    }

    public void SelectAttackAction()
    {
        Debug.Log("[CharacterController] Selecting Attack action");
        currentActionType = CombatActionType.Attack;
        Debug.Log($"[CharacterController] Action type set to: {currentActionType}");
    }

    public void SelectSkill(SkillData skill)
    {
        Debug.Log($"[CharacterController] Selecting skill: {skill?.SkillName ?? "null"}");
        currentActionType = CombatActionType.Skill;
        selectedSkill = skill;
        Debug.Log(
            $"[CharacterController] Skill selected: {skill?.SkillName ?? "null"}, Action type: {currentActionType}");
    }

    public void SelectTarget(ICombatant target)
    {
        Debug.Log($"[CharacterController] Selecting target: {target?.GetType().Name ?? "null"}");

        if (selectedCharacter == null)
        {
            Debug.LogWarning("[CharacterController] Cannot select target: No character selected");
            return;
        }

        if (selectedSkill == null)
        {
            Debug.LogWarning("[CharacterController] Cannot select target: No skill selected");
            return;
        }
        
        
        ExecuteAction(target);
        Debug.Log($"[CharacterController] Target selected and action executed");
    }

    private void ExecuteAction(ICombatant target)
    {
        Debug.Log(
            $"[CharacterController] Executing action: {selectedSkill.SkillName} on target {target?.GetType().Name ?? "null"}");
        Debug.Log(
            $"[CharacterController] Character: {selectedCharacter?.name ?? "null"}, Skill: {selectedSkill?.SkillName ?? "null"}");

        combatSystem.ExecuteSkill(selectedCharacter, selectedSkill, target);
        Debug.Log($"[CharacterController] Executed {selectedSkill.SkillName} successfully");

        ResetSelection();
    }

    private void ResetSelection()
    {
        Debug.Log("[CharacterController] Resetting selection");
        Debug.Log($"[CharacterController] Previous state - Character: {selectedCharacter?.name ?? "null"}, " +
                  $"Skill: {selectedSkill?.SkillName ?? "null"}, Action: {currentActionType}");

        selectedCharacter = null;
        selectedSkill = null;
        currentActionType = CombatActionType.None;

        Debug.Log("[CharacterController] Selection reset complete");
    }
}

public enum CombatActionType
{
    None,
    Attack,
    Skill,
    Guard,
    Item
}