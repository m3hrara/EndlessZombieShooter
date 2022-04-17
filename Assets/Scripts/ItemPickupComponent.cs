using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupComponent : MonoBehaviour
{
    [SerializeField] ItemScript pickupItem;
    [SerializeField] int amount = -1;
    [SerializeField] MeshRenderer propMeshRenderer;
    [SerializeField] private MeshFilter propMeshFilter;
    private ItemScript ItemInstance;

    void Start()
    {
        InstantiateItem();
    }

    private void InstantiateItem()
    {
        ItemInstance = Instantiate(pickupItem);
        if (amount > 0)
        {
            ItemInstance.SetAmount(amount);
        }
        else
        {
            ItemInstance.SetAmount(pickupItem.amountValue);
        }

        ApplyMesh();
    }

    private void ApplyMesh()
    {
        if (propMeshFilter) propMeshFilter.mesh = pickupItem.itemPrefab.GetComponentInChildren<MeshFilter>().sharedMesh;
        if (propMeshRenderer) propMeshRenderer.materials = pickupItem.itemPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterials;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))return;
        InventoryComponent playerInventory = other.GetComponent<InventoryComponent>();
        WeaponHolder weaponHolder = other.GetComponent<WeaponHolder>();

        if (playerInventory)
        {
            playerInventory.AddItem(ItemInstance, amount);
        }
        if (ItemInstance.itemCategory == ItemCategory.Weapon)
        {
            WeaponComponent tempWeaponData = ItemInstance.itemPrefab.GetComponent<WeaponComponent>();
            if (weaponHolder.WeaponAmmoData.ContainsKey(tempWeaponData.weaponStats.weapontype))
            {
                WeaponStats tempWeaponStats = weaponHolder.WeaponAmmoData[tempWeaponData.weaponStats.weapontype];
                tempWeaponStats.totalBullets += ItemInstance.amountValue;

                other.GetComponentInChildren<WeaponHolder>().WeaponAmmoData[tempWeaponData.weaponStats.weapontype] = tempWeaponStats;
                if (weaponHolder.equippedWeapon != null)
                {
                    weaponHolder.equippedWeapon.weaponStats = weaponHolder.WeaponAmmoData[tempWeaponStats.weapontype];
                }
            }
        }
        Destroy(gameObject);
    }
}
