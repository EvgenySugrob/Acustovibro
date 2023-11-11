﻿/* Code provided by Chris Morris of Six Times Nothing (http://www.sixtimesnothing.com) */
/* Free to use and modify */
/* Edited by Aubergine (more than 3 lines of code:p)*/

using UnityEngine;
using System.Collections;

[AddComponentMenu("Frameworks/Terrain/TerrainNormalMap")]
public class TerrainNormalMap : MonoBehaviour
{

    public Texture2D Bump0;
    public Texture2D Bump1;
    public Texture2D Bump2;
    public Texture2D Bump3;

    public Texture2D[] DetailTXArray;
    private Texture2D DetailTX;
    private float img = 0.0F;
    private bool canDetail;
    private float detailFPS = 15.0F;

    public float Tile0;
    public float Tile1;
    public float Tile2;
    public float Tile3;

    public float Spec0;
    public float Spec1;
    public float Spec2;
    public float Spec3;

    public float terrainSizeX;
    public float terrainSizeZ;

    void Start()
    {

        Terrain terrainComp = (Terrain)GetComponent(typeof(Terrain));

        if (Bump0)
            Shader.SetGlobalTexture("_BumpMap0", Bump0);

        if (Bump1)
            Shader.SetGlobalTexture("_BumpMap1", Bump1);

        if (Bump2)
            Shader.SetGlobalTexture("_BumpMap2", Bump2);

        if (Bump3)
            Shader.SetGlobalTexture("_BumpMap3", Bump3);

        if (DetailTXArray.Length > 0)
        {
            canDetail = true;
            DetailTX = DetailTXArray[0];
            Shader.SetGlobalTexture("_DetailTX", DetailTX);
        }
        Shader.SetGlobalFloat("_Spec0", Spec0);
        Shader.SetGlobalFloat("_Spec1", Spec1);
        Shader.SetGlobalFloat("_Spec2", Spec2);
        Shader.SetGlobalFloat("_Spec3", Spec3);

        Shader.SetGlobalFloat("_Tile0", Tile0);
        Shader.SetGlobalFloat("_Tile1", Tile1);
        Shader.SetGlobalFloat("_Tile2", Tile2);
        Shader.SetGlobalFloat("_Tile3", Tile3);

        terrainSizeX = terrainComp.terrainData.size.x;
        terrainSizeZ = terrainComp.terrainData.size.z;

        Shader.SetGlobalFloat("_TerrainX", terrainSizeX);
        Shader.SetGlobalFloat("_TerrainZ", terrainSizeZ);
    }

    /* Don't need this update unless you're testing during play */
    void Update()
    {

        if (Bump0)
            Shader.SetGlobalTexture("_BumpMap0", Bump0);

        if (Bump1)
            Shader.SetGlobalTexture("_BumpMap1", Bump1);

        if (Bump2)
            Shader.SetGlobalTexture("_BumpMap2", Bump2);

        if (Bump3)
            Shader.SetGlobalTexture("_BumpMap3", Bump3);

        if (canDetail)
        {
            float i = Time.time * detailFPS;
            i = i % DetailTXArray.Length;
            DetailTX = DetailTXArray[(int)i];
            Shader.SetGlobalTexture("_DetailTX", DetailTX);
        }

        Shader.SetGlobalFloat("_Spec0", Spec0);
        Shader.SetGlobalFloat("_Spec1", Spec1);
        Shader.SetGlobalFloat("_Spec2", Spec2);
        Shader.SetGlobalFloat("_Spec3", Spec3);

        Shader.SetGlobalFloat("_Tile0", Tile0);
        Shader.SetGlobalFloat("_Tile1", Tile1);
        Shader.SetGlobalFloat("_Tile2", Tile2);
        Shader.SetGlobalFloat("_Tile3", Tile3);

        Shader.SetGlobalFloat("_TerrainX", terrainSizeX);
        Shader.SetGlobalFloat("_TerrainZ", terrainSizeZ);
    }
}