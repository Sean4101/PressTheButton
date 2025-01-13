using UnityEngine;
using TMPro;

public class SerialMessageListener : MonoBehaviour
{
    public MessageManager messageManager;

    public TMP_Text text;
    public int maxLines = 20;

    int lines = 0;

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
        }

        lines = text.text.Split('\n').Length;

        while (lines > maxLines)
        {
            int index = text.text.IndexOf('\n');
            text.text = text.text.Substring(index + 1);
            lines--;
        }
    }
}
