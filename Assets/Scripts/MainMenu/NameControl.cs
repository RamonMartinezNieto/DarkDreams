/**
 * Department: Game Developer
 * File: NameControl.cs
 * Objective: Have a name control, to parse the caracteres about the name and show next dialogs
 * Employee: Ramón Martínez Nieto
 */
using TMPro;
using UnityEngine;

/**
 * This class provide methods to control what name introduce the player. 
 * Control how much characteres are, and launch a error if there are more than 16 characteres. 
 * Launch the next dialogs to. 
 * 
 * @author Ramón Martínez Nieto
 * @version 1.0.0
 */
public class NameControl : MonoBehaviour
{
    /**
     * First inputField that the user see. 
     */
    [Tooltip("Add FirstInputField")]
    public TMP_InputField FirstUserName;
    /**
     * Text with the error if the user add a invalid name 
     */
    [Tooltip("Add TMP_Text with the error to show")]
    public TMP_Text TextError;

    /**
     * GameObject that represent a SamallBox (with the diferentes inputs) 
     */
    [Tooltip("Add TMP_Text with the error to show")]
    public GameObject SmallBox;
    
    /**
     * Confirmation Box (with the buttons to confirmate the name) 
     */
    [Tooltip("Add confirmation Box")]
    public GameObject AreYouSureBox;

    /**
     * Animator for the box
     */
    [Tooltip("Add animator for the box")]
    public Animator AreYouSureAnimator;
    
    /**
     * PanelShadow represents a background shadow to fix the view in the box
     */
    [Tooltip("Add panel shadow")]
    public GameObject PanelShadow; 

    private const int maxCharactersName = 16;
    private const int minCharactersName = 3;

    private Color originalColor;

    private void Start()
    {
        originalColor = TextError.color; 
    }

    /**
    * Method to save the name of the user
    */
    public void ExecuteSave() 
    {
        if (LengthControl())
        {
            NextStep();
        }
        else {
            TextError.text = "The name is to long! 3-16 Characters please.";
            TextError.color = Color.red;
        }
    }

    private bool LengthControl() {        
        return !(FirstUserName.text.Length > maxCharactersName || FirstUserName.text.Length < minCharactersName); 
    }

    private void NextStep() 
    {
        if(SmallBox != null) SmallBox.SetActive(false);
        
        PanelShadow.SetActive(true);
        AreYouSureBox.SetActive(true);
        AreYouSureAnimator.SetTrigger("areYouSure");
        TextError.text = "16 characters maxium.";
        TextError.color = originalColor; 
    }
}