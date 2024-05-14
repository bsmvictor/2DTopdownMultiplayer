using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 5.0f;  // Velocidade de movimento do jogador
    [SerializeField] private Rigidbody2D rb;         // Componente Rigidbody2D do objeto
    [SerializeField] private Vector2 moveInput;      // Entrada do jogador transformada em vetor de movimento
    [SerializeField] private PlayerAnimator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Obtém o componente Rigidbody2D
        anim = GetComponent<PlayerAnimator>();
    }


    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        // Lê as entradas do teclado nas direções horizontal e vertical
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();  // Normaliza o vetor para garantir movimento uniforme em todas as direções

        SpriteFlip(moveInput.x);

        // Aplica o vetor de movimento ao Rigidbody2D para mover o objeto
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    void SpriteFlip(float horizontal)
    {
        // Obtém a escala atual do jogador
        Vector3 playerScale = transform.localScale;

        if (horizontal > 0)
        {
            // Define a escala do jogador diretamente
            transform.localScale = new Vector3(Mathf.Abs(playerScale.x), playerScale.y, playerScale.z);
        }
        else if (horizontal < 0)
        {
            // Inverte a escala do jogador na direção horizontal
            transform.localScale = new Vector3(-Mathf.Abs(playerScale.x), playerScale.y, playerScale.z);
        }
    }

}
