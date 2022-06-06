using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerMultiplayer : NetworkBehaviour
{
    public float health; //nombre de health
    public float maxHealth; //Santé Max
    public int bonusHealth; //nombre de health bonus
    public int speed; //stat de vitesse
    public int attack; //stat d'attaque
    public int shotSpeed; //vitesse des projeciles
    public float cooldown; //cadence de l'attaque
    public float attackRange; //distance d'attaque
    public int gold;
    public GameObject bullet;
    Rigidbody2D rb;
    public Animator animator;
    private float actualcooldown;
    public float Health => health;

    public bool god = true;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        if (PlayerPrefs.HasKey("health"))
        {
            health = PlayerPrefs.GetFloat("health");
        }
        else
        {
            health = maxHealth;
        }

        rb = GetComponent<Rigidbody2D>();
        actualcooldown = cooldown;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;
        if (IsAlive())
        {
            if (health < 5)
                ManagerSfx.PlaySound("Health");
            if (actualcooldown <= 0)
            {
                CmdFire(Input.GetAxis("FireHorizontal"),Input.GetAxis("FireVertical"));
                actualcooldown = cooldown;
            }
            else
                actualcooldown -= Time.deltaTime;


            Moveto();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void addstat(float he, int boHe, int sp, int at, int shSp, float co, float atRa, int go)
    {
        this.health += he;
        if (this.health > this.maxHealth)
        {
            this.health = this.maxHealth;
        }

        this.bonusHealth += boHe;
        this.speed += sp;
        this.attack += at;
        this.shotSpeed += shSp;
        this.cooldown -= co;
        this.attackRange += atRa;
        this.gold += go;
    }

    void Moveto()
    {
        // ManagerSfx.PlaySound("playerWalk");
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal * speed, vertical * speed);
        rb.velocity = (move * speed * Time.deltaTime);

        //animation
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Speed", move.magnitude);
    }

    bool IsAlive() //if player still alive
    {
        return (this.health + this.bonusHealth > 0);
    }

    [Command]
    void CmdFire(float fireHorizontal, float fireVertical)
    {
        switch (fireVertical)
        {
            case 1:
            {
                Vector3 direction = Vector2.up;
                var bul = Instantiate(bullet, transform.position + direction * 2, Quaternion.identity);
                var tir = bul.GetComponent<AllyBulletMultiplayer>();
                tir.Setup(attack, shotSpeed, attackRange, direction);
                NetworkServer.Spawn(bul);
                break;
            }
            case -1:
            {
                Vector3 direction = Vector2.down;
                var bul = Instantiate(bullet, transform.position + direction * 2, Quaternion.identity);
                var tir = bul.GetComponent<AllyBulletMultiplayer>();
                tir.Setup(attack, shotSpeed, attackRange, direction);
                NetworkServer.Spawn(bul);
                break;
            }
            default:
            {
                switch (fireHorizontal)
                {
                    case 1:
                    {
                        Vector3 direction = Vector2.right;
                        var bul = Instantiate(bullet, transform.position + direction * 2, Quaternion.identity);
                        var tir = bul.GetComponent<AllyBulletMultiplayer>();
                        tir.Setup(attack, shotSpeed, attackRange, direction);
                        NetworkServer.Spawn(bul);
                        break;
                    }
                    case -1:
                    {
                        Vector3 direction = Vector2.left;
                        var bul = Instantiate(bullet, transform.position + direction * 2, Quaternion.identity);
                        var tir = bul.GetComponent<AllyBulletMultiplayer>();
                        tir.Setup(attack, shotSpeed, attackRange, direction);
                        NetworkServer.Spawn(bul);
                        break;
                    }
                }

                break;
            }
        }
    }
}