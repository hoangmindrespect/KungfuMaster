using UnityEngine;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class ConsumableController : MonoBehaviour
{
    public CharacterStats characterStats;
    public Player player;
    private void Start()
    {
        characterStats = GetComponent<CharacterStats>(); // Tạm thời, CharacterStats và ConsumableController đều là component gắn vào Object Player nên tại file ConsumableController có thể gọi tới đúng CharacterStats.
                                                         // Còn nếu từ 1 object khác, chẳng hạn potionLog tìm tới component này thông qua GameObject.Find("Player").GetComponent<CharacterStats>() thì sẽ bị null, nguyên nhân chưa biết.
        player = GetComponent<Player>();
    }

    public void ConsumeItem(Item item)
    {
        GameObject itemToSpawn = Instantiate(Resources.Load<GameObject>("Consumables/" + item.ObjectSlug));
        if (item.ItemModifier)
        {
            itemToSpawn.GetComponent<IConsumable>().Consume(item.Stats); // all of out things that can be consumed will be event so we know they can find it
                                                                         // by using GetComponent and looking for that component that shares the IConsumable interface and then consume().
            characterStats.AddStatBonus(item.Stats);
            UIEventHandler.StatsChanged();
        }
        else
        {
            itemToSpawn.GetComponent<IConsumable>().Consume();
            player.Healing(2);
        }
    }

}
