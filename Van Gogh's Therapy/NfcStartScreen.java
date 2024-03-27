import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)
import javax.smartcardio.*; // required for Smart Card input/output ("I/O")
import java.util.List;

/**
 * This is the title screen for the "Van Gogh's Therapy" game. It allows the player to
 * launch the game by plugging in an ACR122U smart card reader and placing
 * a compatible NFC tag or card in contact with the reader. If the NFC tag is 
 * recognized, the instance variable `nfcEnabled` will be set to `true` in the
 * `GameManager` object, which will be passed to an overloaded `CrabWorld` 
 * constructor so that the data are persistent within the game itself.
 * 
 * @author rdsims@email.uscb.edu
 * @version december 14th 2023
 */
public class NfcStartScreen extends World
{
    /* FIELDS */
    private boolean nfcEnabled;
    private boolean spaceBarPressed;
    private boolean started;
    private boolean terminalNotFoundMessageDisplayed;
    /* CONSTRUCTORS */
    
    /**
     * Constructor for objects of class NfcStartScreen.
     */
    public NfcStartScreen()
    {    
        super(940, 700, 1);
        nfcEnabled = false;
        spaceBarPressed = false;
        started = false; // used in conjunction with `start` method
        terminalNotFoundMessageDisplayed = false;
        // it is OK to call methods within the constructor as long as all 
        // methods belong to THIS object only (or to the `Greenfoot` class)
        showText("Press the Run button, then plug in your NFC tag reader\n" 
                  + "and try reading an NFC tag.\n\n" // note extra `\n` for blank line
                  + "Or, if not using a smart card reader: press Run, and then\n" 
                  + "press the space bar to start the game normally.\n\n"
                  + "To fully reset the scenario, unplug the card reader,\n"
                  + "close Greenfoot entirely, then restart the scenario.\n\n", getWidth() / 2, getHeight() / 2 );
    } // end no-arg NfcStartScreen constructor
    
    /**
     * Overloaded Constructor for objects of class NfcStartScreen.
     */
    public NfcStartScreen(boolean nfcEnabled)
    {    
        super(560, 560, 1);
        this.nfcEnabled = nfcEnabled;
        spaceBarPressed = false;
        started = false; // used in conjunction with `start` method
        terminalNotFoundMessageDisplayed = false;
        // it is OK to call methods within the constructor as long as all 
        // methods belong to THIS object only (or to the `Greenfoot` class)
        showText("Press the Run button, then plug in your NFC tag reader\n" 
                  + "and try reading an NFC tag.\n\n" // note extra `\n` for blank line
                  + "Or, if not using a smart card reader: press Run, and then\n" 
                  + "press the space bar to start the game normally.\n\n"
                  + "To fully reset the scenario, unplug the card reader,\n"
                  + "close Greenfoot entirely, then restart the scenario.\n\n"
                  + "Also: before uploading your scenario to the Greenfoot\n"
                  + "website, set `CrabWorld` as the World subclass to be\n"
                  + "instantiated when the scenario is loaded. Otherwise,\n"
                  + "the Greenfoot website won't be able to \"transpile\"\n" // note \" used to print literal double quote mark
                  + "your scenario to JavaScript, and the game won't run.", getWidth() / 2, getHeight() / 2 );
    } // end no-arg NfcStartScreen constructor
    /* METHODS */
    /**
     * This `act` method is designed to require the game
     * to be running before the smart card reader can be checked
     */
    public void act() 
    {
        // call the `start` method only once
        if ( !started ) 
        {
            start();
        } // end if
        
        // go through process of checking for user input
        // either via smart card (NFC) or by keyboard input
        if ( !nfcEnabled && !spaceBarPressed ) 
        {
            checkForNfcConnection();
        }
        else if ( nfcEnabled || spaceBarPressed ) 
        {
            Greenfoot.setWorld( new StartScreen( nfcEnabled ) ); // this enables GameManager and its data
                                                                 // to persist from one world to the next
                                                                  // (by analogy, this would be like a relay 
                                                                  // racer handing off a baton to the next racer)
        } // end if/else
    } // end method act
    
