using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleSonic : MonoBehaviour
{
    //variáveis criadas no escopo da classe, são chamadas de campos(fields)
    public LayerMask layerMascara;//para quais layer eu vou verificar a colisao
    public Vector3 diferenca;
    Rigidbody2D rb;
    Animator animator;
    const float RAIO=0.05f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float horz = Input.GetAxis("Horizontal");

        if (horz != 0)
        {
            GetComponent<Animator>().SetBool("CORRENDO", true);
            transform.Translate(0.75f * Time.deltaTime * horz, 0, 0);//faz o personagem andar
            if (horz < 0)
                transform.localScale = new Vector3(-1, 1, 1);//vira a sprite
            else
                transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            GetComponent<Animator>().SetBool("CORRENDO", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0, 3f), ForceMode2D.Impulse);
            animator.SetTrigger("PULAR");
            animator.SetBool("NOCHAO", false);
        }


    }

    private void FixedUpdate()
    {

            Collider2D[] colisoes = Physics2D.OverlapCircleAll(transform.position - diferenca,
                                                                RAIO,
                                                                layerMascara);
            if (colisoes.Length == 0)
                animator.SetBool("NOCHAO", false);
            else
                animator.SetBool("NOCHAO", true);

       
        
    }
    //Isso aqui é só para debug!!!!
    //void OnDrawGizmos()//desenha o tempo inteiro
    void OnDrawGizmosSelected()//desenha apenas quando o gameObject for selecionado na hierarquia
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position - diferenca,RAIO);
    }

    //tosquera: Vamos acertar isso na segunda parte da aula
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     GetComponent<Animator>().SetBool("NOCHAO", true);
    // }


}
