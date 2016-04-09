using UnityEngine;
using System.Collections;

public class LeekComponent : MonoBehaviour {

    public bool IsActive = false;
    public float Timer = 5;

    public Sprite[] greenLeekSprites;
    public Sprite[] redLeekSprites;

    enum LeekType {
        Green = 0,
        Red = 1
    }
    LeekType type;

    SpriteRenderer spriteRenderer;

    public GameObject Explosion;
    public GameObject LevelUpAnimation;

    [HideInInspector]
    public GameObject owner;

    int _leekLevel;

    public int LeekLevel {
        get {
            return _leekLevel;
        }
        set {
            if (value < greenLeekSprites.Length && value > _leekLevel) {
                _leekLevel = value;
                LevelUp();
            }
        }
    }

    void Start(){
        type = (LeekType) Random.Range(0,2);
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(type == LeekType.Red)
            spriteRenderer.sprite = redLeekSprites[0];
        else
            spriteRenderer.sprite = greenLeekSprites[0];
    }

    void LevelUp() {
        if(type == LeekType.Red)
            spriteRenderer.sprite = redLeekSprites[_leekLevel];
        else
            spriteRenderer.sprite = greenLeekSprites[_leekLevel];
        Instantiate(LevelUpAnimation, transform.position, Quaternion.identity);
    }

    void Update() {
        if (!IsActive)
            return;
        Timer -= Time.deltaTime;
        if (Timer < 0) {
            Explode();
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject != owner) {
            if (other.gameObject.tag == "Player" || other.gameObject.tag == "Ground") {
                Explode();
            }
        }
    }

    void Explode() {
        if(!IsActive)
            return;
        IsActive = false;
        Destroy(gameObject);
        GameObject explosionObject = Instantiate<GameObject>(Explosion);
        explosionObject.transform.position = transform.position;
        explosionObject.transform.rotation = Quaternion.identity;
        LeekExplosion leekExplosion = explosionObject.GetComponent<LeekExplosion>();
        leekExplosion.Level = LeekLevel;
        leekExplosion.GoBoom();
    }
}
