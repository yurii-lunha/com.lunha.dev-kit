#if CINEMACHINE
#if UNITY_6000
using Unity.Cinemachine;
#else
using Cinemachine;
#endif
#endif

using UnityEngine;

namespace Lunha.DevKit.CrossDevice
{
    public class CameraAspectAdjust : MonoBehaviour
    {
        [SerializeField] Camera _targetCamera;

#if CINEMACHINE
#if UNITY_6000
        [SerializeField] CinemachineCamera _targetVirtualCamera;

        float CinemachineFieldOfView
        {
            get => _targetVirtualCamera.Lens.FieldOfView;
            set => _targetVirtualCamera.Lens.FieldOfView = value;
        }
#else
        [SerializeField] CinemachineVirtualCamera _targetVirtualCamera;

        float CinemachineFieldOfView
        {
            get => _targetVirtualCamera.m_Lens.FieldOfView;
            set => _targetVirtualCamera.m_Lens.FieldOfView = value;
        }
#endif
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
                return CinemachineFieldOfView;
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
                CinemachineFieldOfView = fov;
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