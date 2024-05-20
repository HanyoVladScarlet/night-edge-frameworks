
using UnityEngine;
using NightEdgeFrameworks.Models.Rts;


namespace NightEdgeFrameworks.Controllers
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private float _cameraSpeed = 5;
        [SerializeField]
        private float _cameraScrollSpeed = 5;
        [SerializeField]
        private float _cameraHeight = 20;
        [SerializeField]
        private float _cameraRotation = 60;

        [SerializeField]
        private CursorModel _cursor;

        // This is referred to the root game object MAIN CAMERA bound onto.
        private GameObject _cameraSet;
        private float _cameraSpeedBase = 7.5f;
        private float _cameraScrollSpeedBase = 60;
        private Cinemachine.CinemachineVirtualCamera _camera;

        private bool BeginScrollView => Input.GetKeyDown(KeyCode.Mouse2);
        private bool EndScrollView => Input.GetKeyUp(KeyCode.Mouse2);
        private bool ScrollingView => Input.GetKey(KeyCode.Mouse2);


        private void Awake()
        {
            _cursor = NightEdgeFrameworks.Core.Simulation.GetModel<CursorModel>();
            _cameraSet = this.gameObject;
        }

        private void Start()
        {
            if (_cursor == null) Debug.LogError("No IN-GAME-CURSOR found!");
        }

        private void Update()
        {
            TranslateOnBound();
            TranslateOnScroll();
            ResetCameraPosition();
        }

        private void ResetCameraPosition()
        {
            // TODO: Reset camera to the respawn point of player.
            if (Input.GetKeyUp(KeyCode.H)) _cameraSet.transform.position = Vector3.zero;
        }


        /// <summary>
        /// Translate Camera when cursor is on bound.
        /// </summary>
        private void TranslateOnBound()
        {
            var horizontal = ((_cursor.CursorOnBound & CursorBoundingStatus.Right)
                    == CursorBoundingStatus.Right ? 1 : 0)
                        - ((_cursor.CursorOnBound & CursorBoundingStatus.Left)
                            == CursorBoundingStatus.Left ? 1 : 0);
            var vertical = ((_cursor.CursorOnBound & CursorBoundingStatus.Up)
                == CursorBoundingStatus.Up ? 1 : 0)
                    - ((_cursor.CursorOnBound & CursorBoundingStatus.Down)
                        == CursorBoundingStatus.Down ? 1 : 0);

            _cameraSet.transform.position += new Vector3(horizontal, 0, vertical) * Time.deltaTime
                * _cameraSpeedBase * _cameraSpeed;
        }

        /// <summary>
        /// Translate MAIN-CAMERA when dragging cursor with right mouse down.
        /// Not active while cursor is on bound.
        /// </summary>
        private void TranslateOnScroll()
        {
            if (BeginScrollView) _cursor.IsPositionLocked = true;
            if (EndScrollView) _cursor.IsPositionLocked = false;
            if (_cursor.CursorOnBound == CursorBoundingStatus.None && ScrollingView)
                _cameraSet.transform.position -= new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"))
                     * _cameraScrollSpeed * _cameraScrollSpeedBase * Time.deltaTime;
        }
    }
}


