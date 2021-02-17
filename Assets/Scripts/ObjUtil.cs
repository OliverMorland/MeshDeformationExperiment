using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ObjUtil : MonoBehaviour
{


    static public bool SaveMesh(Mesh mesh, string path)
    {
        Vector3[] vs = mesh.vertices;
        Vector3[] ns = mesh.normals;
        Vector2[] uvs = mesh.uv;
        int[] tris = mesh.triangles;

        return SaveMeshFromArrays(path, vs, ns, uvs, tris);
    }


    // Save mesh to obj.  Returns true if mesh is valid for obj.
    // Need a version that will use mesh renderer.
    static public bool SaveMeshFromArrays (string path, Vector3[] vs, Vector3[] ns, Vector2[] uvs, int[] tris)
    {

        // Must either have no normals or the same number of normals as vertices
        if ((ns.Length != 0) && (ns.Length != vs.Length))
            return false;

        // Must either have no uvs or the same number of uvs as vertices
        if ((uvs.Length != 0) && (uvs.Length != vs.Length))
            return false;

        StreamWriter sw = File.CreateText(path);
        sw.WriteLine("# Created by ObjUtil");
        for (int i = 0; i < vs.Length; i++)
            sw.WriteLine("v " + vs[i].x.ToString("R") + " " + vs[i].y.ToString("R") + " " + vs[i].z.ToString("R"));
        sw.WriteLine("# " + vs.Length.ToString() + " vertices");

        for (int i = 0; i < uvs.Length; i++)
            sw.WriteLine("vt " + uvs[i].x.ToString("R") + " " + uvs[i].y.ToString("R"));
        sw.WriteLine("# " + uvs.Length.ToString() + " texture vertices");

        for (int i = 0; i < ns.Length; i++)
            sw.WriteLine("n " + ns[i].x.ToString("R") + " " + ns[i].y.ToString("R") + " " + ns[i].z.ToString("R"));
        sw.WriteLine("# " + ns.Length.ToString() + " normals");

        // default format = "v/uv/n"
        string fmt = "f {0}/{1}/{2} {3}/{4}/{5} {6}/{7}/{8}"; 

        // format if no uv and no ns = "v" 
        if ((ns.Length == 0) && (uvs.Length == 0))
            fmt = "f {0} {3} {6}";
        // format if no uv = "v//n" 
        else if (uvs.Length == 0)
            fmt = "f {0}//{2} {3}//{5} {6}//{8}";
        // format if no ns = "v/uv" 
        else if (ns.Length == 0)
            fmt = "f {0}/{1} {3}/{4} {6}/{7}";

        for (int i = 0; i < tris.Length; i+= 3)
        {
            // indeces are "1" based (instead of "0" based)
            int vIdx0 = tris[i + 0] + 1;
            int uvIdx0 = tris[i + 0] + 1;
            int nIdx0 = tris[i + 0] + 1;
            int vIdx1 = tris[i + 1] + 1;
            int uvIdx1 = tris[i + 1] + 1;
            int nIdx1 = tris[i + 1] + 1;
            int vIdx2 = tris[i + 2] + 1;
            int uvIdx2 = tris[i + 2] + 1;
            int nIdx2 = tris[i + 2] + 1;

            sw.WriteLine(fmt, vIdx0, uvIdx0, nIdx0, vIdx1, uvIdx1, nIdx1, vIdx2, uvIdx2, nIdx2);
        }
        sw.Close();

        return true;
    }
}
