using UnityEngine;
using System.Collections;

public class SpriteAnimator : MonoBehaviour {

    float timeSinceLastSprite;
    bool animate = false;
    bool loop = false;
    float animationSpeed;

    int currentSprite = 0;

    Sprite[] currentSprites;
    SpriteRenderer currentRenderer;

    public void PlayOneShot(SpriteRenderer spriteRenderer, Sprite[] sprites, float speed){
        loop = false;
        animate = true;
        currentSprites = sprites;
        animationSpeed = speed;
        currentRenderer = spriteRenderer;
        currentSprite = 0;
        timeSinceLastSprite = 0;
    }

    public void PlayLoop(SpriteRenderer spriteRenderer, Sprite[] sprites, float speed){
        loop = true;
        animate = true;
        currentSprites = sprites;
        animationSpeed = speed;
        currentRenderer = spriteRenderer;
        currentSprite = 0;
        timeSinceLastSprite = 0;

    }

    void Update(){
        if(animate){
            timeSinceLastSprite += Time.deltaTime;
            if(timeSinceLastSprite > 1.0f/animationSpeed){
                currentSprite++;
                if(currentSprite == currentSprites.Length){
                    if(loop){
                        currentSprite = 0;
                    } else {
                        currentRenderer.sprite = null;
                        animate = false;
                        return;
                    }
                }
                currentRenderer.sprite = currentSprites[currentSprite];
                timeSinceLastSprite = 0;
            }
        }
    }

}
