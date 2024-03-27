import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * The screen that holds the screen for the failure
 * 
 * @author Rdsims@email.uscb.edu 
 * @version December 14th, 2023
 */
public class FailScreen extends World
{

    /**
     * Constructor for objects of class FailScreen.
     * 
     */
    public FailScreen()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(960, 500, 1);
        Greenfoot.playSound("downer_noise.mp3");
        addObject(new Text("Press enter to try again", 35, 300),490, 400);
        
    }
    public void act()
    {
       if(Greenfoot.isKeyDown("enter")) {
                Greenfoot.setWorld(new MyWorld());
            } 
    }
}
