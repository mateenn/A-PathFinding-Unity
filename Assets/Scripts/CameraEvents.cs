using UnityEngine;

public class CameraEvents : MonoBehaviour
{
    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject.TryGetComponent(out ObjectData _data))
                {
                    if (_data.ObjectType == Variables.ObjectType.Passenger)
                    {
                        Debug.Log("Passenger");
                        _data.PerformPassengerAction();
                    }
                    else
                    {
                        Debug.Log("Not Passenger");
                    }
                }
            }
        }
    }
}
