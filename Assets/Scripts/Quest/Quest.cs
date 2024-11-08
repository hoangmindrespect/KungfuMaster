using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEditor.Progress;

public class Quest : MonoBehaviour
{
    private int level;
    public int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }

    public List<Goal> Goals { get; set; }
    public string QuestName { get; set; }
    public string Description { get; set; }
    public int ExperienceReward { get; set; }
    public Item ItemReward { get; set; }
    public bool Completed { get; set; }

    // SETTING FOR CONNECT TO QUESTPANEL
    public GameObject questPanelGameObject;
    public QuestPanel QuestPanel;

    private void Start()
    {
        questPanelGameObject = GameObject.Find("Panel_Quest");
        QuestPanel = questPanelGameObject.GetComponent<QuestPanel>();
    }

    public void CheckGoals()
    {
        Completed = Goals.All(g => g.Completed);
        if(Completed)
        {
            Debug.Log("Quest finish");
            GiveReward();
            QuestPanel.RemoveQuest();
        }
    }

    public void GiveReward()
    {
        if (ItemReward != null)
        {
            InventoryController.Instance.GiveItem(ItemReward);
        }
    }
}