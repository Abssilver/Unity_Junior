using UnityEngine;

namespace Gameplay.Helpers
{
    public static class GameAreaHelper
    {

        private static Camera _camera;

        static GameAreaHelper()
        {
            _camera = Camera.main;
        }
        //Возвращает false, если объект пересек поле зрения камеры
        public static bool IsInGameplayArea(Transform objectTransform, Bounds objectBounds)
        {
            var camHalfHeight = _camera.orthographicSize;
            var camHalfWidth = camHalfHeight * _camera.aspect;
            var camPos = _camera.transform.position;
            var topBound = camPos.y + camHalfHeight;
            var bottomBound = camPos.y - camHalfHeight;
            var leftBound = camPos.x - camHalfWidth;
            var rightBound = camPos.x + camHalfWidth;

            var objectPos = objectTransform.position;

            return (objectPos.x - objectBounds.extents.x < rightBound)
                && (objectPos.x + objectBounds.extents.x > leftBound)
                && (objectPos.y - objectBounds.extents.y < topBound)
                && (objectPos.y + objectBounds.extents.y > bottomBound);

        }
        //проверка на выход корабля игрока за пределы видимости камеры. 
        //Если дальнейшее движение приведет к выходу за экран - возвращаем false
        public static bool IsAvailableForLateralMovement(Transform objectTransform, Bounds objectBounds, float direction)
        {
            var camHalfHeight = _camera.orthographicSize;
            var camHalfWidth = camHalfHeight * _camera.aspect;
            var camPos = _camera.transform.position;
            var leftBound = camPos.x - camHalfWidth + objectBounds.extents.x;
            var rightBound = camPos.x + camHalfWidth - objectBounds.extents.x;
            //ограничиваем позицию игрока (без этого иногда игрок вылетает чуть дальше)
            var Xpos = Mathf.Clamp(objectTransform.position.x, leftBound, rightBound);
            var objectPos = objectTransform.position;
            objectPos.x = Xpos;
            objectTransform.position = objectPos;

            return direction == 0 ? true :
                direction < 0 ? objectPos.x > leftBound :
                objectPos.x < rightBound;
        }
    }
}
