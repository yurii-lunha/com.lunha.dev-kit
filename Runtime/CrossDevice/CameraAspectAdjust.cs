#if CINEMACHINE
using Cinemachine;
#endif

using UnityEngine;

namespace Lunha.DevKit.CrossDevice
{
    public class CameraAspectAdjust : MonoBehaviour
    {
        [SerializeField] Camera _targetCamera;

#if CINEMACHINE
        [SerializeField] CinemachineVirtualCamera _targetVirtualCamera;
#endif

        [SerializeField] float _safeArea = 0.0905f;

        float _baseFOV = 60.0f;

        void Start()
        {
            _baseFOV = GetBaseFov();
            AdjustCameraFOV();
        }

        float GetBaseFov()
        {
#if CINEMACHINE
            if (_targetVirtualCamera)
            {
                return _targetVirtualCamera.m_Lens.FieldOfView;
            }
#endif

            if (_targetCamera)
            {
                return _targetCamera.fieldOfView;
            }

            return 60f;
        }

        [ContextMenu("AdjustCameraFOV")]
        void AdjustCameraFOV()
        {
            var fov = CalculateFOV();

#if CINEMACHINE
            if (_targetVirtualCamera)
            {
                _targetVirtualCamera.m_Lens.FieldOfView = fov;
                return;
            }
#endif

            if (_targetCamera)
            {
                _targetCamera.fieldOfView = fov;
            }
        }

        float CalculateFOV()
        {
            var safeRect = Screen.safeArea;
            var aspectRatio = safeRect.size.x / safeRect.size.y;
            aspectRatio *= _safeArea;

            return _baseFOV * (16.0f / 9.0f) / aspectRatio;
        }
    }
}