    /**
     * Rather than call methods from the constructor, use this `start` method
     * to invoke methods that need to be called only ONCE after an object
     * has been initialized. This programming pattern is similar to that
     * used in game engines (like Unity) can ensure that all objects are 
     * initialized BEFORE invoking any of their methods. (Because objects are
     * not initialized all at once, this helps prevent calling methods to 
     * objects that may not exist yet!)
     */
    private void start()
    {     
        // after calling all "onetime" methods, flip the value
        // of `started` to `true` so that the start method won't be 
        // called again
        started = true;
    } // end method start
    
    /**
     * This method checks for a connected NFC smart card reader
     * and waits for an NFC tag or card to be placed in contact
     * with the reader.
     */
    public void checkForNfcConnection() {
        try {
            TerminalFactory factory = TerminalFactory.getDefault();
            List<CardTerminal> terminals = factory.terminals().list(); // requires `import java.util.List` above

            if ( !terminalNotFoundMessageDisplayed && terminals.isEmpty()) {
                System.out.println("No terminals found.");
                terminalNotFoundMessageDisplayed = true;
            } 
            else if ( !terminals.isEmpty() )
            {
                
                CardTerminal terminal = terminals.get(0); // if multiple terminals found, get the 1st terminal (i.e., at index 0)
                System.out.println("Using terminal: " + terminal.getName());
    
                terminal.waitForCardPresent(0); // Wait for card insertion
    
                Card card = terminal.connect("*"); // Connect to the card
    
                CardChannel channel = card.getBasicChannel(); // gets logical connection channel to card
                
                // APDU = Application Protocol Data Unit
                // This is the main unit of communication between the smart card 
                // and the smart card reader
                CommandAPDU command = new CommandAPDU(new byte[]{(byte) 0xFF, (byte) 0xCA, 0x00, 0x00, 0x00}); // Select APDU command
                ResponseAPDU response = channel.transmit(command);
    
                if (response.getSW1() == 0x90 && response.getSW2() == 0x00) { // SW = "status word"
                    byte[] data = response.getData();
                    System.out.print("Card UID: ");
                    for (byte b : data) {
                        System.out.printf("%02X ", b);
                    } // end for
                    
                    System.out.println(); // advances to next line of debug output
                
                    nfcEnabled = true; // when this is set to `true`, the next call to the `act` method
                                       // will no longer call this checkForNfcConnection() method
                                       
                    //myGameManager.setNfcEnabled( nfcEnabled ); // this ensures that the value of `nfcEnabled` is also
                                                               // set in our GameManager object referenced by `myGameManager`
                                                               // (we could also pass the argument `true` directly)
                } else {
                    System.out.println("Error reading card UID.");
                } // end if/else
    
                card.disconnect(false); // Disconnect from the card
    
                terminal.waitForCardAbsent(0); // Wait for card removal
                } // end if/else
        } 
        catch (CardException e) 
        {        
            // The thrown CardException will have the error message "list() failed"
            // if no card reader is present. Otherwise, if there is a different reason
            // for the CardException being thrown, then we will display the error message
            // and inform the user to try reading the card again. 
            if ( !e.getMessage().contains("list() failed") ) 
            {
                System.err.println("The card was not read. Please try again. Error = " + e.getMessage());
            } // end if

            // Here's an example of how we can allow the user to provide a form
            // of input that can bypass the need for a smart card device for
            // starting the game. In this case, if no smart card is present,
            // then the user can start the game by pressing the space bar
            if ( Greenfoot.isKeyDown("space") ) 
            {
                spaceBarPressed = true;
            } // end if

        } // end try statement
        
    } // end method CheckForNfcConnection
} // end class NfcStartScreen