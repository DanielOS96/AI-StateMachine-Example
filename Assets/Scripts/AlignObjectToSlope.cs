using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Align game object to the surface normals it is on. 
/// <para>(Sometimes rotation can get screwd up and is not facing the player)</para>
/// </summary>
public class AlignObjectToSlope : MonoBehaviour
{
    public Transform transformToAlign;  //The transform that will be aligned.
    public float alignSpeed = 20;       //The speed at which the alignment takes place.
    public bool allowPositionAlign;     //Allow the position to be aligned.
    public bool allowRotationAlign;     //Allow the rotation to be aligned.
    public bool automaticAlign;         //Automatically align gameobject using 'FixedUpdate' method.

    private Vector3 posCur;             //The position aligned to the surface normal.
    private Quaternion rotCur;          //The rotation alligned to the surface normal.

    private void Awake()
    {
        //------SetUp Referances-------------------------------------------------------------------
        transformToAlign = transformToAlign == null ? GetComponent<Transform>() : transformToAlign;
        //-----------------------------------------------------------------------------------------
    }

    private void FixedUpdate()
    {
        if (automaticAlign)Align(transformToAlign);
    }



    //Preform the alignment.
    private void Align(Transform _transfromToAlign)
    {
        //declare a new Ray. It will start at this object's position and it's direction will be straight down from the object (in local space, that is)
        Ray ray = new Ray(transform.position, -transform.up);
        //decalre a RaycastHit. This is neccessary so it can get "filled" with information when casting the ray below.
        RaycastHit hit;

        //cast the ray. Note the "out hit" which makes the Raycast "fill" the hit variable with information. The maximum distance the ray will go is 1.5
        if (Physics.Raycast(ray, out hit, 0.5f) == true)
        {
            //draw a Debug Line so we can see the ray in the scene view.
            Debug.DrawLine(_transfromToAlign.position, hit.point, Color.green);

            //store the roation and position as they would be aligned on the surface
            rotCur = Quaternion.FromToRotation(_transfromToAlign.up, hit.normal) * _transfromToAlign.rotation;
            posCur = new Vector3(_transfromToAlign.position.x, hit.point.y, _transfromToAlign.position.z);

        }

        //smoothly rotate and move the objects until it's aligned to the surface.
        if (allowPositionAlign) _transfromToAlign.position = Vector3.Lerp(_transfromToAlign.position, posCur, Time.deltaTime * alignSpeed);
        if (allowRotationAlign) _transfromToAlign.rotation = Quaternion.Lerp(_transfromToAlign.rotation, rotCur, Time.deltaTime * alignSpeed);



    }

}
