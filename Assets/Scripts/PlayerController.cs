using System;
using UnityEngine;

namespace Shmup
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float smoothness = 0.1f;
        [SerializeField] private float leanAngle = 15f;
        [SerializeField] private float leanSpeed = 5f;

        [SerializeField] private GameObject model;

        [Header("Camera Bounds")]
        [SerializeField] private Transform cameraFollow;
        [SerializeField] private float minX = -8f;
        [SerializeField] private float maxX = 8f;
        [SerializeField] private float minY = -4f;
        [SerializeField] private float maxY = 4f;

        private InputReader input;

        private Vector3 currentVelocity;
        private Vector3 targetPosition;

        private void Start()
        {
            input = GetComponent<InputReader>();
        }

        private void Update()
        {
            targetPosition += new Vector3(input.Move.x, input.Move.y, 0f) * (speed * Time.deltaTime);
            
            // Calculate the min and max X and Y positions for the player based on the camera view
            var minPlayerX = cameraFollow.position.x + minX;
            var maxPlayerX = cameraFollow.position.x + maxX;
            var minPlayerY = cameraFollow.position.y + minY;
            var maxPlayerY = cameraFollow.position.y + maxY;
            
            // Clamp the player's position to the camera view
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPlayerX, maxPlayerX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPlayerY, maxPlayerY);
            
            // Lerp the player's position to the target position
            transform.position =
                Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothness);
            
            // Calculate the rotation effect
            var targetRotationAngle = -input.Move.x * leanAngle;

            var currentYRotation = transform.localEulerAngles.y;
            var newYRotation = Mathf.LerpAngle(currentYRotation, targetRotationAngle, leanSpeed * Time.deltaTime);
            
            // Apply the rotation effect
            transform.localEulerAngles = new Vector3(0f, newYRotation, 0f);
        }
    }
}
