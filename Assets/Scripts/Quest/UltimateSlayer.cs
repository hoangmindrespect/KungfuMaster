using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSlayer : Quest
{
    void Start()
    {
        Debug.Log("Ultimate slayer assigned.");
        QuestName = "Ultimate Slayer";
        Description = "Kill a bunch of stuff.";
        ItemReward = ItemDatabase.Instance.GetItem("potion_nonstat");
        ExperienceReward = 100;
        Goals = new List<Goal>
        {
            new KillGoal(this, 1, "Kill 2 Crowdeaths", false, 0, 2),
            new KillGoal(this, 2, "Kill 2 Skeletons", false, 0, 2),
            //new CollectionGoal(this, "potion_nonstat", "Find a Log Potion", false, 0, 1)
        };

        Goals.ForEach(g => g.Init());

        // update UI to display quest goals
        questPanelGameObject = GameObject.Find("Panel_Quest");
        QuestPanel = questPanelGameObject.GetComponent<QuestPanel>();
        QuestPanel.SetQuest();
        QuestPanel.InitializeQuestGoals();
    }
}