using UnityEngine;
using System.Collections;

public class LeekComponent : MonoBehaviour {

    public bool IsActive = false;
    public float Timer = 5;

    public Sprite[] leekSprites;

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
            if (value < leekSprites.Length && value > _leekLevel) {
                LevelUp();
                _leekLevel = value;
            }
        }
    }

    void LevelUp() {
        spriteRenderer.sprite = leekSprites[_leekLevel];
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
        GameObject explosionObject = Instantiate<GameObject>(Explosion);
        explosionObject.transform.position = transform.position;
        explosionObject.transform.rotation = transform.rotation;
        LeekExplosion leekExplosion = explosionObject.GetComponent<LeekExplosion>();
        leekExplosion.Level = LeekLevel;
        leekExplosion.GoBoom();
        Destroy(gameObject);
    }
}
