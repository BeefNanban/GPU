using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//#region need compute_shader_sample

public class TestScript2 : MonoBehaviour {

    public ComputeShader shader;

	// Use this for initialization
	void Start () {
        int bufsize = 64;
        ComputeBuffer result = new ComputeBuffer(bufsize, sizeof(int));
        //CSMain
        shader.SetBuffer(0, "Result", result);
        shader.Dispatch(0, 1, 1, 1);
        //CSMain2
        shader.SetBuffer(1, "Result", result);
        shader.Dispatch(1, 1, 1, 1);

        //名前指定
        var func = shader.FindKernel("CSMain2");

        shader.SetBuffer(func, "Result", result);
        shader.Dispatch(func, 1, 1, 1);

        //int[] data = new int[bufsize];
        //result.GetData(data);
        //for (int i = 0; i < bufsize; i++) Debug.Log(data[i]);
        //result.Release();
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}