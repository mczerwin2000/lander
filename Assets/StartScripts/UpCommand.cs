using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpCommand : Command
{

    private Rigidbody2D _rb2D;
    private Transform _tr;
    private Vector2 _velocity;
    private float _speed;
    private float _fuel;
    private float _fuelUsage;
    public UpCommand(IEntity entity, Rigidbody2D rb2D, Transform tr, float speed, float fuel, float fuelUsage) : base(entity)
    {
        _rb2D = rb2D;
        _speed = speed;
        _tr = tr;
        _fuel = fuel;
        _fuelUsage = fuelUsage;
    }
    public override void Execute()
    {
        if (_fuel > 0)
        {
            _velocity = _rb2D.velocity;
            _velocity = _speed * _tr.up * Time.deltaTime;
            _rb2D.velocity += _velocity;
            _fuel -= _fuelUsage * Time.deltaTime;
        }
    }

    public float GetFuel() {
        return _fuel;
    }

    public void setFuel(float newFule) { 
        _fuel= newFule;
    }
    // Start is called before the first frame update

}
