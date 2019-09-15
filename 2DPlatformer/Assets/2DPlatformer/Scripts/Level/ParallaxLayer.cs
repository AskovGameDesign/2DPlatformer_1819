using UnityEngine;

namespace Platformer.View
{
    /// <summary>
    /// Used to move a transform relative to the main camera position with a scale factor applied.
    /// This is used to implement parallax scrolling effects on different branches of gameobjects.
    /// </summary>
    public class ParallaxLayer : MonoBehaviour
    {
        /// <summary>
        /// Movement of the layer is scaled by this value.
        /// </summary>
        public Vector3 movementScale = Vector3.one;

        Transform _camera;
        Vector3 startOffset;

        void Awake()
        {
            _camera = Camera.main.transform;
            startOffset = transform.position - _camera.position;
        }

        void LateUpdate()
        {
            Vector3 movement = Vector3.Scale(_camera.position + startOffset, movementScale);
            transform.position = new Vector3(movement.x, movement.y, transform.position.z);
        }

    }
}