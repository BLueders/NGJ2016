using UnityEngine;
using System.Collections.Generic;
using InControl;
using UnityEngine.UI;

class ErrorHelper : Singleton<ErrorHelper>
{
    
    public Text textComponent;
    public Image imageComponent;
    List<ErrorMessage> ErrorMessages = new List<ErrorMessage>();

    public static void DisplayError(string message){
        foreach(ErrorMessage error in Instance.ErrorMessages){
            if(error.Message == message){
                return;
            }
        }
        Instance.ErrorMessages.Add(new ErrorMessage(1,message));
    }

    void Update() {
        List<ErrorMessage> deleteMessages = new List<ErrorMessage>();

        string theErrorText = "";

        if(ErrorMessages.Count == 0){
            imageComponent.gameObject.SetActive(false);
            textComponent.gameObject.SetActive(false);
        } else {
            imageComponent.gameObject.SetActive(true);
            textComponent.gameObject.SetActive(true);
        }

        foreach(ErrorMessage error in Instance.ErrorMessages){
            error.Timer -= Time.deltaTime;
            theErrorText += error.Message + "\n";
            if(error.Timer < 0) {
                deleteMessages.Add(error);
            }
        }

        textComponent.text = theErrorText;

        foreach(ErrorMessage error in deleteMessages){
            ErrorMessages.Remove(error);
        }
    }
}
