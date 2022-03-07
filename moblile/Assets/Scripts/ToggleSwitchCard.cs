using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitchCard : MonoBehaviour {
    public Toggle toggle;
    public bool isOn {
        get { return toggle.isOn; }
        set { toggle.isOn = value; }
    }


    protected List<Action<bool>> listeners = new List<Action<bool>>();

    public void NotifyListeners(bool value) {
        foreach (var listener in listeners) {
            listener(value);
        }
    }

    public void AddListener(Action<bool> listener) {
        listeners.Add(listener);
    }
}
