using System.Threading;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    
    public bool MotorIsOn = false;
    [SerializeField] private bool isGame = false;
    [SerializeField] private MessageManager messageManager;
    [SerializeField] private SerialManager serialManager;
    [SerializeField] private HUD hud;
    [SerializeField] private int count = 0;
    [SerializeField] private bool canUseBounce = false;
    [SerializeField] private bool canUseVariableResistor = false;
    [SerializeField] private bool canUseAccelerometer = false;
    [SerializeField] private bool canUseMotor = false;
    [SerializeField] private int prevVariableResistorValue = -1;
    [SerializeField] private int prevAccelerometerValue = -1;
    [SerializeField] private int accelerometerGauge = 0;
    float elapsed = 0f;
    private void Awake()
    {
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGame = true;
    }

    // Update is called once per frame
    void Update()
    {
        hud.progressText.text = count + "/1000";
        hud.progressBarScript.FillBar((float)count/1000);
        hud.gaugeBarScript.FillBar((float)accelerometerGauge/100);
        accelerometerGauge = Math.Max(accelerometerGauge, 0); // lower bound
        accelerometerGauge = Math.Min(accelerometerGauge, 100); // upper bound
        elapsed += Time.deltaTime;
        if (elapsed > 1f) 
        {
            if (!isGame)
            {
                if(MotorIsOn) { serialManager.SendText("m1"); }
                return;
            }
            CheckCountValue();
            if (MotorIsOn == false && canUseMotor == true) 
            {
                serialManager.SendText("m2");
            }
            if (MotorIsOn == true)
            {
                PressButton();
            }
            elapsed %= 1f;
            accelerometerGauge -= 2;
            
        }
    }
    public int CalculateMultiplier()
    {
        int result = 1;
        if (canUseBounce == true) { result *= BouncingMultiplier(); }
        if (canUseAccelerometer == true) { result *= GaugeMultiplier(); }
        return result;
    }
    public void PressButton() 
    {
        if (!isGame) return;
        int add = 1 * CalculateMultiplier();
        count += add;
    }
    public void UseVariableResistor()
    {
        if (canUseVariableResistor == false) return;
        if(prevVariableResistorValue == -1) 
        {
            prevVariableResistorValue = messageManager.variableResistorValue;
            return;
        }
        int added_value = Math.Min(Math.Abs(messageManager.variableResistorValue - prevVariableResistorValue) / 40, 5) * CalculateMultiplier();
        Debug.Log("variable + " + added_value);
        count += added_value;
        prevVariableResistorValue = messageManager.variableResistorValue;

    }
    
    public void UseAcceleroMeter()
    {
        if (canUseAccelerometer == false) return;
        if (prevAccelerometerValue == -1)
        {
            prevAccelerometerValue = messageManager.accelerometerValue;
            return;
        }
        int added_value = Math.Min(Math.Abs((messageManager.accelerometerValue - prevAccelerometerValue) / 6) - 1, 10);
        Debug.Log("accel + " + added_value);
        accelerometerGauge += added_value;
        prevAccelerometerValue = messageManager.accelerometerValue;
    }

    public void CheckCountValue()
    {
        if (count >= 100)
        {
            canUseMotor = true;   
        }   
        if(count >= 200)
        {
            canUseBounce = true;
        }
        if (count >= 300)
        {
            canUseVariableResistor = true;
        }
        if (count >= 500)
        {
            canUseAccelerometer = true;
        }
        if(count >= 1000)
        {
            EndGame();
        }
    }
    public int GaugeMultiplier()
    {
        if(accelerometerGauge >= 70)
        {
            return 3;
        }
        else if(accelerometerGauge >= 40)
        {
            return 2;
        }
        return 1;
    }
    public int BouncingMultiplier()
    {
        int roll = Random.Range(0, 100);
        if (roll >= 90) { return 4; }
        if (roll >= 70) { return 3; }
        if (roll >= 40) { return 2; }
        return 1;
    }
    public void EndGame()
    {
        isGame = false;
        hud.ShowWinScreen();
        serialManager.SendText("m1");
    }
}

