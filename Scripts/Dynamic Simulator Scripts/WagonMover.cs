using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
public class WagonMoverSample : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {   
        
    }
}
*/

public class WagonMover : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f;

    GameObject WagonToFollow;

    Vector3 lastWagonPos = Vector3.zero;
    Vector3 curWagonPos = new Vector3(0, 0, 1);

    float velocity = 1f;

    void Start()
    {
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        List<GameObject> allGameObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(allGameObjects);

        // The object which is being followed is set to be the last object which was generated in the Unity scene.
        WagonToFollow = allGameObjects[allGameObjects.Count - 2];
    }

    void Update()
    {
        if (WagonToFollow == null)
        {
            gameObject.Destroy(); // If the followed wagon is destroyed (because it reached the endpoint) the following wagon will also destroy itself.
            return;
        }

        // Here we measure the distance to the wagon which is being followed.
        float distance = Vector3.Distance(WagonToFollow.transform.GetChild(0).GetChild(1).position, transform.GetChild(0).GetChild(0).position);
        if (Vector3.Distance(WagonToFollow.transform.position, transform.position) < 30)
        {
            velocity = .1f;
        }
        else if (distance > 20)
        {
            // If the distance to the followed wagon is too big, the following wagon will increase its speed.
            velocity = 1.2f;
        }
        else if (distance < 10)
        {
            // If the distance to the followed wagon is too small, the following wagon will decrease its speed.
            velocity = .8f;
        }
        else
        {
            // If the distance to the followed wagon is good, the speed of the following wagon will stay the same.
            velocity = 1f;
        }

        // Here we measure if the followed wagon has stopped. If this is the case, the following wagon will also stop
        // and move as sonn as the followed wagon start moving again.
        curWagonPos = WagonToFollow.transform.position;
        if (curWagonPos == lastWagonPos)
        {
            // Followed wagon has stopped.
            isWaiting = true;
        }
        else
        {
            // Followed wagon is moving.
            isWaiting = false;
        }
        lastWagonPos = curWagonPos;
        if (isWaiting)
        {
            return;
        }

        // The wagon vehicle is being moved to the next point of the "MoveToTarget" list
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed * velocity);
        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]);

            targetIndex += 1;
        }
        /*
        if (WagonToFollow == null)
        {
            gameObject.Destroy(); // If the followed wagon is destroyed (because it reached the endpoint) the following wagon will also destroy itself.
            return;
        }

        // Here we measure the distance to the wagon which is being followed.
        float distance = Vector3.Distance(WagonToFollow.transform.GetChild(0).GetChild(1).position, transform.GetChild(0).GetChild(0).position);
        if (Vector3.Distance(WagonToFollow.transform.position, transform.position) < 30)
        {
            velocity = .1f;
        }
        else if (distance > 20)
        {
            // If the distance to the followed wagon is too big, the following wagon will increase its speed.
            velocity = 1.2f;
        }
        else if (distance < 10)
        {
            // If the distance to the followed wagon is too small, the following wagon will decrease its speed.
            velocity = .8f;
        }
        else
        {
            // If the distance to the followed wagon is good, the speed of the following wagon will stay the same.
            velocity = 1f;
        }

        // Here we measure if the followed wagon has stopped. If this is the case, the following wagon will also stop
        // and move as sonn as the followed wagon start moving again.
        curWagonPos = WagonToFollow.transform.position;
        if (curWagonPos == lastWagonPos)
        {
            // Followed wagon has stopped.
            isWaiting = true;
        }
        else
        {
            // Followed wagon is moving.
            isWaiting = false;
        }
        lastWagonPos = curWagonPos;
        if (isWaiting)
        {
            return;
        }

        // The wagon vehicle is being moved to the next point of the "MoveToTarget" list
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed * velocity);
        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]);

            targetIndex += 1;
        }
         */
    }
}

