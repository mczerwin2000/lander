using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpCommand : Command
{
    private Rigidbody2D _rb2D;
    private Vector2 _velocity;
    private float _speed;
    private float _fuel;
    private float _fuelUsage;
    public UpCommand(IEntity entity, Rigidbody2D rb2D, float speed, float fuel, float fuelUsage) : base(entity)
    {
        _rb2D = rb2D;
        _speed = speed;
        _fuel = fuel;
        _fuelUsage = fuelUsage;
    }
    public override void Execute()
    {
        if (_fuel > 0)
        {
            _velocity = _rb2D.velocity;
            _velocity = _speed * _entity.transform.up * Time.deltaTime;
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
}
