 using UnityEngine;
 using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    public Gamepad playerControl;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private InputAction attack;



    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerControl = new Gamepad();
        cooldownTimer += Time.deltaTime;
    }

    private void OnEnable()
    {
        attack = playerControl.Player.Attack;
        attack.Enable();

    }

    private void OnDisable()
    {
        attack.Disable();
    }

    private void Update()
    {
       cooldownTimer += Time.deltaTime;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed == true)
        {
            anim.SetTrigger("attack");
            cooldownTimer = 0;
           
            fireballs[FindFireball()].transform.position = firePoint.position;
            fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
            
        }

     
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }


}