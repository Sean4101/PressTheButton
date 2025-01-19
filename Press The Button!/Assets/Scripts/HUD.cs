using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public GameObject interactiPanel;
    public GameObject winPanel;
    public ProgressBarScript progressBarScript;
    public ProgressBarScript gaugeBarScript;
    public TMP_Text progressText;

    private void Start()
    {
        winPanel.SetActive(false);
    }
    public void ToggleInteractPanel()
    {
        interactiPanel.SetActive(!interactiPanel.activeSelf);
    }
    public void ShowWinScreen()
    {
        winPanel.SetActive(true);
    }
}
