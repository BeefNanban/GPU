using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Object
{
    public Vector3 pos;
    public Vector3 scale;
    public Quaternion rot;
    public Matrix4x4 Matrix
    {
        get
        {
            return Matrix4x4.TRS(pos, rot, scale);
        }
    }
    public Object(Vector3 pos,Vector3 scale,Quaternion rot)
    {
        this.pos = pos;
        this.scale = scale;
        this.rot = rot;
    }
}

public class GPUI : MonoBehaviour {
    public int instances;
    public Vector3 maxPos;
    public Mesh objMesh;
    public Material objMat;
    private List<List<Object>> batches = new List<List<Object>>();
	// Use this for initialization
	void Start () {
        int batchIndexNum = 0;
        List<Object> currBatch = new List<Object>();
        for(int i = 0; i < instances; i++)
        {
            AddObj(currBatch, i);
            batchIndexNum++;
            if (batchIndexNum >= 1023)
            {
                batches.Add(currBatch);
                currBatch = BuildNewBatch();
                batchIndexNum = 0;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        RenderBatches();
	}
    private void AddObj(List<Object>currBatch,int i)
    {
        Vector3 position = new Vector3(Random.Range(-maxPos.x, maxPos.x), Random.Range(-maxPos.y, maxPos.y), Random.Range(-maxPos.z, maxPos.z));
        currBatch.Add(new Object(position, new Vector3(2, 2, 2), Quaternion.identity));

    }

    private List<Object> BuildNewBatch()
    {
        return new List<Object>();
    }

    private void RenderBatches()
    {
        foreach(var batch in batches)
        {
            Graphics.DrawMeshInstanced(objMesh, 0, objMat, batch.Select((a) => a.Matrix).ToList());
        }
    }
}
