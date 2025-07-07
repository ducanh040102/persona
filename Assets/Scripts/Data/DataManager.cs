using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AddressableAssets;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;
    public static DataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<DataManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("DataManager");
                    _instance = go.AddComponent<DataManager>();
                }
            }
            return _instance;
        }
    }

    [Header("Data Collections")]
    private Dictionary<string, CharacterData> _characters = new();
    private Dictionary<string, SkillData> _skills = new();
    private Dictionary<string, ItemData> _items = new();
    private Dictionary<string, EnemyData> _enemies = new();
    private Dictionary<string, ConfidantData> _confidants = new();
    
    [Header("Runtime Data")]
    private SocialStatsData _playerSocialStats;
    private Dictionary<string, int> _inventory = new();
    private HashSet<string> _unlockedSkills = new();

    [Header("Resource Paths")]
    private const string CHARACTER_DATA_PATH = "Data/Characters";
    private const string SKILL_DATA_PATH = "Data/Skills";
    private const string ITEM_DATA_PATH = "Data/Items";
    private const string ENEMY_DATA_PATH = "Data/Enemies";
    private const string CONFIDANT_DATA_PATH = "Data/Confidants";

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        
        InitializeData();
    }

    private async void InitializeData()
    {
        // Load all ScriptableObjects
        try
        {
            // Load Characters
            var characterOperation = Addressables.LoadAssetsAsync<CharacterData>(
                CHARACTER_DATA_PATH,
                character =>
                {
                    if (character != null)
                        _characters[character.Name] = character;
                });
            await characterOperation.Task;

            // Load Skills (similar pattern for other data types)
            var skillOperation = Addressables.LoadAssetsAsync<SkillData>(
                SKILL_DATA_PATH,
                skill =>
                {
                    if (skill != null)
                        _skills[skill.name] = skill;
                });
            await skillOperation.Task;

            // Similar loading patterns for items, enemies, and confidants...
            
            Debug.Log("Data Manager: All data loaded successfully");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Data Manager: Error loading data: {e.Message}");
        }
    }

    #region Character Methods
    public CharacterData GetCharacter(string characterId)
    {
        if (_characters.TryGetValue(characterId, out CharacterData character))
            return character;
        
        Debug.LogWarning($"Character {characterId} not found!");
        return null;
    }

    public List<CharacterData> GetAllCharacters()
    {
        return _characters.Values.ToList();
    }
    #endregion

    #region Skill Methods
    public SkillData GetSkill(string skillId)
    {
        if (_skills.TryGetValue(skillId, out SkillData skill))
            return skill;
        
        Debug.LogWarning($"Skill {skillId} not found!");
        return null;
    }

    public void UnlockSkill(string skillId)
    {
        if (_skills.ContainsKey(skillId))
            _unlockedSkills.Add(skillId);
    }

    public bool IsSkillUnlocked(string skillId)
    {
        return _unlockedSkills.Contains(skillId);
    }
    #endregion

    #region Item Methods
    public ItemData GetItem(string itemId)
    {
        if (_items.TryGetValue(itemId, out ItemData item))
            return item;
        
        Debug.LogWarning($"Item {itemId} not found!");
        return null;
    }

    public void AddItem(string itemId, int amount = 1)
    {
        if (!_items.ContainsKey(itemId))
        {
            Debug.LogWarning($"Trying to add non-existent item: {itemId}");
            return;
        }

        if (_inventory.ContainsKey(itemId))
            _inventory[itemId] += amount;
        else
            _inventory[itemId] = amount;
    }

    public bool UseItem(string itemId)
    {
        if (!_inventory.ContainsKey(itemId) || _inventory[itemId] <= 0)
            return false;

        _inventory[itemId]--;
        if (_inventory[itemId] <= 0)
            _inventory.Remove(itemId);

        return true;
    }

    public int GetItemCount(string itemId)
    {
        return _inventory.TryGetValue(itemId, out int count) ? count : 0;
    }
    #endregion

    #region Social Stats Methods
    public void UpdateSocialStat(string statName, float amount)
    {
        var stat = _playerSocialStats.socialStats.Find(s => s.name == statName);
        // Update logic here
    }

    public int GetSocialStatLevel(string statName)
    {
        var stat = _playerSocialStats.socialStats.Find(s => s.name == statName);
        return stat.level;
    }
    #endregion

    #region Save/Load System
    public void SaveGameData()
    {
        GameSaveData saveData = new()
        {
            inventory = _inventory,
            unlockedSkills = _unlockedSkills.ToList(),
            // Add other necessary data
        };

        string json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString("SaveData", json);
        PlayerPrefs.Save();
    }

    public void LoadGameData()
    {
        if (PlayerPrefs.HasKey("SaveData"))
        {
            string json = PlayerPrefs.GetString("SaveData");
            GameSaveData saveData = JsonUtility.FromJson<GameSaveData>(json);
            
            _inventory = saveData.inventory;
            _unlockedSkills = new HashSet<string>(saveData.unlockedSkills);
            // Load other necessary data
        }
    }

    [System.Serializable]
    private class GameSaveData
    {
        public Dictionary<string, int> inventory;
        public List<string> unlockedSkills;
        // Add other necessary data
    }
    #endregion
}