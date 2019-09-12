using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Persistence;

public class RaycastingMouse : MonoBehaviour
{
    [SerializeField] private Vector3 mouseWorldPos;
    public Vector3 MouseWorldPos
    {
        get { return mouseWorldPos; }
        private set { mouseWorldPos = value; }
    }

    [SerializeField] private Ray mouseRay;
    public Ray MouseRay
    {
        get { return mouseRay; }
        private set { mouseRay = value; }
    }
    void Awake()
    {
        // Singleton pattern
        if (GameManager.theGM.mouse == null)
        {
            GameManager.theGM.mouse = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartCoroutines()
    {
        StartCoroutine(GetMouseWorldPosition());
        StartCoroutine(GetMouseRay());
    }

    IEnumerator GetMouseWorldPosition()
    {
        while (GameManager.theGM.gameIsRunning)
        {
            MouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            yield return null;
        }
    }

    IEnumerator GetMouseRay()
    {
        while (GameManager.theGM.gameIsRunning)
        {
            MouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            yield return null;
        }
    }
}
