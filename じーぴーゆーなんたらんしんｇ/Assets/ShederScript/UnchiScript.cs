using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnchiScript : MonoBehaviour {

    public ComputeShader shader;
	
    // Use this for initialization
	void Start () {
        ComputeBuffer buffer = new ComputeBuffer(4*2, sizeof(int));

        shader.SetBuffer(0, "buffer1", buffer);
        shader.Dispatch(0, 2, 1, 1);

        int[] data = new int[4 * 2];
        buffer.GetData(data);

        for (int i  = 0; i < 4 * 2; i++){
            Debug.Log(data[i]);
        }

        buffer.Release();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
