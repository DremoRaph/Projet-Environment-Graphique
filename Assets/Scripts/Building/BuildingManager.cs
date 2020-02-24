using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    public float buildingSizeX;
    public float buildingSizeZ;

    public Camera camera;

    [SerializeField]
    List<GameObject> objectToBuild;
    [SerializeField]
    int indexObjectToBuild;
    GameObject objectPosPreview;

    [SerializeField]
    Material goodPosMaterial;
    [SerializeField]
    Material badPosMaterial;

    Vector3 buildingPosition;

    List<SpaceTaken> spaceTaken;

    bool freeSpace;

    private void Start()
    {

        spaceTaken = new List<SpaceTaken>();

    }


    // Update is called once per frame
    void Update()
    {

        changeObjectToBuild();//provisoire

        

        PosPreview();

        checkCurrentSpace();


        if (Input.GetMouseButtonDown(0) && freeSpace)
        {

            BuildObject();

        }



    }
    
    void checkCurrentSpace()
    {
               
        SpaceTaken currentSpace;

        currentSpace.X = new Vector2(buildingPosition.x + ( buildingSizeX / 2 ), buildingPosition.x - ( buildingSizeX / 2 ));

        currentSpace.Z = new Vector2(buildingPosition.z + ( buildingSizeZ / 2 ), buildingPosition.z - ( buildingSizeZ / 2 ));


        bool isFree = true;


        foreach (SpaceTaken space in spaceTaken)
        {
            if (currentSpace.X == space.X && currentSpace.Z == space.Z)
            {
                isFree = false;
            }

            if ( space.X.y < currentSpace.X.x && currentSpace.X.x <= space.X.x )
            {
                if (( space.Z.y < currentSpace.Z.x && currentSpace.Z.x <= space.Z.x ) || 
                    ( space.Z.y <= currentSpace.Z.y && currentSpace.Z.y < space.Z.x ))
                {
                    isFree = false;
                }
            }

            if ( space.X.y <= currentSpace.X.y && currentSpace.X.y < space.X.x )
            {
                if (( space.Z.y < currentSpace.Z.x && currentSpace.Z.x <= space.Z.x ) ||
                    ( space.Z.y <= currentSpace.Z.y && currentSpace.Z.y < space.Z.x ))
                {
                    isFree = false;
                }
            }
        }

        this.freeSpace = isFree;

        if (isFree)
        {
            objectPosPreview.GetComponentInChildren<MeshRenderer>().material = goodPosMaterial;
        }
        else
        {
            objectPosPreview.GetComponentInChildren<MeshRenderer>().material = badPosMaterial;
        }
        
    }

    private void PosPreview()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {

            if (hit.transform.tag == "Ground") {
                Vector3 hitPoint = hit.point;

                buildingPosition.x = Mathf.Floor(hitPoint.x / 1) + (0.5f * buildingSizeX );
                buildingPosition.y = Mathf.Floor(hitPoint.y + 0.5f );
                buildingPosition.z = Mathf.Floor(hitPoint.z / 1 ) + ( 0.5f * buildingSizeZ );

                objectPosPreview.transform.position = buildingPosition;
            }
        }
        
    }

    void BuildObject()
    {


        GameObject instanceObjectToBuild = Instantiate(objectToBuild[indexObjectToBuild], buildingPosition, Quaternion.identity);


        SpaceTaken instanceSpace;

        instanceSpace.X = new Vector2(( buildingSizeX / 2 ) + buildingPosition.x, buildingPosition.x  - ( buildingSizeX / 2 ));

        instanceSpace.Z = new Vector2(( buildingSizeZ / 2 ) + buildingPosition.z, buildingPosition.z  - ( buildingSizeZ / 2 ));

        spaceTaken.Add(instanceSpace);
    }


    void changeObjectToBuild(/*int indexObjectToBuild*/)
    {
        Destroy(objectPosPreview);
        //this.indexObjectToBuild = indexObjectToBuild;
        objectPosPreview = Instantiate(objectToBuild[indexObjectToBuild]);
        objectPosPreview.name = "objectPosPreview";
        Collider[] colliders = objectPosPreview.GetComponentsInChildren<Collider>();
        foreach (Collider item in colliders)
        {
            item.isTrigger = true;
        }
        Transform buildingZone = objectPosPreview.transform.Find("BuildingZone");
        buildingSizeX = buildingZone.localScale.x;
        buildingSizeZ = buildingZone.localScale.z;
    }
}

struct SpaceTaken
{
    public Vector2 X;
    public Vector2 Z;
}