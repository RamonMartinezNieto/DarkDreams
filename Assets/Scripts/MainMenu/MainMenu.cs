/**
 * Department: Game Developer
 * File: MainMenu.cs
 * Objective: Class to control the diferents caracteristics of the menu. 
 * Employee: Ramón Martínez Nieto
 */
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


/**
 * This class provide diferents methods to control the principal menu. 
 * 
 * If is the first game of the player, this class add a first configuratión with Playerprefs' methods
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class MainMenu : MonoBehaviour
{
    /**
     * Animator of the buttons
     */
    [Tooltip("Add the animtor of the buttons")]
    public Animator [] buttonsAnimator;

    /**
     * The panel representing the box for the player can enter the name
     */
    [Tooltip("Add panelBox with the name input")]
    public GameObject namePanel;

    /**
     * Panel animator of the namePanel
     */
    [Tooltip("add panel animator (of the name smallBox) ")]
    public Animator panelAnimator;
    
    /**
     * Shadow background the smallBox  panel
     */
    [Tooltip("Add background shadow")]
    public GameObject panelShadow;

    /**
     * InputField with the first user name
     */
    [Tooltip("Add InputField witth the user name")]
    public TMP_InputField firstUserName;

    /**
     * Label to display the name entered
     */
    [Tooltip("Add TMP_Text with the label to show to the player")]
    public TMP_Text labelName;
    
    /**
     * Label to show to the player the name entered and confirm
     */
    [Tooltip("add Label to show to the player the name entered and confirm")]
    public TMP_Text labelNameOptions;

    private void Start()
    {
        if (Pause.GameIsPaused) 
        {
            Time.timeScale = 1.0f;
        }

        //TODO:  TO TEST:PlayerPrefs.DeleteAll();

        ShowNameMenu();
        
        SoundManager.Instance.PlayMusic("menuMusic");
        SoundManager.Instance.LoopMusic(true);
    }

    /**
     * Method to check first time in the game, add basic config and show the panel 
     * to enter the user's name.
     */
    public void ShowNameMenu() 
    {
        //TODO: need to change FIRSTIMEOPENING ¿User name in PlayerPrefs?
        if (PlayerPrefs.GetInt("FIRSTIMEOPENING", 1) == 1)
        {
            //Set first parameters
            HandlerOptions ho = HandlerOptions.Instance;
            ho.MusicOn = true;
            ho.MusicVolumen = 0.3f;
            ho.MusicEffectOn = true;
            ho.MusicEffectVolumen = 0.2f;
            ho.LanguageInt = 2;
            ho.CrossHairString = "white";

            //Ask for name
            namePanel.SetActive(true);
            panelAnimator.SetTrigger("smallBox");
            panelShadow.SetActive(true);

            PlayerPrefs.SetInt("FIRSTIMEOPENING", 0);
        }
    }

    /**
     * Method to ask if the name entered is correct
     */
    public void AskCorrectName(TMP_InputField field) 
    {
        labelName.text = field.text; 
    }

    /**
     * Method to ask if the name entered is correct, in the configuration panel
     */
    public void AskCorrectNameOptions(TMP_InputField field)
    {
        labelNameOptions.text = field.text;
    }

    /**
     * Method to set the first name, the name will be save in the computer an database
     */
    public void SetFirstName() 
    {
        //PlayerPrefs.SetString("userName", firstUserName.text); 
        HandlerOptions.Instance.UserName = firstUserName.text;
        FirebaseConnection.Instance.WriteNewUser(firstUserName.text); 
    }

    /**
     * Method to start the game, load the next scene
     */
    public void StartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    /**
     * Method to exit to the desktop
     */
    public void ExitGame()
    {
        Application.Quit();
    }
    
    /**
     * Method to disable the buttons (when the user enter in the other panel)
     */
    public void DisableButtons() 
    {
        foreach (Animator a in buttonsAnimator)
        {
            //Note: Before of the launch triggert set state of the button. If not set bool it causes error
            a.SetBool("buttonActive", false);
            a.SetTrigger("start");
        }
    }

    /**
     * Method to active the buttons (when the user return to the menu)
     */
    public void ActiveButtons()
    {
        foreach (Animator a in buttonsAnimator)
        {
            a.SetBool("buttonActive", true);
            a.SetTrigger("start");
        }
    }
}