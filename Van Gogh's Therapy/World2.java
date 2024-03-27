import greenfoot.*;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;


/**
 * My World2 holds the world of the second level, and relates to the class card2
 * 
 * @author Rdsims@email.uscb.edu 
 * @version December 14th, 2023
 */
public class World2 extends World
{
    private int time;
    private int numberOfCardsFlipped;
    private int Matches;
    private int Strikes;
    private List<Card2> cardAttributesList; // List to hold card attributes
    /*
     * Constructors
     */
    public World2()
    {  
        super(960, 700, 1);
        
        time = 1500;
        numberOfCardsFlipped = 0;
        Matches = 0;
        Strikes = 2;
        cardAttributesList = new ArrayList<>(); // Initialize the list

        // Create and shuffle the card attributes
        createAndShuffleCardAttributes();

        // Create cards and add them to the world
        prepareCards();
    } // end World2
     
    /**
     * Creates a list of the cards for this worlds attributes
     */
    private void createAndShuffleCardAttributes()
    {
        // Create card attributes and add them to the list
        cardAttributesList.add(new Card2("BedroomInArlesFront1.jpeg", true, 1));
        cardAttributesList.add(new Card2("BedroomInArlesFront2.png", false, 1));
        cardAttributesList.add(new Card2("PrisonersFront2.jpeg", true, 2));
        cardAttributesList.add(new Card2("PrisonerFront1.jpeg", false, 2));
        cardAttributesList.add(new Card2("AlmondBlossomsFront1.jpeg", true, 3));
        cardAttributesList.add(new Card2("AlmondBlossomsFront2.png", false, 3));
        cardAttributesList.add(new Card2("WheatfieldWithAReaperFront1.jpeg", true, 4));
        cardAttributesList.add(new Card2("WheatFieldWithAReaperFront2.png", false, 4));
        cardAttributesList.add(new Card2("TheChurchAtAuversFront1.jpeg", true, 5));
        cardAttributesList.add(new Card2("TheChurchOfAuversFront2.png", false, 5));

        // Shuffle the list of card attributes
        Collections.shuffle(cardAttributesList);
    } // end method createAndShuffleCardAttributes
    
    /**
     * Places the cards into a array by taking a list of each of the cards attributes
     */
    private void prepareCards()
    {
        int[] xPositions = {90, 270, 470, 670, 860};
        int[] yPositions = {200, 500};
        int xIndex = 0;
        int yIndex = 0;

        // Create cards using the shuffled card attributes
        for (Card2 attributes : cardAttributesList) {
            Card2 card = new Card2(attributes.getimageName(), attributes.isPainting(), attributes.getPaintingId());
            int xPos = xPositions[xIndex];
            int yPos = yPositions[yIndex];
            addObject(card, xPos, yPos);
            xIndex = (xIndex + 1) % xPositions.length;
            yIndex = (yIndex + 1) % yPositions.length;
            
            
        } // end for loop

    }// end method prepare
    /*
     * methods
     */
    /**
     * if the player matches two cards with the same painting Id, they will disapper.
     */
    public void MatchedCards(Card2 firstFlipped, Card2 secondFlipped) {
        removeObject(firstFlipped);
        removeObject(secondFlipped);

    } // end removeMatchedCards

    /**
     * All methods below are responsible for the checking and updating the time and Strikes for this level
     */
    
    private void showTime()
    {
        showText("Time left:" + this.time , 100, 40);
    } // end showTime
    
    /**
     * shows the player their strikes left
     */
    private void showStrikes()
    {
        showText("Strikes left:" + this.Strikes , 500, 40);
    } // end method showStrikes

    private void timeCount(){
        time -= 1;
        showTime();
        if (this.time == 0){
            Greenfoot.setWorld(new FailScreen());
            Greenfoot.stop();
        } // end if
    } // end method timeCount

    /**
    /* The methods below are the consequences for failing the game in either way.
     */
    private void failing2()
    {
        if (this.Strikes == 0)
        {
            Greenfoot.setWorld(new FailScreen());
        } // end if
    } // end failing2
    
    /**
     * Once the player reaches 5 matches, they will be transported to the next World
     */
    private void Winning()
    {
        if(this.Matches == 5)
        {
            addObject(new Text("Great Work Van Gogh\n Now go get some shut eye!!!!!",40,300), 560, 632);
            Warden warden = new Warden();
            addObject(warden,100,600);
            Greenfoot.delay(200);
            Greenfoot.setWorld(new WinningScreen());
            } // end if
            
        } // end method Winning

    /**
     * The act method handles the logic of getting the values of the cards
     * by grabbing a list of the cards that are present on the screen 
     */

    private void checkForMatch()
    {
        List<Card2> cards = getObjects(Card2.class);
        Card2 firstFlipped = null;
        Card2 secondFlipped = null;
        /**
         * This is going to loop through all the cards to check for flipped ones
         * It is checking to see if they match the same PaintingId number
         * When a card is present on it's back, it holds a null value until clicked on
         * If player clicks two cards, that makes all their values present and compares them
         * If there paintingId's match, the cards matched disapper and makes a sound
         * if there paintingId's don't match, the cards revert back to original image and makes a incorrect sound
         */
        for (Card2 card : cards) {
            if (card.isFlipped()) {
                if (firstFlipped == null){
                    firstFlipped = card;
                    this.numberOfCardsFlipped += 1;
                } // end inner if
                else 
                {
                    secondFlipped = card; 
                    this.numberOfCardsFlipped += 1;
                } // end else
            } // end outer if
        } // end for loop
        if (firstFlipped != null && secondFlipped != null) {
            if(firstFlipped.getPaintingId() == secondFlipped.getPaintingId()) {
                Greenfoot.playSound("correct-6033.mp3");// https://pixabay.com/sound-effects/correct-6033/
                Greenfoot.delay(15);
                MatchedCards(firstFlipped,secondFlipped);
                Matches = Matches + 1;
            } // end inner if
            else
            {
                Greenfoot.playSound("buzzer-or-wrong-answer-20582.mp3");// https://pixabay.com/sound-effects/buzzer-or-wrong-answer-20582/
                Greenfoot.delay(15);
                firstFlipped.flipBack();
                secondFlipped.flipBack();
                Strikes = Strikes - 1;

            } // end else
        }  // end if
    } // end method CheckMatches
    public void act()
    {
        checkForMatch();
        timeCount();
        showStrikes();
        failing2();
        Winning();
    } // end act method
    /**
     * This counts the amount of cards to make sure the player can
     * only click two cards to match. Also makes a strategy to the game
     * by allowing the player to flip a card and then flip it back.
     * 
     */
    public void incrementNumberOfCardsFlipped(int increment)
    {
        this.numberOfCardsFlipped = numberOfCardsFlipped + increment;
    } // end method setNumberOfCardsFlipped
    public void decrementStrikes(int decrement)
    {
        this.Strikes = Strikes + decrement;
    } // end method setStrikes
    public void incrementMatches(int increment)
    {
        this.Matches = Matches + increment;
    } // end method incrementMatches
    } // end class MyWorld