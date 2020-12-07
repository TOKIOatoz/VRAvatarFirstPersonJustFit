using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPositionByTOKI : MonoBehaviour
{
    [SerializeField] GameObject baseController;
    [SerializeField] GameObject baseBone;
    [SerializeField] float scaleX = 1;
    [SerializeField] float scaleY = 1;
    [SerializeField] float scaleZ = 1;

    Vector3 targetPos = Vector3.zero;
    Vector3 difference = Vector3.zero;
    Vector3 cameraEyeUVW = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;
        float theta = (baseBone.transform.eulerAngles.y * Mathf.PI) / 180;
        float sin = Mathf.Sin(theta);
        float cos = Mathf.Cos(theta);

        cameraEyeUVW.x = (baseBone.transform.position.x * cos) - (baseBone.transform.position.z * sin);
        cameraEyeUVW.z = (baseBone.transform.position.x * sin) + (baseBone.transform.position.z * cos);

        float controllerU1 = cameraEyeUVW.x + (((baseController.transform.localPosition.x * cos) - (baseController.transform.localPosition.z * sin)) - (cameraEyeUVW.x)) * scaleX;
        float controllerW1 = cameraEyeUVW.z + (((baseController.transform.localPosition.x * sin) + (baseController.transform.localPosition.z * cos)) - (cameraEyeUVW.z)) * scaleZ;

        targetPos.x = (controllerU1 * cos) + (controllerW1 * sin);
        targetPos.z = (controllerW1 * cos) - (controllerU1 * sin);

        targetPos.y = baseController.transform.localPosition.y * scaleY;
        myTransform.localPosition = targetPos;
        myTransform.rotation = baseController.transform.rotation;
    }
}
