using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    private Command _UpCommand;
    private Command _RotationCommand;

    public void Rotation(KeyCode key, RotationCommand rotation) {
        _RotationCommand = rotation;
        if(Input.GetKey(key)) _RotationCommand.Execute();
    }

    public void Up(KeyCode key, UpCommand up)
    {
        _UpCommand = up;
        if (Input.GetKey(key)) _UpCommand.Execute();
    }
}
