using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuBehaviour : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject creditsPanel;
    public GameObject menuPanel;


    public void onStartPressed()
    {
        if (menuPanel != null && startPanel != null)
        {
            startPanel.SetActive(false);
            menuPanel.SetActive(true);
        }

    }
    public void onPlayPressed()
    {
        SceneManager.LoadScene("Game");
    }
    public void onExitPressed()
    {
        Application.Quit();
    }
    public void onCreditsPressed()
    {
        if (menuPanel != null && creditsPanel != null)
        {
            menuPanel.SetActive(false);
            creditsPanel.SetActive(true);
        }

    }
    public void onMenuPausePessed()
    {
        SceneManager.LoadScene("Menu");
    }

    public void onBackPressed()
    {
        if (menuPanel != null && creditsPanel != null)
        {
            menuPanel.SetActive(true);
            creditsPanel.SetActive(false);
        }
    }
    public void onRestartPessed()
    {
        SceneManager.LoadScene("Game");
    }
}
