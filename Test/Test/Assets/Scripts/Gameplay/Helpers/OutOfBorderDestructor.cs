using UnityEngine;

namespace Gameplay.Helpers
{
    public class OutOfBorderDestructor : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _representation;

        void Update()
        {
            CheckBorders();
        }

        //Уничтожает объект, если он находится вне поля зрения камеры
        private void CheckBorders()
        {
            if (!GameAreaHelper.IsInGameplayArea(transform, _representation.bounds))
            {
                Destroy(gameObject);
            }
        }
    }
}
