using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{
    public enum ActionTrigger
    {
        NONE,
        BUTTON_PRESS,
        ENTER_AREA
    }

    public ActionTrigger trigger;

    [System.Serializable]
    public class InteractionAction : UnityEvent { }
    public InteractionAction action;

    public void Interact()
    {
        action?.Invoke();
    }
}
