using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionDialog : MonoBehaviour
{
    public event Action OnYesEvent;
    public event Action OnNoEvent;

    //Make yes or no question popup appear
    public void Show()
    {
        gameObject.SetActive(true);
        OnYesEvent = null;
        OnNoEvent = null;
    }

    //Make yes or no question popup disappear
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    //When yes is clicked activate yes event
    public void OnYesButtonClick()
    {
        if (OnYesEvent != null)
        {
            OnYesEvent();
        }

        Hide();
    }

    //When no is clicked activate no event
    public void OnNoButtonClick()
    {
        if (OnNoEvent != null)
        {
            OnNoEvent();
        }

        Hide();
    }
}
