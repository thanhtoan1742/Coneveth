using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Background : MonoBehaviour {
    protected TMP_Text text;

    void Awake() {
        text = transform.Find("Time").GetComponent<TMP_Text>();
    }

    void Update() {
        text.text = DateTime.Now.ToString("T");
    }
}