public class WagonMover_1 : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f;

    GameObject WagonToFollow;

    Vector3 lastWagonPos = Vector3.zero;
    Vector3 curWagonPos = new Vector3(0, 0, 1);

    float velocity = 1f;

    void Start()
    {
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        List<GameObject> allGameObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(allGameObjects);

        // The object which is being followed is set to be the last object which was generated in the Unity scene.
        WagonToFollow = allGameObjects[allGameObjects.Count - 2];
    }

    void Update()
    {
        if (WagonToFollow == null)
        {
            gameObject.Destroy(); // If the followed wagon is destroyed (because it reached the endpoint) the following wagon will also destroy itself.
            return;
        }

        // Here we measure the distance to the wagon which is being followed.
        float distance = Vector3.Distance(WagonToFollow.transform.GetChild(0).GetChild(1).position, transform.GetChild(0).GetChild(0).position);
        if (Vector3.Distance(WagonToFollow.transform.position, transform.position) < 30)
        {
            velocity = .1f;
        }
        else if (distance > 20)
        {
            // If the distance to the followed wagon is too big, the following wagon will increase its speed.
            velocity = 1.2f;
        }
        else if (distance < 10)
        {
            // If the distance to the followed wagon is too small, the following wagon will decrease its speed.
            velocity = .8f;
        }
        else
        {
            // If the distance to the followed wagon is good, the speed of the following wagon will stay the same.
            velocity = 1f;
        }

        // Here we measure if the followed wagon has stopped. If this is the case, the following wagon will also stop
        // and move as sonn as the followed wagon start moving again.
        curWagonPos = WagonToFollow.transform.position;
        if (curWagonPos == lastWagonPos)
        {
            // Followed wagon has stopped.
            isWaiting = true;
        }
        else
        {
            // Followed wagon is moving.
            isWaiting = false;
        }
        lastWagonPos = curWagonPos;
        if (isWaiting)
        {
            return;
        }

        // The wagon vehicle is being moved to the next point of the "MoveToTarget" list
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed * velocity);
        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]);

            targetIndex += 1;
        }
        /*
        if (WagonToFollow == null)
        {
            gameObject.Destroy(); // If the followed wagon is destroyed (because it reached the endpoint) the following wagon will also destroy itself.
            return;
        }

        // Here we measure the distance to the wagon which is being followed.
        float distance = Vector3.Distance(WagonToFollow.transform.GetChild(0).GetChild(1).position, transform.GetChild(0).GetChild(0).position);
        if (Vector3.Distance(WagonToFollow.transform.position, transform.position) < 30)
        {
            velocity = .1f;
        }
        else if (distance > 20)
        {
            // If the distance to the followed wagon is too big, the following wagon will increase its speed.
            velocity = 1.2f;
        }
        else if (distance < 10)
        {
            // If the distance to the followed wagon is too small, the following wagon will decrease its speed.
            velocity = .8f;
        }
        else
        {
            // If the distance to the followed wagon is good, the speed of the following wagon will stay the same.
            velocity = 1f;
        }

        // Here we measure if the followed wagon has stopped. If this is the case, the following wagon will also stop
        // and move as sonn as the followed wagon start moving again.
        curWagonPos = WagonToFollow.transform.position;
        if (curWagonPos == lastWagonPos)
        {
            // Followed wagon has stopped.
            isWaiting = true;
        }
        else
        {
            // Followed wagon is moving.
            isWaiting = false;
        }
        lastWagonPos = curWagonPos;
        if (isWaiting)
        {
            return;
        }

        // The wagon vehicle is being moved to the next point of the "MoveToTarget" list
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed * velocity);
        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]);

            targetIndex += 1;
        }
         */
    }
}

