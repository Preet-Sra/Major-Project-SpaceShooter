using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Animator anim;
    public float BossSpeed;
    public float health=30;
    private Slider HealthBar;
    public Transform ShootPoint;
    public GameObject Explosion;
    float TimeBetweenShoots=0;
    public float WaitingTimeToShoot=2f;
    public GameObject EnemyBullet;

    

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _audioClip;

    private void Start()
    {
        HealthBar = FindObjectOfType<Slider>();
        HealthBar.maxValue = health;
        HealthBar.value = health;
        HealthBar.minValue = 0;
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health > 15)
        {
            transform.Translate((Vector3.down * BossSpeed) * Time.deltaTime);

            if (transform.position.y < -7.2f)
            {
                float RandomX = Random.Range(-7.6f, 7.6f);
                transform.position = new Vector3(RandomX, 6.35f, 0);
                anim.SetTrigger("Shoot");
            }

        }
        else
        {
            Vector3 temp = transform.position;
            temp.y = 2.95f;
            if (temp.x <= -7.5)
            {
                temp.x = 7.5f;
            }
            transform.position = temp;
            transform.Translate((Vector3.left*BossSpeed)*Time.deltaTime);

            if (TimeBetweenShoots >= WaitingTimeToShoot)
            {
                TimeBetweenShoots = 0;
                anim.SetTrigger("Shoot");
            }
            else
            {
                TimeBetweenShoots += Time.deltaTime;
            }
        }
        HealthBar.value = health;
    }


    public void Shoot()
    {
        anim.ResetTrigger("Shoot");
        Instantiate(EnemyBullet, ShootPoint.position, transform.rotation);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("laser"))
        {
            health--;
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            if (health <= 0)
            {
                FindObjectOfType<GameManager>().BossSpawned = false;
                FindObjectOfType<SpawnManager>().StartSpawnRoutine();
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            Instantiate(Explosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 0.4f);

            health--;
            
        }
    }
}
