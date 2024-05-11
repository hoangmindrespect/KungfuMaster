using UnityEngine;
using System.Collections;

public class ConsumableController : MonoBehaviour
{
    CharacterStats stats;

    // Use this for initialization
    void Start()
    {
        stats = GetComponent<CharacterStats>();
    }

    public void ConsumeItem(Item item)
    {
        GameObject itemToSpawn = Instantiate(Resources.Load<GameObject>("Consumables/" + item.ObjectSlug));
        if (item.ItemModifier)
        {
            itemToSpawn.GetComponent<IConsumable>().Consume(stats); // all of out things that can be consumed will be event so we know they can find it
                                                                    // by using GetComponent and looking for that component that shares the IConsumable interface and then consume().
        }
        else
            itemToSpawn.GetComponent<IConsumable>().Consume();
    }

}
