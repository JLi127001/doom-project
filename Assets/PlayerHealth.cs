using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    private int health;

    public int maxArmor = 100;
    private int armor;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        armor = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damagePlayer(int damage)
    {
        int accumulatedDamage = damage;
        if (armor > 0)
        {
            //take damage from armor
            armor = armor - damage;
            if (armor < 0)
            {
                accumulatedDamage = Mathf.Abs(armor);
                armor = 0;
                health -= accumulatedDamage;
            }
        }
        else
        {
            health -= accumulatedDamage;
        }
    
        if(health >= 0)
        {
            //restart everything
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }
}
