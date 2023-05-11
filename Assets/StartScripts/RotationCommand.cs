using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCommand : Command
{
    private Rigidbody2D _rb2D;
    private float _rotationSpeed;
    private int _leftRight;
    public RotationCommand(IEntity entity, Rigidbody2D rb2D, float rotationSpeed,int LeftRight) : base(entity)
    {
        _rb2D = rb2D;
        _rotationSpeed = rotationSpeed;
        _leftRight = LeftRight;
    }
    public override void Execute()
    {

        _rb2D.rotation += _rotationSpeed * Time.deltaTime * _leftRight;
    }
}
