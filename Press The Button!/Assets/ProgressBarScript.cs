using UnityEngine;
using UnityEngine.UI;
using System;
public class ProgressBarScript : MonoBehaviour
{
    [SerializeField] private Image mask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillBar(float fillAmount)
    {
        mask.fillAmount = Math.Min(Math.Max(fillAmount, 0f), 1f);
    }
}
