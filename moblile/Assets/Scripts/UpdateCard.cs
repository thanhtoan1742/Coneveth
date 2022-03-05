using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateCard : MonoBehaviour {
    protected GameObject valueText;
    protected string value;
    public string unit = "";

    protected virtual void Start() {
        Debug.Log(gameObject.name);
        valueText = gameObject.transform.GetChild(2).gameObject;
        value = "";
    }

    protected virtual void Update() {
        valueText.GetComponent<TMP_Text>().text = value + unit;
    }
}
