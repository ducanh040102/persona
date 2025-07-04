using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatusPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI spText;
    [SerializeField] private Image hpBar;
    [SerializeField] private Image spBar;
    
    public System.Action OnPanelClicked;
    private CharacterState character;
    
    public void Initialize(CharacterState character)
    {
        this.character = character;
        UpdateUI();
    }
    
    public void OnClick()
    {
        OnPanelClicked?.Invoke();
    }
    
    private void UpdateUI()
    {
        // Update the UI elements with character data
        // You'll need to add methods to CharacterState to expose HP/SP values
        nameText.text = character.name;
        hpText.text = character.GetCurrentHp().ToString();
        spText.text = character.GetCurrentSp().ToString();
        // Update HP/SP bars and text
    }
}