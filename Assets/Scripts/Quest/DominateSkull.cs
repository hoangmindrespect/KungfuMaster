using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominateSkull : Quest
{
    void Start()
    {
        Debug.Log("Dominate skull assigned.");
        QuestName = "Dominate Skull";
        Description = "Kill nine skeleton.";
        ItemReward = ItemDatabase.Instance.GetItem("spear");
        ExperienceReward = 200;
        Goals = new List<Goal>
        {
            new KillGoal(this, 2, "Kill 9 Skeleton", false, 0, 9),
        };

        Goals.ForEach(g => g.Init());

        // update UI to display quest goals
        questPanelGameObject = GameObject.Find("Panel_Quest");
        QuestPanel = questPanelGameObject.GetComponent<QuestPanel>();
        QuestPanel.SetQuest();
        QuestPanel.InitializeQuestGoals();
    }
}
