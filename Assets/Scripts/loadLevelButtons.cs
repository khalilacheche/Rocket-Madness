using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class loadLevelButtons : MonoBehaviour {
    Button button;
	// Use this for initialization
	void Start () {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            FindObjectOfType<main_menu>().loadLevel(gameObject.name);
        });
    }
	
	// Update is called once per frame
	void Update () {
       

	}
}
