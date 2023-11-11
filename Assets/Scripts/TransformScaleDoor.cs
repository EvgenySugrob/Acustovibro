using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformScaleDoor : MonoBehaviour
{
    public GameObject door;
    public EnabledDisableDoorWavePanel enabledDisableDoorWave;
    public DoorParticleSystem doorParticleSystem;
    public WaveformDisplaySetting waveformDisplaySetting;

    private float sizeWoodDoor = 0.3f;
    private float targetPosition = 0.042f;
    private float startPositon = -0.01155017f;
    private float position;
    private bool mainTypeDoor = true;

    public void SwitchScaleDoor(int indexTypeDoor)
    {
        switch (indexTypeDoor)
        {
            case 0:
                if (mainTypeDoor == false)
                {
                    position = startPositon;
                    waveformDisplaySetting.DoorWaveToggleDisable();
                    enabledDisableDoorWave.DisabledDoorWaveToggle();
                    doorParticleSystem.StopDoorPS();
                    MorphDoor(sizeWoodDoor, position);

                    mainTypeDoor = true;
                }
                
                break;
            case 1:
                if (mainTypeDoor == true) 
                {
                    position = targetPosition;
                    enabledDisableDoorWave.EnabledDoorWaveToggle();
                    MorphDoor(-sizeWoodDoor, position);

                    mainTypeDoor = false;
                }
               
                break;
        }
    }

    private void MorphDoor(float localSizeDoor,float endPosition)
    {
        door.transform.localScale = new Vector3(door.transform.localScale.x, door.transform.localScale.y+localSizeDoor, door.transform.localScale.z);
        door.transform.localPosition = new Vector3(door.transform.localPosition.x, endPosition, door.transform.localPosition.z);
    }
}
