using TMPro;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    // Other scripts can access these variables to read the latest values.
    public int variableResistorValue;
    public int accelerometerValue;

    // FOr demonstration purposes, remove these variables and references if you don't need them.
    public TMP_Text varResText;
    public TMP_Text accelText;


    // Called by SerialMessageListener when a new message is received.
    public void ParseMessage(string msg)
    {
        int length = msg.Length;
        if (length == 0)
            return;

        char command = msg[0];

        switch (command)
        {
            case 'A':
                UpdateVariableResistorValue(msg);
                break;
            case 'B':
                UpdateAccelerometerValues(msg);
                break;
            case 'P':
                ButtonPressed();
                break;
            default:
                break;
        }
    }

    public void UpdateVariableResistorValue(string msg)
    {
        msg = msg.Substring(1);
        int.TryParse(msg, out variableResistorValue);
        varResText.text = "Variable Resistor: " + variableResistorValue;
    }

    public void UpdateAccelerometerValues(string msg)
    {
        msg = msg.Substring(1);
        int.TryParse(msg, out accelerometerValue);
        accelText.text = "Accelerometer: " + accelerometerValue;
    }


    // Call whatever you want to happen when the button is pressed.
    public void ButtonPressed()
    {
        Debug.Log("Button pressed!");
    }
}
