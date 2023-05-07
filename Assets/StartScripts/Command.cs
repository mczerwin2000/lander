using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public abstract class Command
{
    protected IEntity _entity;

    public Command(IEntity entity)
    {
        this._entity = entity;
    }
    public abstract void Execute();
}
