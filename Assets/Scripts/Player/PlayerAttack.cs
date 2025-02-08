using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool isAttacking = false;  // Tarkistaa, onko pelaaja hyökkäämässä
    private bool canAttack = true;  // Tarkistaa, voiko pelaaja hyökätä
    public float projectileSpeed = 10f;


    void Update()
    {
        // Hyökkää, jos ollaan idle-tilassa ja hyökkäykselle ei ole estettä
        if (Input.GetButton("Fire1") && PlayerSettings.State == "Idle" && canAttack)
        {
            StartAttack();
        }

        // Jos pelaaja on hyökkäämässä, tarkistetaan, onko animaatio päättynyt
        if (isAttacking)
        {          
            // Tarkistetaan, että animaatio on päättynyt
            AnimatorStateInfo stateInfo = PlayerSettings.animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 1f) // Animaatio päättyy
            {
                FinishAttacking();
            }
        }
    }

    // Hyökkäyksen aloitus
    private void StartAttack()
    {
        PlayerSettings.State = "Attacking";
        isAttacking = true;
        canAttack = false; // Estetään uusi hyökkäys ennen edellisen loppumista
        string attackNumber = Random.Range(1, 5).ToString();  // Valitsee satunnaisen hyökkäyksen
        PlayerSettings.animator.Play("Attack" + attackNumber);
    }

    // Hyökkäyksen lopetus
    private void FinishAttacking()
    {
        isAttacking = false;
        PlayerSettings.State = "Idle";
        PlayerSettings.animator.Play("Idle");

        // Hyökkäys on valmis, voidaan taas hyökätä
        canAttack = true;
    }

    // Hyökkäys
    public void Attack()
    {
        // Luodaan ammus oikeassa paikassa, ja asetetaan alkuperäinen rotatio suoraan eteenpäin
        GameObject projectile = Instantiate(PlayerSettings.Attack, transform.position + transform.forward * 2f + transform.up * 1.5f, Quaternion.identity);  // Siirretään vielä enemmän eteen ja ylöspäin

        // Hakee Rigidbody-komponentin ammuksesta
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Asetetaan ammuksen nopeus
            Vector3 attackDirection = transform.forward * projectileSpeed;  // Käytetään säädettyä nopeutta

            // Käytetään linearVelocitya liikkumiseen
            rb.linearVelocity = attackDirection;

            // Piirretään viiva ammuksen suunnassa pelaajan edessä (1 sekunnin ajaksi)
            Debug.DrawRay(projectile.transform.position, attackDirection * 5f, Color.red, 1f);

            // Asetetaan ammuksen rotatio (niin että se lentää oikeaan suuntaan)
            projectile.transform.rotation = Quaternion.LookRotation(attackDirection);
        }

        // Ammus tuhoutuu 5 sekunnin kuluttua
        Destroy(projectile, 5f);
    }
}