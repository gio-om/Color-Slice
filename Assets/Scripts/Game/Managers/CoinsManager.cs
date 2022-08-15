using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsManager : Manager<CoinsManager>
{
    [HideInInspector] public int CoinsAmount;
    public GameObject[] Effects;
    public TextMeshProUGUI CoinsText;

    public int CubesToIncreaseBonusByOne = 2;
    public float TimeToShowEffect = 5f;

    private int cubeStreak = 0;
    private int streakBonus = 1;

    private void Start()
    {
        CoinsAmount =  PlayerPrefs.GetInt("CoinsAmount");
        //CoinsText.text = CoinsAmount.ToString();
    }

    public void AddCoins(int CoinsToAdd)
    {
        CoinsAmount += CoinsToAdd * streakBonus;
        PlayerPrefs.SetInt("CoinsAmount", CoinsAmount);
        CoinsText.text = CoinsAmount.ToString();
    }

    public void AddCubeToStreak()
    {
        cubeStreak += 1;
        streakBonus = 1 + cubeStreak / CubesToIncreaseBonusByOne;
        if(streakBonus > 1)
        {
            ShowEffect();
        }
    }

    public void ClearStreak()
    {
        cubeStreak = 0;
        streakBonus = 1;
    }

    public void ShowEffect()
    {
        int effectIndex = Random.Range(0, Effects.Length);
        StartCoroutine(EffectCoroutine(effectIndex));
    }

    private IEnumerator EffectCoroutine(int effectIndex)
    {
        Effects[effectIndex].SetActive(true);
        yield return new WaitForSeconds(TimeToShowEffect);
        Effects[effectIndex].SetActive(false);
    }
}
