using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Player name panel")]
    public GameObject namePanel;
    public TMP_InputField nameInput;
    public TMP_Text error;

    private int pendingLevel = -1;

    public void Level1() => OpenNamePanel(1);
    
    public void Level2() => OpenNamePanel(2);
    
    public void Quit() => Application.Quit();

    void Start()
    {
        if (namePanel != null) namePanel.SetActive(false);
        if (error != null) error.text = "";
    }

    private void OpenNamePanel(int sceneIndex)
    {
        pendingLevel = sceneIndex;
        namePanel.SetActive(true);

        if (error != null) error.text = "";

        nameInput.text = PlayerSession.HasName ? PlayerSession.PlayerName : "";
        nameInput.ActivateInputField();
    }

    public void ConfirmName()
    {
        string name = nameInput.text.Trim();
        
        if (string.IsNullOrEmpty(name))
        {
            if (error != null) error.text = "Nazwa nie mo¿e byæ pusta!";
            return;
        }

        PlayerSession.SetName(name);
        SceneManager.LoadScene(pendingLevel);
    }

    public void CancelName()
    {
        namePanel.SetActive(false);
        pendingLevel = -1;
    }
}
