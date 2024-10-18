using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStatsCaretaker
{
    // Stack to hold the player's saved states
    private Stack<CharacterStatsMemento> savedStates = new Stack<CharacterStatsMemento>();

    // Save the current state of the player by pushing onto the stack
    public void SaveState(CharacterStatsMemento memento)
    {
        savedStates.Push(memento);
        //Debug.Log("[PlayerStats] count {save}: " + savedStates.Count.ToString());
        //int countStats = 1;
        //foreach (CharacterStatsMemento state in savedStates) 
        //{
        //    Debug.Log("[PlayerStats] index {save}: " + countStats);
        //    Debug.Log("[PlayerStats] level {save}: " + state.Level);
        //    foreach (BaseStat baseStat in state.PlayerBaseStats)
        //    {
        //        Debug.Log("[PlayerStats] " + baseStat.StatName +   " {save}: " + baseStat.FinalValue.ToString());
        //    }
        //    countStats++;
        //}
    }

    // Get the last saved state by popping from the stack
    public CharacterStatsMemento GetSavedState()
    {
        if(savedStates.Count <= 0)
        {
            // Handle case where no states are available
            Debug.LogError("No saved states to restore.");
            return null;
        }
        savedStates.Pop(); // Remove the current player state
        if (savedStates.Count <= 0) return null; // Check again if the stack is empty

        //Debug.Log("[PlayerStats] count {reborn}: " + savedStates.Count.ToString());
        //foreach (BaseStat baseStat in savedStates.Peek().PlayerBaseStats)
        //{
        //    Debug.Log("[PlayerStats] " + baseStat.StatName + " {reborn}: " + baseStat.FinalValue.ToString());
        //}
        return savedStates.Peek(); // Get the previous player stats
    }

    // Peek at the current state without removing it
    public CharacterStatsMemento PeekState()
    {
        if (savedStates.Count <= 0)
        {
            Debug.LogError("No states available to peek.");
            return null;
        }

        return savedStates.Peek();
    }

    // Clear all saved states (optional if needed)
    public void ClearSavedStates()
    {
        savedStates.Clear();
    }
}
