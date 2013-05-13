using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class TycoonBuildManager : Singleton<TycoonBuildManager>
{
    public List<GameObject> StructureOptions = new List<GameObject>();
    public Material BuildMaterial;
    public int BuildGridSize = 1;
    public Color ValidBuildColor = Color.yellow;
    public Color InvalidBuildColor = Color.red;

    private GameObject mCurrentBuildOption = null;
    private GameObject mCurrentBuildOptionGhost = null;
    private bool mBuildLocationIsValid = false;

    public GameObject CurrentBuildOption
    {
        get
        {
            return mCurrentBuildOption;
        }
    }

    public void SetCurrentBuildOption(GameObject prefab)
    {
        if (mCurrentBuildOption != null)
        {
            mCurrentBuildOption = null;
            GameObject.Destroy(mCurrentBuildOptionGhost.gameObject);
        }

        if (prefab == null)
        {
            return;
        }

        mCurrentBuildOption = prefab;

        GameObject newBuildOption = new GameObject();

        foreach(Transform t in prefab.transform)
        {
            MeshFilter originalMeshFilter = t.GetComponent<MeshFilter>();

            if (originalMeshFilter == null)
            {
                continue;
            }

            GameObject child = new GameObject();
            MeshRenderer childRenderer = child.AddComponent<MeshRenderer>();
            MeshFilter childMeshFilter = child.AddComponent<MeshFilter>();

            childMeshFilter.mesh = Mesh.Instantiate(originalMeshFilter.sharedMesh) as Mesh;

            childRenderer.material = new Material(BuildMaterial);
            childRenderer.material.mainTexture = t.renderer.sharedMaterial.mainTexture;

            child.transform.parent = newBuildOption.transform;
            child.transform.position = t.localPosition;
            child.transform.rotation = t.localRotation;
            child.transform.localScale = t.localScale;
        }

        mCurrentBuildOptionGhost = newBuildOption;
    }

    private void Update()
    {
        if (mCurrentBuildOption == null)
        {
            Screen.showCursor = true;
            return;
        }

        Screen.showCursor = false;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;

        pos.x = pos.x - (pos.x % BuildGridSize);
        pos.y = pos.y - (pos.y % BuildGridSize);

        mCurrentBuildOptionGhost.transform.position = pos;

        Ray ray = new Ray(mCurrentBuildOptionGhost.transform.position + new Vector3(0, 0.1f, 0), Vector3.down);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, 0.1f, 1 << LayerMask.NameToLayer("GroundLayer")))
        {
            UpdateColor(ValidBuildColor);
            mBuildLocationIsValid = true;
        }
        else
        {
            UpdateColor(InvalidBuildColor);
            mBuildLocationIsValid = false;
        }

        if (Input.GetMouseButtonDown(0) && mBuildLocationIsValid)
        {
            GameObject.Instantiate(mCurrentBuildOption, mCurrentBuildOptionGhost.transform.position, Quaternion.identity);
            SetCurrentBuildOption(null);
        }
    }

    private void UpdateColor(Color color)
    {
        foreach(Transform child in mCurrentBuildOptionGhost.transform)
        {
            if (child.renderer == null)
            {
                continue;
            }

            child.renderer.sharedMaterial.color = color;
        }
    }
}
