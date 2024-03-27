import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)
import java.util.List;

/**
 * The card1 class holds the main values for the cards including the image, 
 * if it's a painting and it's paintingId
 * 
 * @author Rdsims@email.uscb.edu 
 * @version December 14th, 2023
 */
public class Card1 extends Actor
{
    /*
     * Fields
     */
    private GreenfootImage front;
    private GreenfootImage back;
    private GreenfootImage revert;
    private String imageName;
    private boolean isPainting; // if true, this is a painting. otherwise, if false, it is the title.
    private int uniqueId; // paint Id 1 corresponds to starry night
    private boolean flipped; // if true, the card is showing front

    /*
     * Constructors 
     */
    public Card1(String imageName, boolean isPainting, int uniqueId)
    {
        back = new GreenfootImage("CardBack.png");
        front = new GreenfootImage(imageName);
        this.imageName = imageName;
        this.isPainting = isPainting;
        this.uniqueId = uniqueId;
        act();
        setImage(back);
    } // end constructor Card
    /*
     * Methods
     */
    
    /**
     * Dr.Canada helped me by setting up the skeleton
     */
    public void act()
    {
        
            if (Greenfoot.mouseClicked(this))
            {
            if (getImage() == back) 
            {
                setImage(front);
                flipped = true;
                // update the value of `numberOfCardsFlipped` in MyWorld
                MyWorld referenceToMyWorld = (MyWorld)getWorld();
                
                referenceToMyWorld.incrementNumberOfCardsFlipped(+1);
                
            } // end if-else
            else
            {
                setImage(back);
                flipped = false;
                MyWorld referenceToMyWorld = (MyWorld)getWorld();
                
                
                referenceToMyWorld.incrementNumberOfCardsFlipped(-1);
                
            } // end if-else  
        } // end if
        
        
    } // end act method
    
    public void flipBack()
    {
        setImage(back);
        flipped = false;
    } // end method flipBack
    
    public String getimageName()
    {
        return imageName;
    } // end getimageName
    
    public boolean isPainting() 
    {
        return isPainting;
    } // end isPainting
    public int getPaintingId()
    {
        return uniqueId;
    } // end get PaintingId
   
    public boolean isFlipped()
    {
        return flipped; 
    } // end is flipped
    
    
    } // end class Card
