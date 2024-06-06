using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{

    SceneManagerScript scene = new SceneManagerScript();
    SaveLoadFile io = new SaveLoadFile();
    string player_data_file = "user_data.json";
    JObject player_data;
    public PlayerCombat PlayerCombat;
    public Animator animator;
    public PlayerMovement playerMovement;
    public int maxHealth;
    private int currentHealth;
    private Rigidbody2D rb;
    private Collider2D col;
    private string potionName;
    public GameManager gameManager;
    Vector2 moveDirection;


    void Start()
    {
        player_data = JObject.Parse(io.Load_to_file(player_data_file));
        maxHealth = int.Parse(player_data["HP"].ToString());
        int speed = 1;
        int jump = 1;
        int.TryParse(player_data["Skills"]["Speed"].ToString(), out speed);
        int.TryParse(player_data["Skills"]["Jump"].ToString(), out jump);
        playerMovement.speed = playerMovement.speed * (1 + speed);
        playerMovement.jump = playerMovement.jump * (1 + jump);
        currentHealth = maxHealth;

        potionName = player_data["ItemsList"]["Potion"].ToString();
       if (player_data["ItemsList"] != null && player_data["ItemsList"]["Potion"] != null)
        {
            UsePotion(potionName);
        }
        Debug.Log("Potion loaded: " + potionName);
        Debug.Log("Initial Health: " + currentHealth);
    }

    void Update()
    {
        player_data = JObject.Parse(io.Load_to_file(player_data_file));
        potionName = player_data["ItemsList"]["Potion"].ToString();
        if (Input.GetKeyDown(KeyCode.Mouse2))
        { 
            Debug.Log("Middle mouse button clicked.");
            UsePotion(potionName);
            player_data["ItemsList"]["Potion"] = "";
            io.Save_to_file(player_data.ToString(), player_data_file);
            Debug.Log("Health after using potion: " + currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("You Died");

        GameManager.instance.PlayerDied();

        animator.SetBool("IsDead", true);

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.velocity = Vector2.zero;
        }

        if (col != null)
        {
            col.enabled = false;
        }

        gameObject.layer = LayerMask.NameToLayer("Dead");

        this.enabled = false;

        if (PlayerCombat != null)
        {
            PlayerCombat.enabled = false;
        }

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        this.enabled = false;

        FindObjectOfType<GameManager>().GameOver();
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    public void UsePotion(string potionName)
    {
        if (potionName == "RedPotion")
        {
            currentHealth += 20;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            Debug.Log("RedPotion used. Current Health: " + currentHealth);
        }
        else if (potionName == "YellowPotion")
        {
            playerMovement.speed += 5;
            Debug.Log("YellowPotion used. Current Speed: " + playerMovement.speed);
        }
        else if (potionName == "BluePotion")
        {
            PlayerCombat.attackDamage += 10;
            Debug.Log("BluePotion used. Current Damage: " + PlayerCombat.attackDamage);
        }

        
    }
}
