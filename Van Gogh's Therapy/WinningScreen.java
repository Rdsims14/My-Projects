import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * The screen that holds the screen for victory
 * 
 * @author Rdsims@email.uscb.edu 
 * @version December 14th, 2023
 */
public class WinningScreen extends World
{

    /**
     * Constructor for objects of class WinningScreen.
     * 
     */
    public WinningScreen()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(940, 700, 1); 
        act();
    } // end WinningScreen
    public void act()
    {
        Greenfoot.stop();
    } // end method act
    
} // end WinningScreen Class
