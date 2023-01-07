using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputControlToKeyCodeConverter
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public KeyCode Convert(InputControl input){
        switch(input.ToString()){
            case("Key:/Keyboard/1"):
                return KeyCode.Alpha1;
            case("Key:/Keyboard/2"):
                return KeyCode.Alpha2;
            case("Key:/Keyboard/3"):
                return KeyCode.Alpha3;
            case("Key:/Keyboard/4"):
                return KeyCode.Alpha4;
            case("Key:/Keyboard/q"):
                return KeyCode.Q;
            case("Key:/Keyboard/e"):
                return KeyCode.E;
            case("Key:/Keyboard/f"):
                return KeyCode.F;
            default:
                return KeyCode.None;
        }
    }
}
