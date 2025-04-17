using UnityEngine;

public class EnemyJump : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //트리거 충돌 시 점프
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("JumpPoint"))
        {
            var jumpPoint = other.GetComponent<JumpPoint>();
            if (jumpPoint == null) return;

            // 점프포인트와 몬스터의 상대적인 방향(=점프포인트 방향) 판단
            float directionToJumpPoint = other.transform.position.x - transform.position.x;

            // y축 속도를 초기화해 점프 속도에 기존 속도가 누적되는 현상을 방지
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

            // 점프포인트 방향에 따른 점프 가능 여부 판단 후 점프
            switch (jumpPoint.allowedDirection)
            {
                case JumpPoint.AllowedDirection.Both:
                    rb.AddForce(Vector2.up.normalized * jumpPoint.jumpForce, ForceMode2D.Impulse);
                    break;
                case JumpPoint.AllowedDirection.LeftOnly:
                    if (directionToJumpPoint > 0)
                    {
                        rb.AddForce(Vector2.up.normalized * jumpPoint.jumpForce, ForceMode2D.Impulse);
                    }
                    break;
                case JumpPoint.AllowedDirection.RightOnly:
                    if (directionToJumpPoint < 0)
                    {
                        rb.AddForce(Vector2.up.normalized * jumpPoint.jumpForce, ForceMode2D.Impulse);
                    }
                    break;
            }

        }
    }
}
