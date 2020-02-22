using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RegisterSelectorBox : MonoBehaviour
{
    void Awake()
    {
        this.GetComponentInParent<SelectorBehavior>().RegisterSelectorBox(this.gameObject);
        this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
