using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float BulletSpeed;
    [SerializeField]
    private GameObject _Enemy_Explosion;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _audioClip;



    void Start()
    {
        
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.down * BulletSpeed) * Time.deltaTime);

        if (transform.position.y <= -5.45f)
        {
           
            Destroy(this.gameObject);         
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player");

            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            Instantiate(_Enemy_Explosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 0.4f);

            Destroy(this.gameObject);
            _uiManager.UpdateScore();

        }
    }
}
