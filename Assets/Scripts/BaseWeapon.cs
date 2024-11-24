using UnityEngine;

abstract public class BaseWeapon : MonoBehaviour
{

    abstract public Vector3 position
    {
        get;
    }
    abstract public Vector3 rotation
    {
        get;
    }

    abstract public void Fire();
}
