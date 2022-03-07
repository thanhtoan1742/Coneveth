using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour {
    public string unit = "";
    protected TMP_Text valueText;
    protected string _value;
    public string value {
        get { return _value; }
        set {
            _value = value;
            UpdateUI();
        }
    }


    void Awake() {
        valueText = gameObject.transform.Find("Value").gameObject.GetComponent<TMP_Text>();
        UpdateUI();
    }

    void UpdateUI() {
        valueText.text = value + unit;
    }
}
