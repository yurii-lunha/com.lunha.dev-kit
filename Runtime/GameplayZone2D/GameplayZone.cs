using System;
using UnityEngine;

namespace Lunha.DevKit.GameplayZone2D
{
    /// <summary>
    ///     Use for landscape only
    /// </summary>
    [DefaultExecutionOrder(-2)]
    public class GameplayZone : MonoBehaviour
    {
        /// <summary>
        ///     Aspect of 4:3 ratio
        /// </summary>
        private const float Aspect = 1.33f;

        private const float Orthographic = 5f;
        private const float OrthographicWide = 4f;

        [Tooltip("By default it will get the main camera")]
        public Camera targetCamera;

        [Space, Tooltip("Max. aspect ratio width")]
        public float aspectWidth = 21f;

        [Tooltip("Max. aspect ratio height")]
        public float aspectHeight = 9f;

        [Space, SerializeField, Tooltip("4:3")]
        private float orthographicAdd;

        [SerializeField, Tooltip("21:9 ++")]
        private float orthographicWideAdd;

        [Space, SerializeField, Tooltip("16:10 ~~")]
        private float orthographicMiddleAdd;

        [Space, SerializeField, Tooltip("False to draw gizmos only when the camera is selected")]
        private bool drawAlways;

        [Space, SerializeField]
        private bool drawGreenZone = true;

        [SerializeField]
        private bool draw43Zone = true;

        [SerializeField]
        private bool drawWideZone = true;

        // Colors
        private readonly Color _color43 = new Color(0.8f, 0.37f, 0f);
        private readonly Color _colorGreenZone = new Color(0.13f, 0.8f, 0f);
        private readonly Color _colorWide = new Color(0.82f, 0.02f, 1f);

        /// <summary>
        ///     Wide size
        /// </summary>
        private float _aspectWide = 2.33f;

        private void Awake()
        {
            if (!targetCamera)
            {
                targetCamera = Camera.main;
            }

            if (!targetCamera)
            {
                throw new NullReferenceException("Camera not found!");
            }

            _aspectWide = aspectWidth / aspectHeight;

            // Calculate and set orthographic size
            AutoArrangeCamera();
        }

        public static event Action<float> OrthographicSizeChanged;

        /// <summary>
        ///     Calculate and set orthographic size
        /// </summary>
        private void AutoArrangeCamera()
        {
            if (!targetCamera)
            {
                targetCamera = Camera.main;
            }

            var aspect = targetCamera.aspect;

            if (aspect > 1.61f)
            {
                targetCamera.orthographicSize = OrthographicWide + orthographicWideAdd;
            }
            else if (aspect > 1.59f && aspect < 1.61f)
            {
                targetCamera.orthographicSize = OrthographicWide + orthographicMiddleAdd + .16f;
            }
            else
            {
                targetCamera.orthographicSize = Orthographic + orthographicAdd;
            }

            OrthographicSizeChanged?.Invoke(targetCamera.orthographicSize);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!drawAlways)
            {
                return;
            }

            DrawGizmos();
        }

        private void OnDrawGizmosSelected()
        {
            if (drawAlways)
            {
                return;
            }

            DrawGizmos();
        }

        private void DrawGizmos()
        {
            _aspectWide = aspectWidth / aspectHeight;
            AutoArrangeCamera();
            DrawGameplayZone();
        }

        private void DrawGameplayZone()
        {
            var cameraPosition = targetCamera.transform.position;

            var height = Orthographic + orthographicAdd;
            var width = (Orthographic + orthographicAdd) * Aspect;

            var heightWide = OrthographicWide + orthographicWideAdd;
            var widthWide = (OrthographicWide + orthographicWideAdd) * _aspectWide;

            // 4:3
            if (draw43Zone)
            {
                DrawSquare(height, width, _color43, cameraPosition);
            }

            // wides
            if (drawWideZone)
            {
                DrawSquare(heightWide, widthWide, _colorWide, cameraPosition);
            }

            // green zone
            if (drawGreenZone)
            {
                DrawSquare(heightWide, width, _colorGreenZone, cameraPosition);
            }
        }

        private static void DrawSquare(float h, float w, Color color, Vector3 cameraPosition)
        {
            Gizmos.color = color;

            Gizmos.DrawLine(new Vector3(cameraPosition.x - w, cameraPosition.y + h),
                new Vector3(cameraPosition.x + w, cameraPosition.y + h));

            Gizmos.DrawLine(new Vector3(cameraPosition.x - w, cameraPosition.y - h),
                new Vector3(cameraPosition.x + w, cameraPosition.y - h));

            Gizmos.DrawLine(new Vector3(cameraPosition.x - w, cameraPosition.y - h),
                new Vector3(cameraPosition.x - w, cameraPosition.y + h));

            Gizmos.DrawLine(new Vector3(cameraPosition.x + w, cameraPosition.y - h),
                new Vector3(cameraPosition.x + w, cameraPosition.y + h));
        }
#endif
    }
}