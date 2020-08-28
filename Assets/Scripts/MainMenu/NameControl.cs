using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NameControl : MonoBehaviour
{
    public TMP_InputField FirstUserName;
    public TMP_Text TextError; 

    public GameObject SmallBox; 
    public GameObject AreYouSureBox;
    public Animator AreYouSureAnimator;
    public GameObject PanelShadow; 

    private const int maxCharactersName = 16;
    private const int minCharactersName = 3;

    Color originalColor;

    private void Start()
    {
        originalColor = TextError.color; 
    }

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
