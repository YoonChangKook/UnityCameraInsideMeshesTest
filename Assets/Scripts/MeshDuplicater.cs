using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshDuplicater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MakeBackFaceObjs(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 자기 자신부터 자기 자신 아래의 모든 자식들까지 재귀적으로 back face 매쉬를 만들어낸다.
    /// </summary>
    /// <param name="gameObject">복제할 게임 오브젝트</param>
    /// <param name="parent">부모 게임 오브젝트</param>
    private void MakeBackFaceObjs(GameObject gameObject, GameObject parent = null)
    {
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();

        // make back face object when the game object has a mesh
        if (meshFilter != null && meshRenderer != null)
        {
            // get current mesh
            Mesh mesh = meshFilter.mesh;
            // Raycast를 정상적으로 하기 위해선 반드시 MeshCollider가 필요함
            MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
            if (meshCollider == null)
            {
                MeshCollider newCollider = gameObject.AddComponent<MeshCollider>();
                newCollider.sharedMesh = mesh;
            }
            
            // create back face mesh
            Mesh backFaceMesh = Instantiate(mesh);
            backFaceMesh.triangles = backFaceMesh.triangles.Reverse().ToArray();

            // make back face mesh game object
            GameObject backFaceMeshObj = MakeGameObjectWithMesh(backFaceMesh, meshRenderer.materials);
            if (parent != null)
                backFaceMeshObj.transform.parent = parent.transform;

            backFaceMeshObj.name = gameObject.gameObject.name + "-Backface";
            backFaceMeshObj.tag = gameObject.gameObject.tag;
            backFaceMeshObj.layer = gameObject.gameObject.layer;
            backFaceMeshObj.transform.position = gameObject.transform.position;
            backFaceMeshObj.transform.localScale = gameObject.transform.localScale;
            backFaceMeshObj.transform.rotation = gameObject.transform.rotation;
        }

        int childCount = gameObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
            MakeBackFaceObjs(gameObject.transform.GetChild(i).gameObject, gameObject);
    }

    /// <summary>
    /// Mesh 및 Material을 받아서 게임 오브젝트를 만든다.
    /// </summary>
    /// <param name="mesh">게임 오브젝트의 매쉬</param>
    /// <param name="materials">게임 오브젝트의 재질</param>
    /// <param name="meshCenter">매쉬의 중앙 좌표. 기본값은 Vector3.zero</param>
    /// <returns></returns>
    private GameObject MakeGameObjectWithMesh(Mesh mesh, Material[] materials, Vector3? meshCenter = null)
    {
        GameObject gameObject = new GameObject(mesh.name);
        MeshCollider mc = gameObject.AddComponent<MeshCollider>();
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();
        MeshFilter mf = gameObject.AddComponent<MeshFilter>();

        Vector3 center = Vector3.zero;
        if (meshCenter != null)
            center = meshCenter.Value;

        Vector3[] newVertices = new Vector3[mesh.vertexCount];
        for (int i = 0; i < mesh.vertexCount; i++)
            newVertices[i] = mesh.vertices[i] - center;
        mesh.vertices = newVertices;

        mesh.RecalculateBounds();

        mc.sharedMesh = mesh;
        mr.materials = materials;
        mf.mesh = mesh;

        return gameObject;
    }
}
