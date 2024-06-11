using System.Collections;
using UnityEngine;

public class VehicleMover : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.
    int maxIndex;  

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f; 

    void Start()
    {       
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        maxIndex = SortWay.MoveToTarget.Count - 1; 
    }

    void Update()
    {
        if (IngameMenu.DarkModeOn)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (isWaiting)
        {
            return; // Vehicle doesnt move uppon reaching a station.
        }

        // The vehicles moves to the next point from the "MoveToTarget" list.
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed);

        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (TranSportWayMarker.StationOrder.Contains(transform.position))
            {
                // If the vehicle reaches a station, it stops.
                StartCoroutine(Waiting());
            }
            if(targetIndex + 1 > maxIndex) 
            {
                // If the vehicle reaches the last point of the list, it destroys itself.
                Destroy(gameObject);
                return;
            }
            else if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]); // Rotates te vehicle in the right direction.

            targetIndex += 1;           
        }
    }

    /// <summary>
    /// Stops the vehicle movement for 2 seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);
        isWaiting = false;
    }
}
public class VehicleMover : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.
    int maxIndex;  

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f; 

    void Start()
    {       
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        maxIndex = SortWay.MoveToTarget.Count - 1; 
    }

    void Update()
    {
        if (IngameMenu.DarkModeOn)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (isWaiting)
        {
            return; // Vehicle doesnt move uppon reaching a station.
        }

        // The vehicles moves to the next point from the "MoveToTarget" list.
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed);

        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (TranSportWayMarker.StationOrder.Contains(transform.position))
            {
                // If the vehicle reaches a station, it stops.
                StartCoroutine(Waiting());
            }
            if(targetIndex + 1 > maxIndex) 
            {
                // If the vehicle reaches the last point of the list, it destroys itself.
                Destroy(gameObject);
                return;
            }
            else if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]); // Rotates te vehicle in the right direction.

            targetIndex += 1;           
        }
    }

    /// <summary>
    /// Stops the vehicle movement for 2 seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);
        isWaiting = false;
    }
}
public class VehicleMover : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.
    int maxIndex;  

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f; 

    void Start()
    {       
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        maxIndex = SortWay.MoveToTarget.Count - 1; 
    }

    void Update()
    {
        if (IngameMenu.DarkModeOn)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (isWaiting)
        {
            return; // Vehicle doesnt move uppon reaching a station.
        }

        // The vehicles moves to the next point from the "MoveToTarget" list.
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed);

        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (TranSportWayMarker.StationOrder.Contains(transform.position))
            {
                // If the vehicle reaches a station, it stops.
                StartCoroutine(Waiting());
            }
            if(targetIndex + 1 > maxIndex) 
            {
                // If the vehicle reaches the last point of the list, it destroys itself.
                Destroy(gameObject);
                return;
            }
            else if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]); // Rotates te vehicle in the right direction.

            targetIndex += 1;           
        }
    }

    /// <summary>
    /// Stops the vehicle movement for 2 seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);
        isWaiting = false;
    }
}
public class VehicleMover : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.
    int maxIndex;  

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f; 

    void Start()
    {       
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        maxIndex = SortWay.MoveToTarget.Count - 1; 
    }

    void Update()
    {
        if (IngameMenu.DarkModeOn)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (isWaiting)
        {
            return; // Vehicle doesnt move uppon reaching a station.
        }

        // The vehicles moves to the next point from the "MoveToTarget" list.
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed);

        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (TranSportWayMarker.StationOrder.Contains(transform.position))
            {
                // If the vehicle reaches a station, it stops.
                StartCoroutine(Waiting());
            }
            if(targetIndex + 1 > maxIndex) 
            {
                // If the vehicle reaches the last point of the list, it destroys itself.
                Destroy(gameObject);
                return;
            }
            else if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]); // Rotates te vehicle in the right direction.

            targetIndex += 1;           
        }
    }

    /// <summary>
    /// Stops the vehicle movement for 2 seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);
        isWaiting = false;
    }
}
public class VehicleMover : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.
    int maxIndex;  

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f; 

    void Start()
    {       
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        maxIndex = SortWay.MoveToTarget.Count - 1; 
    }

    void Update()
    {
        if (IngameMenu.DarkModeOn)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (isWaiting)
        {
            return; // Vehicle doesnt move uppon reaching a station.
        }

        // The vehicles moves to the next point from the "MoveToTarget" list.
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed);

        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (TranSportWayMarker.StationOrder.Contains(transform.position))
            {
                // If the vehicle reaches a station, it stops.
                StartCoroutine(Waiting());
            }
            if(targetIndex + 1 > maxIndex) 
            {
                // If the vehicle reaches the last point of the list, it destroys itself.
                Destroy(gameObject);
                return;
            }
            else if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]); // Rotates te vehicle in the right direction.

            targetIndex += 1;           
        }
    }

    /// <summary>
    /// Stops the vehicle movement for 2 seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);
        isWaiting = false;
    }
}
public class VehicleMover : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.
    int maxIndex;  

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f; 

    void Start()
    {       
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        maxIndex = SortWay.MoveToTarget.Count - 1; 
    }

    void Update()
    {
        if (IngameMenu.DarkModeOn)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (isWaiting)
        {
            return; // Vehicle doesnt move uppon reaching a station.
        }

        // The vehicles moves to the next point from the "MoveToTarget" list.
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed);

        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (TranSportWayMarker.StationOrder.Contains(transform.position))
            {
                // If the vehicle reaches a station, it stops.
                StartCoroutine(Waiting());
            }
            if(targetIndex + 1 > maxIndex) 
            {
                // If the vehicle reaches the last point of the list, it destroys itself.
                Destroy(gameObject);
                return;
            }
            else if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]); // Rotates te vehicle in the right direction.

            targetIndex += 1;           
        }
    }

    /// <summary>
    /// Stops the vehicle movement for 2 seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);
        isWaiting = false;
    }
}
public class VehicleMover : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.
    int maxIndex;  

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f; 

    void Start()
    {       
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        maxIndex = SortWay.MoveToTarget.Count - 1; 
    }

    void Update()
    {
        if (IngameMenu.DarkModeOn)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (isWaiting)
        {
            return; // Vehicle doesnt move uppon reaching a station.
        }

        // The vehicles moves to the next point from the "MoveToTarget" list.
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed);

        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (TranSportWayMarker.StationOrder.Contains(transform.position))
            {
                // If the vehicle reaches a station, it stops.
                StartCoroutine(Waiting());
            }
            if(targetIndex + 1 > maxIndex) 
            {
                // If the vehicle reaches the last point of the list, it destroys itself.
                Destroy(gameObject);
                return;
            }
            else if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]); // Rotates te vehicle in the right direction.

            targetIndex += 1;           
        }
    }

    /// <summary>
    /// Stops the vehicle movement for 2 seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);
        isWaiting = false;
    }
}
public class VehicleMover : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.
    int maxIndex;  

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f; 

    void Start()
    {       
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        maxIndex = SortWay.MoveToTarget.Count - 1; 
    }

    void Update()
    {
        if (IngameMenu.DarkModeOn)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (isWaiting)
        {
            return; // Vehicle doesnt move uppon reaching a station.
        }

        // The vehicles moves to the next point from the "MoveToTarget" list.
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed);

        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (TranSportWayMarker.StationOrder.Contains(transform.position))
            {
                // If the vehicle reaches a station, it stops.
                StartCoroutine(Waiting());
            }
            if(targetIndex + 1 > maxIndex) 
            {
                // If the vehicle reaches the last point of the list, it destroys itself.
                Destroy(gameObject);
                return;
            }
            else if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]); // Rotates te vehicle in the right direction.

            targetIndex += 1;           
        }
    }

    /// <summary>
    /// Stops the vehicle movement for 2 seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);
        isWaiting = false;
    }
}
public class VehicleMover : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.
    int maxIndex;  

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f; 

    void Start()
    {       
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        maxIndex = SortWay.MoveToTarget.Count - 1; 
    }

    void Update()
    {
        if (IngameMenu.DarkModeOn)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (isWaiting)
        {
            return; // Vehicle doesnt move uppon reaching a station.
        }

        // The vehicles moves to the next point from the "MoveToTarget" list.
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed);

        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (TranSportWayMarker.StationOrder.Contains(transform.position))
            {
                // If the vehicle reaches a station, it stops.
                StartCoroutine(Waiting());
            }
            if(targetIndex + 1 > maxIndex) 
            {
                // If the vehicle reaches the last point of the list, it destroys itself.
                Destroy(gameObject);
                return;
            }
            else if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]); // Rotates te vehicle in the right direction.

            targetIndex += 1;           
        }
    }

    /// <summary>
    /// Stops the vehicle movement for 2 seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);
        isWaiting = false;
    }
}
public class VehicleMover : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.
    int maxIndex;  

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f; 

    void Start()
    {       
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        maxIndex = SortWay.MoveToTarget.Count - 1; 
    }

    void Update()
    {
        if (IngameMenu.DarkModeOn)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (isWaiting)
        {
            return; // Vehicle doesnt move uppon reaching a station.
        }

        // The vehicles moves to the next point from the "MoveToTarget" list.
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed);

        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (TranSportWayMarker.StationOrder.Contains(transform.position))
            {
                // If the vehicle reaches a station, it stops.
                StartCoroutine(Waiting());
            }
            if(targetIndex + 1 > maxIndex) 
            {
                // If the vehicle reaches the last point of the list, it destroys itself.
                Destroy(gameObject);
                return;
            }
            else if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]); // Rotates te vehicle in the right direction.

            targetIndex += 1;           
        }
    }

    /// <summary>
    /// Stops the vehicle movement for 2 seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);
        isWaiting = false;
    }
}
public class VehicleMover : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.
    int maxIndex;  

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f; 

    void Start()
    {       
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        maxIndex = SortWay.MoveToTarget.Count - 1; 
    }

    void Update()
    {
        if (IngameMenu.DarkModeOn)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (isWaiting)
        {
            return; // Vehicle doesnt move uppon reaching a station.
        }

        // The vehicles moves to the next point from the "MoveToTarget" list.
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed);

        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (TranSportWayMarker.StationOrder.Contains(transform.position))
            {
                // If the vehicle reaches a station, it stops.
                StartCoroutine(Waiting());
            }
            if(targetIndex + 1 > maxIndex) 
            {
                // If the vehicle reaches the last point of the list, it destroys itself.
                Destroy(gameObject);
                return;
            }
            else if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]); // Rotates te vehicle in the right direction.

            targetIndex += 1;           
        }
    }

    /// <summary>
    /// Stops the vehicle movement for 2 seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);
        isWaiting = false;
    }
}
public class VehicleMover : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.
    int maxIndex;  

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f; 

    void Start()
    {       
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        maxIndex = SortWay.MoveToTarget.Count - 1; 
    }

    void Update()
    {
        if (IngameMenu.DarkModeOn)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (isWaiting)
        {
            return; // Vehicle doesnt move uppon reaching a station.
        }

        // The vehicles moves to the next point from the "MoveToTarget" list.
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed);

        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (TranSportWayMarker.StationOrder.Contains(transform.position))
            {
                // If the vehicle reaches a station, it stops.
                StartCoroutine(Waiting());
            }
            if(targetIndex + 1 > maxIndex) 
            {
                // If the vehicle reaches the last point of the list, it destroys itself.
                Destroy(gameObject);
                return;
            }
            else if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]); // Rotates te vehicle in the right direction.

            targetIndex += 1;           
        }
    }

    /// <summary>
    /// Stops the vehicle movement for 2 seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);
        isWaiting = false;
    }
}
public class VehicleMover : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.
    int maxIndex;  

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f; 

    void Start()
    {       
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        maxIndex = SortWay.MoveToTarget.Count - 1; 
    }

    void Update()
    {
        if (IngameMenu.DarkModeOn)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        if (isWaiting)
        {
            return; // Vehicle doesnt move uppon reaching a station.
        }

        // The vehicles moves to the next point from the "MoveToTarget" list.
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed);

        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (TranSportWayMarker.StationOrder.Contains(transform.position))
            {
                // If the vehicle reaches a station, it stops.
                StartCoroutine(Waiting());
            }
            if(targetIndex + 1 > maxIndex) 
            {
                // If the vehicle reaches the last point of the list, it destroys itself.
                Destroy(gameObject);
                return;
            }
            else if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]); // Rotates te vehicle in the right direction.

            targetIndex += 1;           
        }
    }

    /// <summary>
    /// Stops the vehicle movement for 2 seconds.
    /// </summary>
    /// <returns></returns>
    IEnumerator Waiting()
    {
        isWaiting = true;
        yield return new WaitForSeconds(2);
        isWaiting = false;
    }
}
