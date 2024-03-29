import greenfoot.*;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

/**
 * My World holds the world of the first level, and relates to the class card1
 * 
 * @author Rdsims@email.uscb.edu 
 * @version December 14th, 2023
 */
public class MyWorld extends World
{
    private int time;
    private int numberOfCardsFlipped;
    private int Matches;
    private int Strikes;
    private List<Card1> cardAttributesList; // List to hold card attributes
    
    
    private boolean gameStarted;
    
    private GreenfootImage image1;
    private GreenfootImage image2;
    /*
     * Constructors
     */
    public MyWorld()
    {  
        super(960, 700, 1);
        
        image1 = new GreenfootImage("NFC-Background.jpeg");
        image2 = new GreenfootImage("WorldBackground.jpeg");
        gameStarted = false;
        
        time = 2000;
        numberOfCardsFlipped = 0;
        Matches = 0;
        Strikes = 3;
        
        cardAttributesList = new ArrayList<>(); // Initialize the list
        // Create and shuffle the card attributes
        createAndShuffleCardAttributes();

        // Create cards and add them to the world
        prepareCards();
    } // end MyWorld
    /*
     * Houston helped with everything involving the Nrc reader
     */

    public MyWorld( boolean nfcEnabled )
    {
        this();// this line causes the no-arg constructor above to be invoked
               // (helps to reduce redundant or duplicated constructor code)

        if (nfcEnabled) {
            setBackground(new GreenfootImage(image1));
        } else {
            setBackground(new GreenfootImage(image2));
        }
    } // end 1-arg MyWorld constructor
    /**
     * Add all the cards and their values to the list to be put into an array
     */
    /*
     * methods
     */
    /*
     * My Brother, Joseph helped me with this section by giving me the skeleton needed
     */
    private void createAndShuffleCardAttributes()
    {
        // Create card attributes and add them to the list
        cardAttributesList.add(new Card1("StarryNightFront2.png", true, 1));
        cardAttributesList.add(new Card1("StarryNightFront1.png", false, 1));
        cardAttributesList.add(new Card1("ThePotatoEatersFront1.png", true, 2));
        cardAttributesList.add(new Card1("ThePotatoEatersFront2.png", false, 2));
        cardAttributesList.add(new Card1("SkullWithCigaretteFront1.jpeg", true, 3));
        cardAttributesList.add(new Card1("SkullWithCigaretteFront2.png", false, 3));
        cardAttributesList.add(new Card1("WheatFieldOfCrowsFront1.jpeg", true, 4));
        cardAttributesList.add(new Card1("WheatFieldWithCrowsFront2.png", false, 4));
        cardAttributesList.add(new Card1("VaseWithFifteenSunflowersFront1.jpeg", true, 5));
        cardAttributesList.add(new Card1("VaseWithFifteenSunFlowersFront2.png", false, 5));

        // Shuffle the list of card attributes
        Collections.shuffle(cardAttributesList);
    } // end createAndShuffleCardAttributes
    /**
     * this places all the cards into a array and then will randomly place them in certain spots
     */
    /**
     * https://www.greenfoot.org/topics/7621/0, Inspriation came from this
     * https://www.geeksforgeeks.org/string-arrays-in-java/  Houston gave me this link
     */
    private void prepareCards()
    {
        int[] xPositions = {90, 270, 470, 670, 860};
        int[] yPositions = {200, 500};
        int xIndex = 0;
        int yIndex = 0;

        // Create cards using the shuffled card attributes
        for (Card1 attributes : cardAttributesList) {
            Card1 card = new Card1(attributes.getimageName(), attributes.isPainting(), attributes.getPaintingId());
            int xPos = xPositions[xIndex];
            int yPos = yPositions[yIndex];
            addObject(card, xPos, yPos);
            xIndex = (xIndex + 1) % xPositions.length;
            yIndex = (yIndex + 1) % yPositions.length;
            
            
        } // end for

    }// end method prepare
    /**
     * When a player matches two cards, they disapper
     */
    public void MatchedCards(Card1 firstFlipped, Card1 secondFlipped) {
        removeObject(firstFlipped);
        removeObject(secondFlipped);

    } // end removeMatchedCards
    /**
     * All methods involving time and strikes are used in chapter 5
     */
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
    /**
     * The time and strike methods were taken from 
     */
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
    
    private void Winning()
    {
        if(this.Matches == 5)
        {
            addObject(new Text("Well done Van Gogh, let's continue!!",40,300), 560, 632);
            Warden warden = new Warden();
            addObject(warden,100,600);
            Greenfoot.delay(100);
            Greenfoot.setWorld(new World2());
            
            
        } // end if
    } // end method Winning

    /**
     * The act method handles the logic of getting the values of the cards
     * by grabbing a list of the cards that are present on the screen 
     */
    /**
     * My brother helped me with this
     */

    private void checkForMatch()
    {
        List<Card1> cards = getObjects(Card1.class);
        Card1 firstFlipped = null;
        Card1 secondFlipped = null;
        /**
         * This is going to loop through all the cards to check for flipped ones
         * It is checking to see if they match the same PaintingId number
         * When a card is present on it's back, it holds a null value until clicked on
         * If player clicks two cards, that makes all their values present and compares them
         * If there paintingId's match, the cards matched disapper and makes a sound
         * if there paintingId's don't match, the cards revert back to original image and makes a incorrect sound
         */
        for (Card1 card : cards) {
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
