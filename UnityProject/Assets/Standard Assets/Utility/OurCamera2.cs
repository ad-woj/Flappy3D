using UnityEngine;

namespace UnityStandardAssets.Utility
{
    public class OurCamera2 : MonoBehaviour
    {

        // The target we are following
        [SerializeField]
        private Transform target;
        // The distance in the x-z plane to the target
        [SerializeField]
        private float distance = 10.0f;
        // the height we want the camera to be above the target
        [SerializeField]
        private float height = 1.0f;

        [SerializeField]
        private float rotationDamping;
        [SerializeField]
        private float heightDamping = 0.1f;

        private float positionX = 0.0f;
        private float positionZ = 10.0f;
        private int counter = 0;

        private Vector3 cameraPosition;

        private bool targetPositionReached = false;

        private bool sideView = false;


        // Use this for initialization
        void Start() { }

        // Update is called once per frame
        void LateUpdate()
        {
            // Early out if we don't have a target
            if (!target)
                return;

            // Calculate the current rotation angles
            var wantedRotationAngle = target.eulerAngles.y;
            var wantedHeight = target.position.y + height;

            var currentRotationAngle = transform.eulerAngles.y;
            var currentHeight = transform.position.y;

            // Damp the rotation around the y-axis
            currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

            // Convert the angle into a rotation
            var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            transform.position = target.position;
            //transform.position -= currentRotation * Vector3.forward * distance;

            //if (!targetPositionReached)
            //{
            //    if (counter > 100)
            //    {
            //        positionX += 0.015f;
            //        positionZ += 0.015f;
            //    }
            //    else
            //        counter++;

            //    if (positionX >= 6.0f)
            //        positionX = 6.0f;

            //    if (positionZ >= 14.0f)
            //        positionZ = 14.0f;

            //    if (positionX >= 6.0f && positionZ >= 14.0f)
            //        targetPositionReached = true; // camera is in target place, end of checking conditions above
            //}

            if (Input.GetKeyDown(KeyCode.V))
            {
                if (!sideView)
                { // Forcing view same as in original Flappy - from side
                    targetPositionReached = true;
                    positionX = 70f;
                    positionZ = 0f;
                    sideView = true;
                } else
                {  // Forcing our standard view
                    positionX = 6.0f;
                    positionZ = 14.0f;
                    sideView = false;
                }
            }

            // Set the height of the camera
            //transform.position = new Vector3(target.position.x + positionX, wantedHeight, transform.position.z);
            transform.position = new Vector3(target.position.x + positionX, wantedHeight, target.position.z - positionZ);

            // Always look at the target
            transform.LookAt(target);
        }
    }
}
