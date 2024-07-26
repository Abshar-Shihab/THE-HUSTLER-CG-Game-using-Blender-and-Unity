using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Scope : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ScopeOverlay;

    private bool isScoped= false;

    public CinemachineVirtualCamera alternateCamera;
    public float scopeFOV = 15f;

    float minFOV = 1f;
    float maxFOV = 20f;
    float zoomSensitivity = 10f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isScoped = true;
        }
        if(Input.GetMouseButtonUp(1))
        {
            isScoped= false;
        }
        if (isScoped)
        {
            StartCoroutine(OnScoped());
        }
        else
        {
            OnUnScoped();
        }
    }

    void OnUnScoped()
    {
        ScopeOverlay.SetActive(false);
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(0.15f);

        ScopeOverlay.SetActive(true);

        //zoom in
        float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        alternateCamera.m_Lens.FieldOfView = Mathf.Clamp(alternateCamera.m_Lens.FieldOfView - mouseWheel * zoomSensitivity, minFOV, maxFOV);
    }
}
