using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxeSkillImage : MonoBehaviour {

    bool isActive;
    float coolDownCount;
    Image skillImg;

    private void Start()
    {
        skillImg = gameObject.GetComponent<Image>();
    }

    private void Update()
    {

        if (skillImg.fillAmount == 1)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }

        if (isActive)
        {
            skillImg.color = Color.white;
        }
        else
        {
            skillImg.color = Color.red;
            skillImg.fillAmount += Time.deltaTime / coolDownCount;
        }
    }

    public void SetAxeCD(float cd)
    {
        skillImg.fillAmount = 0;
        coolDownCount = cd;
    }

  
}
