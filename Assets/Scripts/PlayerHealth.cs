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
        CanvasManager.Instance.updateHealth(health);
        CanvasManager.Instance.updateArmor(armor);
        
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
    
        if(health <= 0)
        {
            //restart everything
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex, LoadSceneMode.Single);
        }
        CanvasManager.Instance.updateHealth(health);
        CanvasManager.Instance.updateArmor(armor);
    }

    public bool heal(int amount)
    {
        if(health < maxHealth)
        {
            this.health += amount;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            CanvasManager.Instance.updateHealth(health);
            return true;
        }
        return false;

    }

    public bool shield(int amount)
    {
        if(armor < maxArmor)
        {
            this.armor += amount;
            if (armor > maxArmor)
            {
                armor = maxArmor;
            }
            CanvasManager.Instance.updateArmor(armor);
            return true;
        }
        return false;
    }
}
