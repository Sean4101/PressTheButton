using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameObject interactiPanel;

    public void ToggleInteractPanel()
    {
        interactiPanel.SetActive(!interactiPanel.activeSelf);
    }
}
