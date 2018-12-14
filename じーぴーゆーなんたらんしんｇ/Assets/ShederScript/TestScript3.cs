using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript3 : MonoBehaviour {

    public ComputeShader shader;

	// Use this for initialization
	void Start () {
        int bufSize = 64;
        ComputeBuffer result = new ComputeBuffer(bufSize, sizeof(int));
        var func = shader.FindKernel("CSMain2");

        shader.SetBuffer(func, "Result",result);
        shader.SetInt("hoge", 100);
        shader.Dispatch(func, 1, 1, 1);

        int[] data = new int[bufSize];
        result.GetData(data);

        for (int i = 0; i < bufSize; i++) Debug.Log(data[i]);

        result.Release();


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
