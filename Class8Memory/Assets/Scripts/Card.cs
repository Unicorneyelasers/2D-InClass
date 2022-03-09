using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    [SerializeField] private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
   
    public void SetSprite(Sprite image)
    {
        spriteRenderer.sprite = image;
    }

    public Sprite GetSprite()
    {
        return spriteRenderer.sprite;
    }
    public bool IsFaceViable()
    {
        return !cardBack.activeSelf;
    }
    public void SetFaceVisable(bool faceVisible)
    {
        if (faceVisible)
        {
            cardBack.SetActive(false);
        }
        else
        {
            cardBack.SetActive(true);
            //Messenger<Card>.Broadcast(GameEvent.CARD_CLICKED, this);
        }
    }
    private void OnMouseDown()
    {
        Messenger<Card>.Broadcast(GameEvent.CARD_CLICKED, this);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
