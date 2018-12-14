using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region need compute_shader_sample

struct Particle
{
    public int id;
    public bool active;
    public Vector3 position;
    public Vector3 velocity;
    public Vector3 rotation;
    public Vector3 angVelocity;
    public float scale;
    public float time;
    public float lifeTime;
}

public class TestScript : MonoBehaviour {

    public ComputeShader compute_shader;
    private ComputeBuffer buffer;
    private int shader_kanel;
    private int[] set_data;
    int index;
    int flam = 0;
    Mesh mesh;

    Vector3 velocity = new Vector3(2f, 5f, 2f);
    Vector3 angVelocity = new Vector3(45f, 45f, 45f);
    Vector3 range = Vector3.one;

    #endregion

    // Use this for initialization
    void Start () {
        if (null == compute_shader)
        {
            return;
        }

        shader_kanel = compute_shader.FindKernel("CSMain");
        shader_kanel = compute_shader.FindKernel("Emit");
        buffer = new ComputeBuffer(5, sizeof(int));
        set_data = new int[4];
        for(int _i = 0; _i < 4; _i++)
        {
            set_data[_i] = _i;

            Debug.Log("numbers:" + set_data[_i]);
        }
        buffer.SetData(set_data);
        compute_shader.SetBuffer(shader_kanel, "int_result", buffer);
        compute_shader.Dispatch(shader_kanel, 1, 1, 1);
        buffer.GetData(set_data);
        for(int _i = 0; _i < 4; _i++)
        {
            Debug.Log("Aftertreatment number" + set_data[_i]);
        }

        index = 0;

    }
	// Update is called once per frame
	void Update () {
        ++flam;

        compute_shader.SetVector("_Velocity", velocity);
        compute_shader.SetVector("_AngVelocity", angVelocity * Mathf.Deg2Rad);

        if(flam >= 60)
        {
            transform.localScale = new Vector3(set_data[index], 1, 1);
            index = (index + 1 >= set_data.Length) ? 0 : ++index;
            flam = 0;
        }
    }
}
