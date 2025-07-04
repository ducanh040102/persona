using System;
using UnityEngine;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Transform playerSpawnArea;
    [SerializeField] private Transform enemySpawnArea;
    [SerializeField] private float spacingBetweenCharacters = 2f;

    [Header("References")]
    [SerializeField] private CombatSystem combatSystem;
    [SerializeField] private CombatUI combatUI;
    
    [SerializeField] private GameObject characterPrefab;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private List<CharacterData> charcters;
    [SerializeField] private List<EnemyData> enemies;
    
    [SerializeField] private List<EnemyState> enemiesInBattle;
    [SerializeField] public List<CharacterState> characterInBattle;

    public CombatManager Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializeCombat(charcters, enemies);
    }
    

    public void SpawnParty(List<CharacterData> partyData)
    {
        if (playerSpawnArea == null)
        {
            Debug.LogError("Player spawn area not set!");
            return;
        }

        for (int i = 0; i < partyData.Count; i++)
        {
            Vector3 spawnPosition = playerSpawnArea.position + Vector3.right * (i * spacingBetweenCharacters);
            GameObject characterObject = Instantiate(characterPrefab, spawnPosition, playerSpawnArea.rotation);
            
            // Get or add CharacterState component
            CharacterState character = characterObject.GetComponent<CharacterState>();
            if (character == null)
            {
                character = characterObject.AddComponent<CharacterState>();
            }

            character.Initialize(partyData[i]);
            
            characterInBattle.Add(character);
        }
    }

    public void SpawnEnemies(List<EnemyData> enemyGroup)
    {
        if (enemySpawnArea == null)
        {
            Debug.LogError("Enemy spawn area not set!");
            return;
        }

        for (int i = 0; i < enemyGroup.Count; i++)
        {
            Vector3 spawnPosition = enemySpawnArea.position + Vector3.right * (i * spacingBetweenCharacters);
            GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, enemySpawnArea.rotation);
            
            // Get or add EnemyState component
            EnemyState enemy = enemyObject.GetComponent<EnemyState>();
            if (enemy == null)
            {
                enemy = enemyObject.AddComponent<EnemyState>();
            }

            // Initialize enemy with data
            enemy.Initialize(enemyGroup[i]);

            enemiesInBattle.Add(enemy);
        }
    }

    public void ClearBattlefield()
    {
        // Clear existing characters and enemies
        foreach (var character in characterInBattle)
        {
            if (character != null && character is MonoBehaviour mb)
            {
                Destroy(mb.gameObject);
            }
        }

        foreach (var enemy in enemiesInBattle)
        {
            if (enemy != null && enemy is MonoBehaviour mb)
            {
                Destroy(mb.gameObject);
            }
        }
        
        combatSystem.ClearCombatData();
    }

    // Example method to start a new combat encounter
    public void InitializeCombat(List<CharacterData> party, List<EnemyData> enemies)
    {
        ClearBattlefield();
        SpawnParty(party);
        SpawnEnemies(enemies);
        combatSystem.StartCombat(characterInBattle, enemiesInBattle);
        combatUI.InitializeUI();
        
        
    }
}