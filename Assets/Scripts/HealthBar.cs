using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Damageable playerDamageable;

    public TMP_Text healthBarText;
    public Slider healthSlider;

    public float timerOnceDead = 1f;
    

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null )
        {
            Debug.Log("No player found in scene, make sure it has tag 'Player' ");
        }
        playerDamageable = player.GetComponent<Damageable>();
    }

    void Start()
    {   
        healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "Health " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private void OnPlayerHealthChanged( int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "Health " + newHealth + " / " + maxHealth;
    }

    private void Update()
    {
        if (playerDamageable.Health <= 0 )
        {
            DeathTimer();
        }
    }

    private void DeathTimer()
    {
        if (timerOnceDead > 0)
        {
            timerOnceDead -= Time.deltaTime;
        }

        if (timerOnceDead <= 0 )
        {
            SceneManager.LoadSceneAsync("Game Over");
            Debug.Log("Death Timer completed");
        }
        
    }
}