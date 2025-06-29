using UnityEngine;

abstract public class InteractiveObject : MonoBehaviour
{
    protected bool input;
    public void SetInput(bool value)
    {
        input = value;
    }
}