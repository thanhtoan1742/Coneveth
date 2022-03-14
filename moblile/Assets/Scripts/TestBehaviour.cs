using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TestBehaviour : MonoBehaviour {
    protected Gauge gauge;

    protected Task task = null;
    void Awake() {
        gauge = GetComponent<Gauge>();
    }

    void Update() {
        if (task != null && !task.IsCompleted)
            return;
        gauge.amount = Random.Range(0f, 1f);
        task = Task.Delay(1000);
    }
}
