import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Holds the warden and makes text as if he was speaking to you
 * 
 * @author Rdsims@email.uscb.edu 
 * @version December 14th, 2023
 */
public class DialogueScreen extends World
{
    private boolean nfcEnabled;
    /**
     * Constructor for objects of class DialogeScreen.
     * 
     */
    public DialogueScreen()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(940, 700, 1); 
        nfcEnabled = false;
        prepare(); 
    }   // end DialogeScreen
    
    /**
     * Constructor for objects of class DialogeScreen.
     * 
     */
    public DialogueScreen(boolean nfcEnabled)
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(940, 700, 1); 
        this.nfcEnabled = nfcEnabled;
        prepare(); 
    } // end DialogeScreen
     
    public void prepare()
    {
        Warden warden = new Warden();
        Greenfoot.playSound("wha-wha1.mp3");
        addObject(new Text("Good evening Van Gogh, \nI hope you have been treated well on your first night\nLet's begin your session with something new,\nby reminiscing on the past, \nMatch your painting with the name. \n",35,300), 490, 400);
        
        addObject(warden,100,600);
    } // end prepare method
    
    public void act()
    {   
        if (!(nfcEnabled)) { 
            Greenfoot.delay(500);
            Greenfoot.setWorld(new MyWorld());
        } // end if
        else {
            Greenfoot.delay(500);
            Greenfoot.setWorld(new MyWorld(nfcEnabled));            
        } // end else
    } // end act method
} // end DialogeScreen class

