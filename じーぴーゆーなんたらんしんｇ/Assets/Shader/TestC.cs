using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestC : MonoBehaviour {

    public ComputeShader shader;

	// Use this for initialization
	void Start () {
        ComputeBuffer result = new ComputeBuffer(4, sizeof(int));
        shader.SetBuffer(0, "Result", result);

        int kernel = shader.FindKernel("CSMain");

        shader.Dispatch(kernel, 1, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
