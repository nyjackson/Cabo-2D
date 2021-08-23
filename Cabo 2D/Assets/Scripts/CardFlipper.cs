using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardFlipper : MonoBehaviour
{
    public Sprite CardFront;
    public Sprite CardBack;

    //public SpriteRenderer SpriteRenderer;
    // Start is called before the first frame update
    /*
    public void changeSprite(Sprite sprite) {
        CardFront = sprite;
        SpriteRenderer.sprite = CardFront;
    }
    */
    public void Flip() {
        Sprite currSprite = GetComponent<Image>().sprite;

        if (currSprite == CardFront)
        {
            gameObject.GetComponent<Image>().sprite = CardBack;
        }

        else {
            gameObject.GetComponent<Image>().sprite = CardFront;
        }
    }

}