public class WagonMover_2 : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f;

    GameObject WagonToFollow;

    Vector3 lastWagonPos = Vector3.zero;
    Vector3 curWagonPos = new Vector3(0, 0, 1);

    float velocity = 1f;

    void Start()
    {
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        List<GameObject> allGameObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(allGameObjects);

        // The object which is being followed is set to be the last object which was generated in the Unity scene.
        WagonToFollow = allGameObjects[allGameObjects.Count - 2];
    }

    void Update()
    {
        if (WagonToFollow == null)
        {
            gameObject.Destroy(); // If the followed wagon is destroyed (because it reached the endpoint) the following wagon will also destroy itself.
            return;
        }

        // Here we measure the distance to the wagon which is being followed.
        float distance = Vector3.Distance(WagonToFollow.transform.GetChild(0).GetChild(1).position, transform.GetChild(0).GetChild(0).position);
        if (Vector3.Distance(WagonToFollow.transform.position, transform.position) < 30)
        {
            velocity = .1f;
        }
        else if (distance > 20)
        {
            // If the distance to the followed wagon is too big, the following wagon will increase its speed.
            velocity = 1.2f;
        }
        else if (distance < 10)
        {
            // If the distance to the followed wagon is too small, the following wagon will decrease its speed.
            velocity = .8f;
        }
        else
        {
            // If the distance to the followed wagon is good, the speed of the following wagon will stay the same.
            velocity = 1f;
        }

        // Here we measure if the followed wagon has stopped. If this is the case, the following wagon will also stop
        // and move as sonn as the followed wagon start moving again.
        curWagonPos = WagonToFollow.transform.position;
        if (curWagonPos == lastWagonPos)
        {
            // Followed wagon has stopped.
            isWaiting = true;
        }
        else
        {
            // Followed wagon is moving.
            isWaiting = false;
        }
        lastWagonPos = curWagonPos;
        if (isWaiting)
        {
            return;
        }

        // The wagon vehicle is being moved to the next point of the "MoveToTarget" list
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed * velocity);
        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]);

            targetIndex += 1;
        }
        /*
        if (WagonToFollow == null)
        {
            gameObject.Destroy(); // If the followed wagon is destroyed (because it reached the endpoint) the following wagon will also destroy itself.
            return;
        }

        // Here we measure the distance to the wagon which is being followed.
        float distance = Vector3.Distance(WagonToFollow.transform.GetChild(0).GetChild(1).position, transform.GetChild(0).GetChild(0).position);
        if (Vector3.Distance(WagonToFollow.transform.position, transform.position) < 30)
        {
            velocity = .1f;
        }
        else if (distance > 20)
        {
            // If the distance to the followed wagon is too big, the following wagon will increase its speed.
            velocity = 1.2f;
        }
        else if (distance < 10)
        {
            // If the distance to the followed wagon is too small, the following wagon will decrease its speed.
            velocity = .8f;
        }
        else
        {
            // If the distance to the followed wagon is good, the speed of the following wagon will stay the same.
            velocity = 1f;
        }

        // Here we measure if the followed wagon has stopped. If this is the case, the following wagon will also stop
        // and move as sonn as the followed wagon start moving again.
        curWagonPos = WagonToFollow.transform.position;
        if (curWagonPos == lastWagonPos)
        {
            // Followed wagon has stopped.
            isWaiting = true;
        }
        else
        {
            // Followed wagon is moving.
            isWaiting = false;
        }
        lastWagonPos = curWagonPos;
        if (isWaiting)
        {
            return;
        }

        // The wagon vehicle is being moved to the next point of the "MoveToTarget" list
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed * velocity);
        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]);

            targetIndex += 1;
        }
         */
    }
}

public class WagonMover_3 : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f;

    GameObject WagonToFollow;

    Vector3 lastWagonPos = Vector3.zero;
    Vector3 curWagonPos = new Vector3(0, 0, 1);

    float velocity = 1f;

    void Start()
    {
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        List<GameObject> allGameObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(allGameObjects);

        // The object which is being followed is set to be the last object which was generated in the Unity scene.
        WagonToFollow = allGameObjects[allGameObjects.Count - 2];
    }

    void Update()
    {
        if (WagonToFollow == null)
        {
            gameObject.Destroy(); // If the followed wagon is destroyed (because it reached the endpoint) the following wagon will also destroy itself.
            return;
        }

        // Here we measure the distance to the wagon which is being followed.
        float distance = Vector3.Distance(WagonToFollow.transform.GetChild(0).GetChild(1).position, transform.GetChild(0).GetChild(0).position);
        if (Vector3.Distance(WagonToFollow.transform.position, transform.position) < 30)
        {
            velocity = .1f;
        }
        else if (distance > 20)
        {
            // If the distance to the followed wagon is too big, the following wagon will increase its speed.
            velocity = 1.2f;
        }
        else if (distance < 10)
        {
            // If the distance to the followed wagon is too small, the following wagon will decrease its speed.
            velocity = .8f;
        }
        else
        {
            // If the distance to the followed wagon is good, the speed of the following wagon will stay the same.
            velocity = 1f;
        }

        // Here we measure if the followed wagon has stopped. If this is the case, the following wagon will also stop
        // and move as sonn as the followed wagon start moving again.
        curWagonPos = WagonToFollow.transform.position;
        if (curWagonPos == lastWagonPos)
        {
            // Followed wagon has stopped.
            isWaiting = true;
        }
        else
        {
            // Followed wagon is moving.
            isWaiting = false;
        }
        lastWagonPos = curWagonPos;
        if (isWaiting)
        {
            return;
        }

        // The wagon vehicle is being moved to the next point of the "MoveToTarget" list
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed * velocity);
        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]);

            targetIndex += 1;
        }
        /*
        if (WagonToFollow == null)
        {
            gameObject.Destroy(); // If the followed wagon is destroyed (because it reached the endpoint) the following wagon will also destroy itself.
            return;
        }

        // Here we measure the distance to the wagon which is being followed.
        float distance = Vector3.Distance(WagonToFollow.transform.GetChild(0).GetChild(1).position, transform.GetChild(0).GetChild(0).position);
        if (Vector3.Distance(WagonToFollow.transform.position, transform.position) < 30)
        {
            velocity = .1f;
        }
        else if (distance > 20)
        {
            // If the distance to the followed wagon is too big, the following wagon will increase its speed.
            velocity = 1.2f;
        }
        else if (distance < 10)
        {
            // If the distance to the followed wagon is too small, the following wagon will decrease its speed.
            velocity = .8f;
        }
        else
        {
            // If the distance to the followed wagon is good, the speed of the following wagon will stay the same.
            velocity = 1f;
        }

        // Here we measure if the followed wagon has stopped. If this is the case, the following wagon will also stop
        // and move as sonn as the followed wagon start moving again.
        curWagonPos = WagonToFollow.transform.position;
        if (curWagonPos == lastWagonPos)
        {
            // Followed wagon has stopped.
            isWaiting = true;
        }
        else
        {
            // Followed wagon is moving.
            isWaiting = false;
        }
        lastWagonPos = curWagonPos;
        if (isWaiting)
        {
            return;
        }

        // The wagon vehicle is being moved to the next point of the "MoveToTarget" list
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed * velocity);
        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]);

            targetIndex += 1;
        }
         */
    }
}

