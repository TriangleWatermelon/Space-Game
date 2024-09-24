using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    Camera playerCam;
    void Start(){
        playerCam = Camera.main;
    }

    void Update(){
        // Determine which direction to rotate towards
        Vector3 targetDirection = playerCam.transform.position - transform.position;

        // The step size is equal to speed times frame time.
        float singleStep = Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
