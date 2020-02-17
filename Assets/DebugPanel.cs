using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour {

    public static void U(string key, object val) {
        DebugPanel instance = FindObjectOfType<DebugPanel>();
        if (instance)
            instance.SetVal(key, val);
    }

    private Text text;

    private Dictionary<string, string> values;

    // Start is called before the first frame update
    void Start() {
        text = transform.GetChild(0).GetChild(0).GetComponent<Text>();

        values = new Dictionary<string, string>();
    }

    // Update is called once per frame
    void Update() { }

    public void SetVal(string key, object value) {
        values[key] = value != null ? value.ToString() : "null";

        string updatedText = "";
        foreach (KeyValuePair<string, string> entry in values) {
            updatedText += entry.Key + ": " + entry.Value + "\n";
        }

        text.text = updatedText;
    }
}