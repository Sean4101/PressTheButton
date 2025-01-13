using TMPro;
using UnityEngine;

public class SerialMessageSender : MonoBehaviour
{
    public MessageManager messageManager;

    public TMP_InputField inputField;
    SerialController serialController;

    private void Awake()
    {
        serialController = GameObject.Find("Serial Controller").GetComponent<SerialController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Send();
        }
    }

    // Called when the user presses 'Enter'.
    public void Send()
    {
        if (inputField.text.Length > 0)
        {
            SendText(inputField.text);
            inputField.text = "";
        }
    }

    public void SendText(string text)
    {
        serialController.SendSerialMessage(text + "\r");
    }
}
