using UnityEngine;
namespace OurNamespace
{
    public class Box
    {
        /// <summary>
        /// Muestra la caja con los parametros que se le dan
        /// </summary>
        public static void ShowBox(Vector3 _boxSize, Vector3 _boxOffSet, Transform _spawnTransform)
        {
            //la caja cambia según la rotación del objeto(para más info buscar el operador ?:)
            int _direction = _spawnTransform.rotation.y == 0 ? 1 : -1;

            //pintado de la caja
            Debug.DrawRay(_spawnTransform.position - _boxSize + new Vector3(_boxOffSet.x * _direction, _boxOffSet.y), Vector2.right);
            Debug.DrawRay(_spawnTransform.position + _boxSize + new Vector3(_boxOffSet.x * _direction, _boxOffSet.y), Vector2.left);
            Debug.DrawRay(_spawnTransform.position - _boxSize + new Vector3(_boxOffSet.x * _direction, _boxOffSet.y), Vector2.up);
            Debug.DrawRay(_spawnTransform.position + _boxSize + new Vector3(_boxOffSet.x * _direction, _boxOffSet.y), Vector2.down);
        }

        /// <summary>
        /// Devuelve si detecta lo que se le ha pedido o no
        /// </summary>     
        public static bool DetectSomethingBox(Vector3 _boxSize, Vector3 _boxOffSet, Transform _spawnTransform, LayerMask _layerToFliter)
        {
            //la caja cambia según la rotación del objeto(para más info buscar el operador ?:)
            int _direction = _spawnTransform.rotation.y == 0 ? 1 : -1;

            Collider2D _colliderResult = Physics2D.OverlapArea(_spawnTransform.position - _boxSize + new Vector3(_boxOffSet.x * _direction, _boxOffSet.y) //punto 1 de la caja
                                          , _spawnTransform.position + _boxSize + new Vector3(_boxOffSet.x * _direction, _boxOffSet.y), //punto 2 de la caja
                                           _layerToFliter);//capa que filtra la detección
            return _colliderResult != null;
        }
    }
}
