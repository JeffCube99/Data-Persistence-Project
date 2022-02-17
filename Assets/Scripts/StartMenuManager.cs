using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public TextMeshProUGUI greetingText;
    public Button startButton;

    private void Awake()
    {
        startButton.interactable = false;
    }

    public void SetPlayerName()
    {
        PlayerInformationManager playerInfo = PlayerInformationManager.Instance;
        if (playerInfo != null)
        {
            playerInfo.name = playerNameInput.text;
            if (!string.IsNullOrEmpty(playerInfo.name))
            {
                greetingText.text = "Hello " + playerInfo.name;
                startButton.interactable = true;
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
