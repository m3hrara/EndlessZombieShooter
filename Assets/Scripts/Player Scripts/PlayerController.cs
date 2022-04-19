using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public HealthComponent healthComponent;
    public MovementComponent movementComponent;
    public bool isFiring;
    public bool isReloading;
    public bool isJumping;
    public bool isRunning;
    public bool isAiming;
    public bool isInventoryOn = false;
    public InventoryComponent inventory;
    public GameUIController gameUIController;
    public WeaponHolder weaponHolder;
    public GameObject pausePanel;
    public bool isPaused = false;
    public void OnInventory(InputValue value)
    {
        isInventoryOn = !isInventoryOn;
        gameUIController.ToggleInventory(isInventoryOn);
        AppEvents.InvokeMouseCursorEnable(isInventoryOn);

    }
    public void OnPause(InputValue value)
    {
        if (isPaused)
        {
            //unpause
            Cursor.visible = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            //pause
            Cursor.visible = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        isPaused = !isPaused;

    }
    public void onResumePressed()
    {
        Cursor.visible = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = !isPaused;

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
        if (movementComponent == null)
        {
            movementComponent = GetComponent<MovementComponent>();
        }
    }
}