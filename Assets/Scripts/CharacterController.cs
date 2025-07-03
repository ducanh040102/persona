using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private CombatSystem combatSystem;
    [SerializeField] private DataManager dataManager;
    
    public UnityEvent<CharacterState> onCharacterSelected;
    public UnityEvent<ICombatant> onTargetSelected;
    
    private CharacterState selectedCharacter;
    private SkillData selectedSkill;
    private CombatActionType currentActionType;
    
    public void SelectCharacter(CharacterState character)
    {
        selectedCharacter = character;
        onCharacterSelected?.Invoke(character);
    }
    
    public void SelectAttackAction()
    {
        currentActionType = CombatActionType.Attack;
        // Use a basic attack skill
        selectedSkill = dataManager.GetSkill("Attack");
    }
    
    public void SelectSkill(string skillId)
    {
        currentActionType = CombatActionType.Skill;
        selectedSkill = dataManager.GetSkill(skillId);
    }
    
    public void SelectTarget(ICombatant target)
    {
        if (selectedCharacter == null || selectedSkill == null) return;
        
        onTargetSelected?.Invoke(target);
        ExecuteAction(target);
    }
    
    private void ExecuteAction(ICombatant target)
    {
        combatSystem.ExecuteSkill(selectedCharacter, selectedSkill, target);
        ResetSelection();
    }
    
    private void ResetSelection()
    {
        selectedCharacter = null;
        selectedSkill = null;
        currentActionType = CombatActionType.None;
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
