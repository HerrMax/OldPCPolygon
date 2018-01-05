using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour {

    [SerializeField] private float recoilControlToVertical;
    [SerializeField] private float recoilControlToHorizontal;

    [SerializeField] private float maxVerticalRot;
    [SerializeField] private float maxHorizontalRot;

    private float verticalRot;
    private float horizontalRot;

    void Start () {
		
	}
	
	void Update () {
        RecoilControl();
        //transform.localEulerAngles = new Vector3(-verticalRot/100,horizontalRot/100,0);
        transform.localEulerAngles = new Vector3(-verticalRot/2, horizontalRot/2, 0);
    }

    public void ApplyVertKick(int recoilToVertical)
    {
        verticalRot += recoilToVertical;
    }

    public void ApplyHorizKick(int recoilToHorizontal)
    {
        horizontalRot += Random.Range(-recoilToHorizontal, recoilToHorizontal);
    }

    public void ApplyAdditionalKickToSidee(int additionalRecoilToSide)
    {
        horizontalRot += additionalRecoilToSide / 2;
    }

    public void RecoilControl()
    {
        if (verticalRot > 0)
        {
            verticalRot -= recoilControlToVertical * Time.deltaTime * 3;
        }

        if (horizontalRot > 0)
        {
            horizontalRot -= recoilControlToHorizontal * Time.deltaTime * 3;
        }

        if (horizontalRot < 0)
        {
            horizontalRot += recoilControlToHorizontal * Time.deltaTime * 3;
        }

        if (verticalRot > maxVerticalRot)
        {
            verticalRot = maxVerticalRot + Random.Range(-1, 2);
        }

        if (horizontalRot > maxHorizontalRot)
        {
            horizontalRot = maxHorizontalRot + Random.Range(-1, 1); ;
        }
    }
}
