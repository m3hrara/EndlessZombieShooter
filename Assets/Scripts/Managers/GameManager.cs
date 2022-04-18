using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : Singleton<GameManager>
{
    public int zombieKills = 0;
    public float timer = 120;
    public GameObject gameoverPanel;
    public GameObject winPanel;
    public bool cursorActive = true;
    public TMP_Text killText;
    public TMP_Text timeText;
    public PlayerController playerController;

    void Awake()
    {
        Time.timeScale = 1;
    }
    void EnableCursor(bool enable)
    {
        if (enable)
        {
            cursorActive = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            cursorActive = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void OnEnable()
    {
        AppEvents.MouseCursorEnabled += EnableCursor;
    }
    private void OnDisable()
    {
        AppEvents.MouseCursorEnabled -= EnableCursor;
    }

    public void Update()
    {
        killText.text = zombieKills.ToString();
        timer -= Time.deltaTime;
        timeText.text = Mathf.Round(timer).ToString();
        if (playerController.healthComponent.CurrentHealth <= 0)
        {
            Time.timeScale = 0f;
            gameoverPanel.SetActive(true);
        }
        if (timer <= 0f && zombieKills >= 20)
        {
            Time.timeScale = 0f;
            winPanel.SetActive(true);
        }
    }
}