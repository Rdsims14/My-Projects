import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class Warden here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */

public class Warden extends Actor
{
    private int duration = -1;
    private int time =500;
    
    
    public Warden()
    {    
        GreenfootImage image = getImage();  
        image.scale(400, 400);
        setImage(image);
        duration = time;
        
    } // end warden
    
} // end warden class
