using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSlayer : Quest
{
    void Start()
    {
        Debug.Log("Skeleton slayer assigned.");
        QuestName = "Skeleton Slayer";
        Description = "Kill one skeleton.";
        ItemReward = ItemDatabase.Instance.GetItem("potion_nonstat");
        ExperienceReward = 100;
        Goals = new List<Goal>
        {
            new KillGoal(this, 2, "Kill 1 Skeleton", false, 0, 1),
        };

        Goals.ForEach(g => g.Init());

        // update UI to display quest goals
        questPanelGameObject = GameObject.Find("Panel_Quest");
        QuestPanel = questPanelGameObject.GetComponent<QuestPanel>();
        QuestPanel.SetQuest();
        QuestPanel.InitializeQuestGoals();
    }
}
