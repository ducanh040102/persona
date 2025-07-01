// Example usage in another script

using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    void Start()
    {
        // Get a character
        CharacterData hero = DataManager.Instance.GetCharacter("Hero");
        
        // Add items to inventory
        DataManager.Instance.AddItem("Potion", 5);
        
        // Use an item
        if (DataManager.Instance.UseItem("Potion"))
        {
            Debug.Log("Used a potion!");
        }
        
        // Unlock a skill
        DataManager.Instance.UnlockSkill("Fireball");
        
        // Check if skill is unlocked
        if (DataManager.Instance.IsSkillUnlocked("Fireball"))
        {
            Debug.Log("Can use Fireball!");
        }
        
        // Save game
        DataManager.Instance.SaveGameData();
    }
}