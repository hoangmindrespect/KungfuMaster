using UnityEngine;
using System.Collections;

public class PlayerWeaponController : MonoBehaviour
{
    public Animator animator;
    public playerMovement playerMovement;

    //public GameObject playerHand;
    public GameObject EquippedWeapon { get; set; }

    //Transform spawnProjectile;
    public Item currentlyEquippedItem;
    public IWeapon equippedWeapon;
    public CharacterStats characterStats;

    void Start()
    {
        //spawnProjectile = transform.Find("ProjectileSpawn");
        characterStats = GetComponent<Player>().characterStats;
        characterStats = GetComponent<CharacterStats>();

        playerMovement = GetComponent<playerMovement>();
        animator = GetComponent<Animator>();
    }

    public void EquipWeapon(Item itemToEquip)
    {
        if (EquippedWeapon != null)
        {
            UnequipWeapon();
        }

        EquippedWeapon = (GameObject)Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug)
            /*, playerHand.transform.position, playerHand.transform.rotation*/);
        equippedWeapon = EquippedWeapon.GetComponent<IWeapon>();
        //if (EquippedWeapon.GetComponent<IProjectileWeapon>() != null)
        //    EquippedWeapon.GetComponent<IProjectileWeapon>().ProjectileSpawn = spawnProjectile;
        //EquippedWeapon.transform.SetParent(playerHand.transform);
        equippedWeapon.Stats = itemToEquip.Stats;
        currentlyEquippedItem = itemToEquip;
        characterStats.AddStatBonus(itemToEquip.Stats);
        UIEventHandler.ItemEquipped(itemToEquip);
        UIEventHandler.StatsChanged();

        // Set animator back to player when eqqip
        if (itemToEquip.ObjectSlug == "fire")
        {
            playerMovement.resetPlayerState();
            animator.SetBool("isElementState", true);
        }
        else if (itemToEquip.ObjectSlug == "sword")
        {
            playerMovement.resetPlayerState();
            animator.SetBool("isWeaponState", true);
        }

        //RESET PARAMETER OF ANIMATOR
        playerMovement.setAnimator();

        //Debug.Log(equippedWeapon.Stats[0].GetCalculatedStatValue());
    }

    public void UnequipWeapon()
    {
        InventoryController.Instance.GiveItem(currentlyEquippedItem.ObjectSlug);
        characterStats.RemoveStatBonus(equippedWeapon.Stats);
        Destroy(EquippedWeapon.transform.gameObject);
        UIEventHandler.StatsChanged();

        playerMovement.resetPlayerState();
        animator.SetBool("isNoneState", true);
        playerMovement.setAnimator();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.X))
        //    PerformWeaponAttack();
        //if (Input.GetKeyDown(KeyCode.Z))
        //    PerformWeaponSpecialAttack();
    }

    public void PerformWeaponAttack()
    {
        equippedWeapon.PerformAttack(CalculateDamage());
    }
    public void PerformWeaponSpecialAttack()
    {
        equippedWeapon.PerformSpecialAttack();
    }

    private int CalculateDamage()
    {
        int damageToDeal = (characterStats.GetStat(BaseStat.BaseStatType.ATK).GetCalculatedStatValue() * 2)
            + Random.Range(2, 8);
        damageToDeal += CalculateCrit(damageToDeal);
        Debug.Log("Damage dealt: " + damageToDeal);
        return damageToDeal;
    }

    private int CalculateCrit(int damage)
    {
        if (Random.value <= .10f)
        {
            int critDamage = (int)(damage * Random.Range(.5f, .75f));
            return critDamage;
        }
        return 0;
    }
}
