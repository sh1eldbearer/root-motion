using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCanvasRotation : MonoBehaviour
{
    #region Private Properties
#pragma warning disable CS0649
    [Tooltip(""),
        SerializeField] private Vector3 _targetRotation;
#pragma warning restore CS0649
    #endregion

    #region Public Properties

    #endregion
	
	// Awake is called before Start
	private void Awake()
    {
        _targetRotation = this.transform.rotation.eulerAngles;
    }

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(LockRotation());
    }

    // Update is called once per frame
    private IEnumerator LockRotation()
    {
        while (true)
        {
            this.transform.rotation = Quaternion.Euler(_targetRotation);
            yield return null;  
        }
    }
}
