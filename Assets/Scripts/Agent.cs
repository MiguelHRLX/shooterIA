using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Agent : MonoBehaviour
{
    public int healt=100;
    public int damage=10;
    public float shootRange = 80f;
    public float shootRate = 1f;
    public float shootTimer;
    public float velocity;
    public Transform shootOrigin;
    [SerializeReference] LineRenderer shootLine;
    RaycastHit hit;
    private void Start()
    {
        shootLine.enabled = false;
    }
    public void Shot()
    {
        Vector3 realShoot = Variation()*shootOrigin.right;
        if (Physics.Raycast(shootOrigin.position, realShoot, out hit, shootRange))
        {
            shootLine.SetPosition(0, shootOrigin.position);
            shootLine.SetPosition(1, hit.point);
            if (hit.transform.GetComponent<Agent>())
            {
                
                Agent other=hit.transform.GetComponent<Agent>();
                Damage(other);
                
            }
        }
        else
        {
            shootLine.SetPosition(0, shootOrigin.position);
            shootLine.SetPosition(1, shootOrigin.position + (realShoot*shootRange));
        }
        StartCoroutine(VisibilityShoot());
    }
    public void Damage(Agent agent)
    {
        if (agent.healt - damage >= 0) agent.healt -= damage;
        else agent.healt = 0;
        Debug.Log("le di a " + agent.name + ": " + agent.healt + "/100");
    }

    public IEnumerator VisibilityShoot()
    {
        shootLine.enabled = true;
        yield return new WaitForSeconds(0.1f);
        shootLine.enabled = false;
    }
    private Quaternion Variation()
    {
        float angleVariation = 2.5f;
        return Quaternion.Euler(0, Random.Range(-angleVariation, angleVariation), 0);
    }

}