/*
public class WagonMover_4 : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f;

    GameObject WagonToFollow;

    Vector3 lastWagonPos = Vector3.zero;
    Vector3 curWagonPos = new Vector3(0, 0, 1);

    float velocity = 1f;

    void Start()
    {
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        List<GameObject> allGameObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(allGameObjects);

        // The object which is being followed is set to be the last object which was generated in the Unity scene.
        WagonToFollow = allGameObjects[allGameObjects.Count - 2];
    }

    void Update()
    {
        if (WagonToFollow == null)
        {
            gameObject.Destroy(); // If the followed wagon is destroyed (because it reached the endpoint) the following wagon will also destroy itself.
            return;
        }

        // Here we measure the distance to the wagon which is being followed.
        float distance = Vector3.Distance(WagonToFollow.transform.GetChild(0).GetChild(1).position, transform.GetChild(0).GetChild(0).position);
        if (Vector3.Distance(WagonToFollow.transform.position, transform.position) < 30)
        {
            velocity = .1f;
        }
        else if (distance > 20)
        {
            // If the distance to the followed wagon is too big, the following wagon will increase its speed.
            velocity = 1.2f;
        }
        else if (distance < 10)
        {
            // If the distance to the followed wagon is too small, the following wagon will decrease its speed.
            velocity = .8f;
        }
        else
        {
            // If the distance to the followed wagon is good, the speed of the following wagon will stay the same.
            velocity = 1f;
        }

        // Here we measure if the followed wagon has stopped. If this is the case, the following wagon will also stop
        // and move as sonn as the followed wagon start moving again.
        curWagonPos = WagonToFollow.transform.position;
        if (curWagonPos == lastWagonPos)
        {
            // Followed wagon has stopped.
            isWaiting = true;
        }
        else
        {
            // Followed wagon is moving.
            isWaiting = false;
        }
        lastWagonPos = curWagonPos;
        if (isWaiting)
        {
            return;
        }

        // The wagon vehicle is being moved to the next point of the "MoveToTarget" list
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed * velocity);
        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]);

            targetIndex += 1;
        }
        /*
        if (WagonToFollow == null)
        {
            gameObject.Destroy(); // If the followed wagon is destroyed (because it reached the endpoint) the following wagon will also destroy itself.
            return;
        }

        // Here we measure the distance to the wagon which is being followed.
        float distance = Vector3.Distance(WagonToFollow.transform.GetChild(0).GetChild(1).position, transform.GetChild(0).GetChild(0).position);
        if (Vector3.Distance(WagonToFollow.transform.position, transform.position) < 30)
        {
            velocity = .1f;
        }
        else if (distance > 20)
        {
            // If the distance to the followed wagon is too big, the following wagon will increase its speed.
            velocity = 1.2f;
        }
        else if (distance < 10)
        {
            // If the distance to the followed wagon is too small, the following wagon will decrease its speed.
            velocity = .8f;
        }
        else
        {
            // If the distance to the followed wagon is good, the speed of the following wagon will stay the same.
            velocity = 1f;
        }

        // Here we measure if the followed wagon has stopped. If this is the case, the following wagon will also stop
        // and move as sonn as the followed wagon start moving again.
        curWagonPos = WagonToFollow.transform.position;
        if (curWagonPos == lastWagonPos)
        {
            // Followed wagon has stopped.
            isWaiting = true;
        }
        else
        {
            // Followed wagon is moving.
            isWaiting = false;
        }
        lastWagonPos = curWagonPos;
        if (isWaiting)
        {
            return;
        }

        // The wagon vehicle is being moved to the next point of the "MoveToTarget" list
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed * velocity);
        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]);

            targetIndex += 1;
        }
    }
}
*/

