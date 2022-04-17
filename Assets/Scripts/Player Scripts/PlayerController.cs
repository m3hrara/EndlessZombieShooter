using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public HealthComponent healthComponent;
    public bool isFiring;
    public bool isReloading;
    public bool isJumping;
    public bool isRunning;
    public bool isAiming;
    public bool isInventoryOn = false;
    public InventoryComponent inventory;
    public GameUIController gameUIController;
    public WeaponHolder weaponHolder;
    public void OnInventory(InputValue value)
    {
        isInventoryOn = !isInventoryOn;
        gameUIController.ToggleInventory(isInventoryOn);
        AppEvents.InvokeMouseCursorEnable(isInventoryOn);

    }
    private void Awake()
    {
        if(inventory == null)
        {
            inventory = GetComponent<InventoryComponent>();
        }
        if (gameUIController == null)
        {
            gameUIController = GetComponent<GameUIController>();
        }
        if (healthComponent == null)
        {
            healthComponent = GetComponent<HealthComponent>();
        }
        if (gameUIController == null)
        {
            gameUIController = FindObjectOfType<GameUIController>();
        }
        if (weaponHolder == null)
        {
            weaponHolder = GetComponent<WeaponHolder>();
        }
    }
}