import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
* This class is the text of the wardens voice once you start the game, in between levels and once you finish
* 
 * @author (your name) 
 * @version (a version number or a date)
*/
public class Text extends Actor
{
    private int duration = -1;
    private int time =500;
     
    //Constructor and other methods


    public void act()
    {
    if (time > 0)
        {
            time--;
            if (time == 0) getWorld().removeObject(this);
        }
    } // end act method
    public Text(String text, int size, int time)
    {
        setImage(new GreenfootImage(text, size, null, null)); //black on transparent, you can also use other colors/pass them to the constructor
        duration = time;
        
    } // end text method

    public Text(String text, int size)
    {
        this(text, size, -1);
    }
     
    
}


