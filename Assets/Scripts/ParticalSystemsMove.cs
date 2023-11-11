using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalSystemsMove : MonoBehaviour
{
    private Quaternion rotationTarget,rotationTargetReflection, rotationLongit;
    private Vector3 targetPositionRefl,targetPositionLongit;
    public GameObject soundWaveOut,soundWaveReflection,longitudinalWave;

    public void RotationOnWhallBack()
    {
        RotationWave(0f,0f,0f,0f,0f,-6.1f,0f,0f,0f,0f,0f,0f,0f,0f,0f);
    }
    public void RotationOnWhallRight()
    {
        RotationWave(0f,90f,0f,-6.46f,0f,0f,0f,90f,0f, -7.48f, -1.905f, 0f, 0f, 0f, 90f);
    }
    public void RotationOnWhallFront()
    {
        RotationWave(0f,180f,0f,0f,0f,6f,0f,180f,0f, 0f, -8.849f, 0f, 0f, 0f, 0f);
    }
    public void RotationWhallOnLeft()
    {
        RotationWave(0f,270f,0f,6.73f,0f,0f,0f,270f,0f, 7.623f, -1.925f, -0.157f, 0f, 0f, 90f);
    }


    public void RotationWave(float x,float y, float z, float xTransformRefl, float yTransformRefl, float zTransformRefl,float xRotationRefl,float yRotationRefl, float zRotationRefl,float xTransformLongit,float yTransformLongit,float zTransformLongit, float xRotationLongit, float yRotationLongit, float zRotationLongit)
    {
        rotationTarget = Quaternion.Euler(x, y, z);
        rotationTargetReflection = Quaternion.Euler(xRotationRefl, yRotationRefl, zRotationRefl);
        rotationLongit = Quaternion.Euler(xRotationLongit, yRotationLongit, zRotationLongit);
        targetPositionRefl = new Vector3(xTransformRefl, yTransformRefl, zTransformRefl);
        targetPositionLongit = new Vector3(xTransformLongit, yTransformLongit, zTransformLongit);
        soundWaveOut.transform.localRotation = rotationTarget;
        soundWaveReflection.transform.localRotation = rotationTargetReflection;
        soundWaveReflection.transform.localPosition =targetPositionRefl;
        longitudinalWave.transform.localPosition = targetPositionLongit;
        longitudinalWave.transform.localRotation = rotationLongit;
    }
}
