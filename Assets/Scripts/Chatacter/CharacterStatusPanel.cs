using System;
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
    
    private CharacterState character;
    
    public void Initialize(CharacterState character)
    {
        this.character = character;
        UpdateUI();
    }
    
    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        nameText.text = character.name;
        hpText.text = character.GetCurrentHp().ToString();
        spText.text = character.GetCurrentSp().ToString();
    }
}