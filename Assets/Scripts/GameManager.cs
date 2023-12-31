using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public int health = 100;
    public int money = 500;

    [Header("Components")] 
    public TextMeshProUGUI healthAndMoneyText;
    public EnemyPath enemyPath;

    [Header("Events")] 
    public UnityEvent onEnemyDestroyed;
    public UnityEvent onMoneyChanged;

    //Singleton
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UpdateHealthAndMoneyText();
        
        onMoneyChanged.Invoke();
    }
    
    public void TakeMoney(int amount)
    {
        money -= amount;
        UpdateHealthAndMoneyText();
        
        onMoneyChanged.Invoke();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        UpdateHealthAndMoneyText();

        if (health <= 0)
            GameOver();
    }
    
    public void OnEnemyDestroyed()
    {
        
    }
    
    void UpdateHealthAndMoneyText()
    {
        //healthAndMoneyText.text = $"Health: {health} \nMoney: {money}";
    }

    void GameOver()
    {
        
    }

    void WinGame()
    {
        
    }
}
