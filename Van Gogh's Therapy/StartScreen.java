import greenfoot.*;

public class StartScreen extends World
{
    private boolean nfcEnabled;
    private GreenfootSound backgroundMusic; // Declare a GreenfootSound object for background music
    
    /**
     * Constructor for objects of class StartScreen.
     */
    public StartScreen()
    {    
        super(940, 700, 1);
         // Initialize the background music

    }
    
    /**
     * Constructor for objects of class StartScreen.
     */
    public StartScreen(boolean nfcEnabled)
    {    
        super(940, 700, 1); 
        this.nfcEnabled = nfcEnabled;
         // Initialize the background music
        
    }
    
    public void act()
    {
        // Logic for handling user interaction
        if (!(nfcEnabled)) {
            if(Greenfoot.isKeyDown("enter")) {
                Greenfoot.setWorld(new DialogueScreen());
            }
        } else {
            if(Greenfoot.isKeyDown("enter")) {
                Greenfoot.setWorld(new DialogueScreen(nfcEnabled));
            }
        }
        
        
    } // end act method
} // end StartScreen Method