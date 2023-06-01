using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public static UIHandler instance;
    [SerializeField] Image StaminaFillImage;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
        {
            Destroy(instance);
        }
    }

    public void FillStaminaUI(float currentStamina , float maxStamina)
    {
        StaminaFillImage.fillAmount = currentStamina/maxStamina;
    }
}
