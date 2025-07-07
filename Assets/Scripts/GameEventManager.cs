using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    private static GameEventManager instance;
    public static GameEventManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("EventManager");
                instance = go.AddComponent<GameEventManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private Dictionary<EventType, Action> eventDictionary = new Dictionary<EventType, Action>();
    private Dictionary<EventType, Dictionary<string, Action<object>>> parameterizedEventDictionary = 
        new Dictionary<EventType, Dictionary<string, Action<object>>>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Subscribe(EventType eventType, Action listener)
    {
        if (!eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] = null;
        }
        eventDictionary[eventType] += listener;
    }

    public void Subscribe<T>(EventType eventType, string key, Action<T> listener)
    {
        if (!parameterizedEventDictionary.ContainsKey(eventType))
        {
            parameterizedEventDictionary[eventType] = new Dictionary<string, Action<object>>();
        }

        Action<object> wrapper = (obj) =>
        {
            if (obj is T typedObj)
            {
                listener(typedObj);
            }
        };

        if (!parameterizedEventDictionary[eventType].ContainsKey(key))
        {
            parameterizedEventDictionary[eventType][key] = null;
        }
        parameterizedEventDictionary[eventType][key] += wrapper;
    }

    public void Unsubscribe(EventType eventType, Action listener)
    {
        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] -= listener;
        }
    }

    public void Unsubscribe<T>(EventType eventType, string key, Action<T> listener)
    {
        if (parameterizedEventDictionary.ContainsKey(eventType) && 
            parameterizedEventDictionary[eventType].ContainsKey(key))
        {
            // Note: This is a simplified unsubscribed. In a production environment,
            // you might want to maintain a separate dictionary of wrapper functions
            parameterizedEventDictionary[eventType].Remove(key);
        }
    }

    public void TriggerEvent(EventType eventType)
    {
        if (eventDictionary.TryGetValue(eventType, out Action action))
        {
            action?.Invoke();
        }
    }

    public void TriggerEvent<T>(EventType eventType, string key, T parameter)
    {
        if (parameterizedEventDictionary.TryGetValue(eventType, out var dictionary))
        {
            if (dictionary.TryGetValue(key, out Action<object> action))
            {
                action?.Invoke(parameter);
            }
        }
    }
}

public enum EventType
{
    // Combat Events
    CombatStart,
    CombatEnd,
    TurnStart,
    TurnEnd,
    CharacterSelected,
    TargetSelected,
    SkillSelected,
    DamageDealt,
    CharacterDied,
    
    // UI Events
    UpdateUI,
    ShowMenu,
    HideMenu,
    
    // Game State Events
    GamePause,
    GameResume,
    GameOver,
    LevelComplete,
    
    // Character Events
    CharacterSpawned,
    CharacterDestroyed,
    StatChanged,
    StatusEffectApplied,
    StatusEffectRemoved,
}
