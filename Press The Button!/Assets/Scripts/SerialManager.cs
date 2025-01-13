using TMPro;
using UnityEngine;

public class SerialManager : MonoBehaviour
{
    MessageManager messageManager;
    SerialController serialController;

    [Header("Serial Controller")]
    public TMP_InputField portNameInputField;
    string portName = "";

    [Header("Sender")]
    public TMP_InputField inputField;

    [Header("Listener")]
    public TMP_Text text;
    public int maxLines = 20;

    int lines = 0;

    private void Awake()
    {
        messageManager = GetComponent<MessageManager>();
        serialController = GetComponent<SerialController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Send();
        }
    }

    public void Connect()
    {
        portName = portNameInputField.text;
        serialController.portName = portName;
        serialController.enabled = false;
        Invoke("EnableSerialController", 0.5f);
    }

    void EnableSerialController()
    {
        serialController.enabled = true;
    }

    // ---------------------------------------------------------------------
    // Sender methods
    // ---------------------------------------------------------------------

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

    // ---------------------------------------------------------------------
    // Listener methods
    // ---------------------------------------------------------------------

    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        if (text != null)
        {
            AppendMessage(success ? "Connected" : "Disconnected");
        }
    }

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        AppendMessage(msg);
        messageManager.ParseMessage(msg);
    }

    void AppendMessage(string msg)
    {
        if (text != null)
        {
            text.text += msg + "\n";
            lines++;
        }
        if (lines > maxLines)
        {
            text.text = text.text[(text.text.IndexOf('\n') + 1)..];
            lines--;
        }
    }
}
