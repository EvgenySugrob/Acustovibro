using UnityEngine;

namespace Vmaya.Util
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class MergeMesh : MonoBehaviour
    {
        public Transform meshes;

        public void Merge()
        {
            MeshFilter mf = GetComponent<MeshFilter>();
            mf.mesh.Clear();

            MeshFilter[] meshFilters = meshes.GetComponentsInChildren<MeshFilter>(true);
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            int i = 0;
            while (i < meshFilters.Length)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = transform.worldToLocalMatrix * meshFilters[i].transform.localToWorldMatrix;
                i++;
            }
            mf.mesh = new Mesh();
            mf.mesh.CombineMeshes(combine, true, true);

            MeshCollider col = GetComponent<MeshCollider>();
            if (col) col.sharedMesh = mf.mesh;
        }

        public void Clear()
        {
            GetComponent<MeshFilter>().mesh.Clear();
        }
    }
}