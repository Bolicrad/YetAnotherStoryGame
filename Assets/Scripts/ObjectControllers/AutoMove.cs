using UnityEngine;

namespace ObjectControllers
{
    public class AutoMove : MonoBehaviour
    {
        public float moveSpeed = 5f; // 移动速度
        public float moveDistance = 10f; // 移动距离

        private Vector3 _startPosition; // 初始位置
        private Vector3 _endPosition; // 结束位置
        private bool _isMovingToEnd = true; // 是否向结束位置移动

        void Start()
        {
            _startPosition = transform.position;
            _endPosition = _startPosition + Vector3.forward * moveDistance;
        }

        void Update()
        {
            // 根据当前移动方向和速度移动物体
            transform.position += (_isMovingToEnd ? Vector3.forward : Vector3.back) * (moveSpeed * Time.deltaTime);

            // 如果到达结束位置，改变移动方向
            if (_isMovingToEnd && transform.position.z >= _endPosition.z)
            {
                _isMovingToEnd = false;
            }
            // 如果回到初始位置，改变移动方向
            else if (!_isMovingToEnd && transform.position.z <= _startPosition.z)
            {
                _isMovingToEnd = true;
            }
        }
    }
}