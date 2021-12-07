using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;

    private void OnTriggerEnter2D( Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            gameObject.SetActive(false);
            collision.GetComponent<Health>().AddHealth(healthValue);
        }
    }
}