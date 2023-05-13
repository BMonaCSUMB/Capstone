using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
   public Slider slider; 
   public Gradient gradient;
   public Image fill;
   
   public void setMaxHealth(int health)
   {
      // Set max value of slider
      slider.maxValue = health;
      // Set slider value
      slider.value = health;
      // Fill color is gradient
      fill.color = gradient.Evaluate(1f);
   }

   public void setHealth(int health)
   {
      // Set slider value to value of health
      slider.value = health;
      // Change color of fill using evaluate
      fill.color = gradient.Evaluate(slider.normalizedValue);
   }
}
