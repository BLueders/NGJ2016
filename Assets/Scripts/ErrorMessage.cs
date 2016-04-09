using UnityEngine;
using System.Collections;
using InControl;

class ErrorMessage
{
    public float Timer;
    public string Message;

    public ErrorMessage(float time, string message){
        Timer = time;
        Message = message;
    }

}
