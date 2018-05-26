using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LifeBar : MonoBehaviour {
    private Image lifeBar;
    private Light[] lifes;
    private int life_amount = 3;

	public void Start()
	{
        lifeBar = GameObject.Find("LifeBar").GetComponent<Image>();
        lifes = new Light[5];

        lifes[0] = GameObject.Find("Life_1").GetComponent<Light>();
        lifes[1] = GameObject.Find("Life_2").GetComponent<Light>();
        lifes[2] = GameObject.Find("Life_3").GetComponent<Light>();
        lifes[3] = GameObject.Find("Life_4").GetComponent<Light>();
        lifes[4] = GameObject.Find("Life_5").GetComponent<Light>();

        for (int i = 0; i < 5; i++) {
            lifes[i].intensity = (float) (life_amount > i ? 0.8 : 0);
        }
	}

	public void decreaseLifeBar ()
    {
        lifeBar.fillAmount -= (float) 0.1;
    }

    public void increaseLifeBar ()
    {
        lifeBar.fillAmount += (float) 0.1;

    }

    public void setLife(float value) {
        lifeBar.fillAmount = value;
    }

    public void addLife() {
        if(life_amount < 5) {
            lifes[life_amount].intensity = (float)0.8;
            life_amount++;
        }
    }

    public void removeLife()
    {
        if (life_amount > 0)
        {
            lifes[life_amount-1].intensity = (float)0;
            life_amount--;
        }
    }
}
