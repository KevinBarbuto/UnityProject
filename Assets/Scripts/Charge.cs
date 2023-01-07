using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : MonoBehaviour
{
    public float ChargeValue;
    public static float CoulombConstant = 8.99f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var coulomb = GetForce();
        var rb = GetComponent<Rigidbody>();
        rb.AddForce(coulomb);
    }

    Vector3 GetForce()
    {
        var chargedObjects = GetChargedObjects();
        var resultVector = new Vector3();
        for (var i = 0; i < chargedObjects.Length; i++) {
            var co = chargedObjects[i];
            if ( co == this) {
                continue;
            }
            // do some calc for thisValue
            var dis = GetDistances(co);
            var mag = CoulombConstant * (ChargeValue * co.ChargeValue) / (dis*dis);
            Debug.Log("how big is this number?: " + mag);
            var thisValue = GetDirection(co) * mag;
            resultVector = resultVector + thisValue;
        }
        return resultVector;
    }

    Vector3 GetDirection(Charge c)
    {
        var DisplacementVector = transform.position - c.transform.position;
        return DisplacementVector.normalized;
    }
    float GetDistances(Charge c)
    {
        float Distance = Vector3.Distance(c.transform.position, transform.position);
        return Distance;
    }

    Charge[] GetChargedObjects() 
    {
        return FindObjectsOfType<Charge>();
    }
}
