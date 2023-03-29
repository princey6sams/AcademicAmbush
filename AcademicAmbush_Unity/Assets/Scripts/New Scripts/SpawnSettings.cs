using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface SpawnSettings
{
    IEnumerator spawn(Quaternion spawnRotation);
}
