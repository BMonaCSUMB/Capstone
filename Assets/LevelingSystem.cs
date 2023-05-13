using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelingSystem : MonoBehaviour
{
  public int level;
  public float currentXp;
  public float requiredXp;
  private float delayTimer;
  [Header("UI")]
  public Image frontXpBar;
  public Image backXpBar;
  [Header("Multipliers")]
  [Range(1f, 300f)]
  public float additionMultiplier = 300;
  [Range(2f, 4f)]
  public float powerMultiplier = 2;
  [Range(7f, 14f)]
  public float divisionMultiplier = 7;
  public TextMeshProUGUI levelText;

  void Start()
  {
    // Set bar fill amounts
    frontXpBar.fillAmount = currentXp / requiredXp;
    backXpBar.fillAmount = currentXp / requiredXp;
    // Set required Xp
    requiredXp = CalcRequiredXp();
    // Textmeshpro Level text above player health 
    levelText.text = "Level " + level;
  }

  void Update()
  {
    //Debug.Log(enemyScript.EnemySlain);
    updateXp();
    // If player has enough xp to level
    if (currentXp > requiredXp)
    {
      // Level function
      LevelUp();
    }
  }

  public void updateXp()
  {
    float xpFraction = currentXp / requiredXp;
    float FXP = frontXpBar.fillAmount;
    if (FXP < xpFraction)
    {
      delayTimer += Time.deltaTime;
      backXpBar.fillAmount = xpFraction;
    }
  }

  public void GainExp(float xpGained)
  {
    currentXp += xpGained;
  }

  public void LevelUp()
  {
    // Increment level
    level++;
    // Set xp fill
    frontXpBar.fillAmount = 0f;
    backXpBar.fillAmount = 0f;
    currentXp = Mathf.RoundToInt(currentXp - requiredXp);
    // Calculate required xp for next level
    requiredXp = CalcRequiredXp();
    // Set textmeshpro Level
    levelText.text = "Level " + level;
  }

  private int CalcRequiredXp()
  {
    int solveRequiredXp = 0;
    for ( int levelCycle = 1; levelCycle <= level; levelCycle++)
    { 
      // Runescape Algorithm For XP
      solveRequiredXp += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
    } 
    return solveRequiredXp / 4;
  }
}

