using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class BarChart : MonoBehaviour {
    public GameObject barPrefab;
    public int capacity = 10;
    protected GameObject barsGameObject;

    void Awake() {
        barsGameObject = gameObject.transform.Find("Bars").gameObject;
    }

    GameObject CreateBar(float amount) {
        GameObject barGameObject = Instantiate(barPrefab);
        Bar bar = barGameObject.GetComponent<Bar>();
        bar.amount = amount;
        bar.Awake();
        return barGameObject;
    }

    void Add(float amount) {
        if (barsGameObject.transform.childCount == capacity) {
            Transform child = barsGameObject.transform.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
        GameObject g = CreateBar(amount);
        g.transform.SetParent(barsGameObject.transform, false);
        g.transform.SetAsLastSibling();
        UpdateUI();
    }

    void UpdateUI() {
        int cnt = 0;
        foreach (Transform child in barsGameObject.transform) {
            var rectTransform = child.gameObject.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(1.0f/capacity*cnt, 0);
            rectTransform.anchorMax = new Vector2(1.0f/capacity*(cnt + 1), 1);
            cnt += 1;
        }
    }


    protected Task task = null;
    void Update() {
        if (task != null && !task.IsCompleted)
            return;
        Add(Random.Range(0f, 1f));
        task = Task.Delay(100);
    }
}
