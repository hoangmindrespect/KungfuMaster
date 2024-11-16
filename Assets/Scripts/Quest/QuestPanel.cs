using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestPanel : MonoBehaviour
{
    [SerializeField] private GameObject questGameObject;
    [SerializeField] private Quest quest;

    [SerializeField] private TextMeshProUGUI questName;
    // Quest goals
    private List<TextMeshProUGUI> questGoalTexts = new List<TextMeshProUGUI>();
    [SerializeField] private TextMeshProUGUI questGoalPrefab;
    [SerializeField] private Transform questGoalPanel;

    public void SetQuest()
    {
        quest = questGameObject.GetComponent<Quest>();
        questName.text = quest.QuestName;

    }

    public void InitializeQuestGoals()
    {
        questGoalTexts.Clear();

        for (int i = 0; i < quest.Goals.Count; i++)
       {
           questGoalTexts.Add(Instantiate(questGoalPrefab));
           questGoalTexts[i].transform.SetParent(questGoalPanel);
           questGoalTexts[i].text = quest.Goals[i].Description + ": " + quest.Goals[i].CurrentAmount + "/" + quest.Goals[i].RequiredAmount;
       }
        CombatEvents.OnEnemyDeath += UpdateQuest;
    }

    void UpdateQuest(IEnemy enemy)
    {
        for (int i = 0; i < quest.Goals.Count; i++)
        {
            questGoalTexts[i].text = quest.Goals[i].Description + ": " + quest.Goals[i].CurrentAmount + "/" + quest.Goals[i].RequiredAmount;
        }
    }

    public void RemoveQuest()
    {
        CombatEvents.OnEnemyDeath -= UpdateQuest;
        questName.text = "-";
        for (int i = 0; i < quest.Goals.Count; i++)
        {
            Destroy(questGoalTexts[i].gameObject);
            //questGoalTexts.RemoveAt(i);
        }
    }
}
