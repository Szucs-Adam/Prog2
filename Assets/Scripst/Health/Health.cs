using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    public GameObject Player;


    [Header("Components")]
    [SerializeField] private Behaviour[] components;



    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth -_damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                
                foreach (Behaviour component in components)
                    component.enabled = false;
                dead = true;
            }
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private void Update()
    {
       if (Player.transform.position.y < 200)
        {
            TakeDamage(100);
        }
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    public void LoadGameOver()
    {
        SceneManager.LoadScene("Death");
        dead = false;
    }
}