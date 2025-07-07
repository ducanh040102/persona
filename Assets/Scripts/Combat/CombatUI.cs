using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class CombatUI : MonoBehaviour
{
    [Header("Character UI")]
    [SerializeField] private GameObject characterPanelPrefab;
    [SerializeField] private Transform characterUIContainer;
    
    [Header("Action Menu")]
    [SerializeField] private GameObject actionMenuPanel;
    [SerializeField] private GameObject skillMenuPanel;
    [SerializeField] private Transform skillButtonContainer;
    [SerializeField] private GameObject skillButtonPrefab;
    
    [SerializeField] private Button attackButton;
    [SerializeField] private Button skillsButton;
    
    [FormerlySerializedAs("playerController")]
    [Header("References")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private CombatSystem combatSystem;
    
    private List<CharacterStatusPanel> characterPanels = new List<CharacterStatusPanel>();
    
    [SerializeField] private List<SkillData> testSkill = new List<SkillData>();
    
    public void InitializeUI()
    {
        // Create character status panels for each character in battle
        foreach (var character in FindFirstObjectByType<CombatManager>().characterInBattle)
        {
            CreateCharacterPanel(character);
        }
        
        attackButton.onClick.AddListener(OnAttackButton);
        skillsButton.onClick.AddListener(OnSkillsButton);
    }
    
    private void CreateCharacterPanel(CharacterState character)
    {
        var panelObj = Instantiate(characterPanelPrefab, characterUIContainer);
        var panel = panelObj.GetComponent<CharacterStatusPanel>();
        panel.Initialize(character);
        //panel.OnPanelClicked += () => characterController.SelectCharacter(character);
        characterPanels.Add(panel);
    }
    
    public void ShowActionMenu(CharacterState character)
    {
        actionMenuPanel.SetActive(true);
        skillMenuPanel.SetActive(false);
    }
    
    public void OnAttackButton()
    {
        characterController.SelectSkill(testSkill[0]);
        characterController.SelectTarget(combatSystem.GetRandomEnemy());
        //characterController.SelectAttackAction();
        //EnableTargetSelection();
    }
    
    public void OnSkillsButton()
    {
        actionMenuPanel.SetActive(false);
        skillMenuPanel.SetActive(true);
        PopulateSkillMenu();
    }
    
    private void PopulateSkillMenu()
    {
        // Clear existing skill buttons
        foreach (Transform child in skillButtonContainer)
        {
            Destroy(child.gameObject);
        }
        
        // Create new skill buttons
        // Note: You'll need to implement a way to get the character's available skills
        foreach (var skill in testSkill)
        {
            var buttonObj = Instantiate(skillButtonPrefab, skillButtonContainer);
            var button = buttonObj.GetComponent<Button>();
            var text = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            
            text.text = $"{skill.SkillName} ({skill.SpCost} SP)";
            button.onClick.AddListener(() => {
                characterController.SelectSkill(skill);
                characterController.SelectTarget(combatSystem.GetRandomEnemy());
                //EnableTargetSelection();
                HideAllMenus();
            });
        }
        
        Debug.Log("Populated skill menu");
    }
    
    private void EnableTargetSelection()
    {
        // Enable target selection mode
        // This would highlight available targets and allow clicking them
        HideAllMenus();
    }
    
    private void HideAllMenus()
    {
        actionMenuPanel.SetActive(false);
        skillMenuPanel.SetActive(false);
    }
    
    // Placeholder method - implement based on your skill system
    
}

// Additional UI component for character status