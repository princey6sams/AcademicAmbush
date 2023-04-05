using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InteractiveObjects
{
    public void moveObj(params object[] args);
    // public bool destroyObj(Collider other);
    public void applyPlayerDamage(Collider other);
}