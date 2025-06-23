using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "RPG/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string itemName;
    [SerializeField] [TextArea] private string description;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private ItemType type;
    
    [Header("Shop")]
    [SerializeField] private int buyPrice;
    [SerializeField] private int sellPrice;
    [SerializeField] private bool isKeyItem;
    
    [Header("Equipment Stats")]
    [SerializeField] private EquipmentStats equipStats;
    
    [Header("Consumable Effects")]
    [SerializeField] private ConsumableEffect consumableEffect;

    [System.Serializable]
    public struct EquipmentStats
    {
        public float attack;
        public float defense;
        public float magicAttack;
        public float magicDefense;
        public float speed;
        public List<ElementType> elementalBonus;
    }

    [System.Serializable]
    public struct ConsumableEffect
    {
        public float hpRestore;
        public float spRestore;
        public List<string> statusEffectsHealed;
    }
}