using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject cardPreFab;
    [SerializeField] private Transform cardSpawnPoint;
    [SerializeField] private Sprite[] cardImages;
    [SerializeField] List<Card> cards;
    private Card card1;
    private Card card2;
    private int score = 0;
    // Start is called before the first frame update
   
    Card CreateCard(Vector3 pos)
    {
        GameObject obj = Instantiate(cardPreFab, pos, cardPreFab.transform.rotation);
        Card card = obj.GetComponent<Card>();
        return card;
    }

    void Start()
    {
        //Card card = CreateCard(cardSpawnPoint.position);
        //int imageIndex = Random.Range(0, cardImages.Length);
        //card.SetSprite(cardImages[imageIndex]);
        cards = CreateCards();
        AssignImgesToCards();
    }
    List<Card> CreateCards()
    {
        List<Card> newCards = new List<Card>();
        int rows = 2;
        int cols = 4;
        float offsetX = 2f;
        float offsetY = 2.5f;


        for(int y = 0; y < rows; y++)
        {
            for(int x = 0; x < cols; x++)
            {

                Vector3 pos = cardSpawnPoint.position;
                pos.x += (offsetX * x);
                pos.y -= (offsetY * y);

                Card card = CreateCard(pos);
                newCards.Add(card);
            }
        }
        return newCards;
    }

    void AssignImgesToCards()
    {
        List<int> imageIndices = new List<int>();
        List<Card> shuffler = new List<Card>();
        for(int i=0; i < cardImages.Length; i++)
        {
            imageIndices.Add(i);
            imageIndices.Add(i);
        }

        //To DO write code to shuffle the list of imageindices
        for (int i = 0; i < cards.Count; i++)
        {
            int rand = Random.Range(0, cards.Count);
            int temp = imageIndices[i];
            imageIndices[i] = imageIndices[rand];
            imageIndices[rand] = temp;

        }


        for (int i = 0; i < cards.Count; i++)
        {
            
            int imageIndex = imageIndices[i];
            cards[i].SetSprite(cardImages[imageIndex]);
        }
        
    }

    IEnumerator EvaluatePair()
    {
        if(card1.GetSprite() == card2.GetSprite())
        {
            Debug.Log("match");
            score++;
        }
        else
        {
            float sawpTime = 1f;
            yield return new WaitForSeconds(1f);
            Debug.Log("not a match");
            iTween.MoveTo(card1.gameObject, card2.transform.position, sawpTime);
            iTween.MoveTo(card2.gameObject, card1.transform.position, sawpTime);
            yield return new WaitForSeconds(sawpTime);
            card1.SetFaceVisable(false);
            card2.SetFaceVisable(false);
        }
        card1 = null;
        card2 = null;
    }

    public void OnCardClicked(Card card)
    {
        if (!card.IsFaceViable())
        {
            if (card1 == null)
            {
                card1 = card;
                card1.SetFaceVisable(true);
            }
            else if (card2 == null)
            {
                card2 = card;
                card2.SetFaceVisable(true);

                StartCoroutine(EvaluatePair());
            }
        }
        Debug.Log(this + "CardClicked(): " + card.GetSprite());
    }

    private void Awake()
    {
        Messenger<Card>.AddListener(GameEvent.CARD_CLICKED, this.OnCardClicked); 
    }

    private void OnDestroy()
    {
        Messenger<Card>.RemoveListener(GameEvent.CARD_CLICKED, this.OnCardClicked);
    }


   public void OnResetButtonPressed()
    {
        Debug.Log("reset");
        Reset();
    }

    private void Reset()
    {
        score = 0;
        card1 = null;
        card2 = null;

        foreach(Card card in cards)
        {
            card.SetFaceVisable(false);
        }
        AssignImgesToCards();
    }
    
}