/*
public class WagonMover_5 : MonoBehaviour
{
    int targetIndex = 0; // Index of the next point which we are moving to.

    bool isWaiting = false;

    float VehicleMaxSpeed = 100f;

    GameObject WagonToFollow;

    Vector3 lastWagonPos = Vector3.zero;
    Vector3 curWagonPos = new Vector3(0, 0, 1);

    float velocity = 1f;

    void Start()
    {
        transform.position = SortWay.PathsInRightOrder[0][0]; // Starting point for the vehicle.

        List<GameObject> allGameObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(allGameObjects);

        // The object which is being followed is set to be the last object which was generated in the Unity scene.
        WagonToFollow = allGameObjects[allGameObjects.Count - 2];
    }

    void Update()
    {
        if (WagonToFollow == null)
        {
            gameObject.Destroy(); // If the followed wagon is destroyed (because it reached the endpoint) the following wagon will also destroy itself.
            return;
        }

        // Here we measure the distance to the wagon which is being followed.
        float distance = Vector3.Distance(WagonToFollow.transform.GetChild(0).GetChild(1).position, transform.GetChild(0).GetChild(0).position);
        if (Vector3.Distance(WagonToFollow.transform.position, transform.position) < 30)
        {
            velocity = .1f;
        }
        else if (distance > 20)
        {
            // If the distance to the followed wagon is too big, the following wagon will increase its speed.
            velocity = 1.2f;
        }
        else if (distance < 10)
        {
            // If the distance to the followed wagon is too small, the following wagon will decrease its speed.
            velocity = .8f;
        }
        else
        {
            // If the distance to the followed wagon is good, the speed of the following wagon will stay the same.
            velocity = 1f;
        }

        // Here we measure if the followed wagon has stopped. If this is the case, the following wagon will also stop
        // and move as sonn as the followed wagon start moving again.
        curWagonPos = WagonToFollow.transform.position;
        if (curWagonPos == lastWagonPos)
        {
            // Followed wagon has stopped.
            isWaiting = true;
        }
        else
        {
            // Followed wagon is moving.
            isWaiting = false;
        }
        lastWagonPos = curWagonPos;
        if (isWaiting)
        {
            return;
        }

        // The wagon vehicle is being moved to the next point of the "MoveToTarget" list
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed * velocity);
        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]);

            targetIndex += 1;
        }
        /*
        if (WagonToFollow == null)
        {
            gameObject.Destroy(); // If the followed wagon is destroyed (because it reached the endpoint) the following wagon will also destroy itself.
            return;
        }

        // Here we measure the distance to the wagon which is being followed.
        float distance = Vector3.Distance(WagonToFollow.transform.GetChild(0).GetChild(1).position, transform.GetChild(0).GetChild(0).position);
        if (Vector3.Distance(WagonToFollow.transform.position, transform.position) < 30)
        {
            velocity = .1f;
        }
        else if (distance > 20)
        {
            // If the distance to the followed wagon is too big, the following wagon will increase its speed.
            velocity = 1.2f;
        }
        else if (distance < 10)
        {
            // If the distance to the followed wagon is too small, the following wagon will decrease its speed.
            velocity = .8f;
        }
        else
        {
            // If the distance to the followed wagon is good, the speed of the following wagon will stay the same.
            velocity = 1f;
        }

        // Here we measure if the followed wagon has stopped. If this is the case, the following wagon will also stop
        // and move as sonn as the followed wagon start moving again.
        curWagonPos = WagonToFollow.transform.position;
        if (curWagonPos == lastWagonPos)
        {
            // Followed wagon has stopped.
            isWaiting = true;
        }
        else
        {
            // Followed wagon is moving.
            isWaiting = false;
        }
        lastWagonPos = curWagonPos;
        if (isWaiting)
        {
            return;
        }

        // The wagon vehicle is being moved to the next point of the "MoveToTarget" list
        transform.position = Vector3.MoveTowards(transform.position, SortWay.MoveToTarget[targetIndex], Time.deltaTime * VehicleMaxSpeed * velocity);
        if (transform.position == SortWay.MoveToTarget[targetIndex])
        {
            if (SortWay.PathLastNode.Contains(transform.position))
            {
                // If the road/railroad consists of more than one part we have to teleport the vehicle to the other part upon reaching.
                // the end of one part.
                int index = SortWay.MoveToTarget.IndexOf(transform.position);
                transform.position = SortWay.MoveToTarget[index + 1];
            }
            transform.LookAt(SortWay.MoveToTarget[targetIndex + 1]);

            targetIndex += 1;
        }
         
    }
}
*/