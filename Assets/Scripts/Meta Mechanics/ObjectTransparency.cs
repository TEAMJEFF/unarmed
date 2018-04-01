using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTransparency : MonoBehaviour {

    private Shader oldShader = null;
    private Color oldColor = Color.clear;
    private float transparency = 0.5f;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void MakeTransparent()
    {
        if (oldShader == null && rend != null)
        {
            oldShader = rend.material.shader;
            oldColor = rend.material.color;
            rend.material.shader = Shader.Find("Transparent/Diffuse");
        }
        transparency = 0.5f;
    }

    public void ResetTransparency()
    {
        rend = GetComponent<Renderer>();
        transparency = 1.0f;
        if (rend != null) {
            rend.material.shader = Shader.Find("Standard");
            oldShader = null;
            oldColor = Color.clear;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (transparency < 1.0f)
        {
            Color C = rend.material.color;
            C.a = transparency;
            rend.material.color = C;
        }
        else
        {
            Color C = rend.material.color;
            C.a = 1.0f;
            rend.material.color = C;
        }
	}
}
