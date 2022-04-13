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
        if(playerInventory)
        {
            playerInventory.AddItem(ItemInstance, amount);
        }
            if (ItemInstance.itemCategory == ItemCategory.Weapon)
            {
                WeaponHolder playerWeapon = other.GetComponent<WeaponHolder>();
                if (playerWeapon.equippedWeapon != null)
                {
                    playerWeapon.equippedWeapon.weaponStats.totalBullets += pickupItem.amountValue;
                }
            }
        Destroy(gameObject);
    }
}
