using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 5f;

    public Vector3 input;

    [SerializeField] private GameObject unitPrefab;
    [SerializeField] private List<bool> actualTeam = new List<bool>();
    [SerializeField] private GameObject[] unitsPosiblePositon = new GameObject[8];
    [SerializeField] private List<bool> positionOcupated = new List<bool>();
    [SerializeField] private int backUpTeam = 0;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        for(var i =0; i<8; i++)
        {
            positionOcupated.Add(false);
        }

        actualTeam.Add(true);
        positionOcupated[0] = true;
        GameObject soldier = Instantiate(unitPrefab, unitsPosiblePositon[0].transform.position, Quaternion.identity);
        soldier.transform.parent = unitsPosiblePositon[0].transform;
        unitsPosiblePositon[0].SetActive(true);
    }


    void AddUnitsToTeam(int units)
    {
        for(var i =0; i<units; i++)
        {
            if(actualTeam.Count < 8)
            {
                actualTeam.Add(true);

                var notOnTeam = true;
                for (var j = 0; j < positionOcupated.Count; j++)
                {
                    if (!positionOcupated[j] && notOnTeam)
                    {
                        positionOcupated[j] = true;
                        GameObject soldier = Instantiate(unitPrefab, unitsPosiblePositon[j].transform.position, Quaternion.identity);
                        soldier.transform.parent = unitsPosiblePositon[j].transform;
                        notOnTeam = false;
                        unitsPosiblePositon[j].SetActive(true);
                    }
                }
            }
            else
            {
                if(backUpTeam < 10)
                {
                    backUpTeam++;
                }
                else
                {
                    Debug.Log("Buff o algo en el futuro");
                }
            
            }
        }
    }

    public void RemoveUnitOfTheTeam(GameObject positionGameObject)
    {
        Debug.Log(positionGameObject.name);
        //tengo el nombre y el gameobject... cortando el nombre y agarrando el ultimo caracter tengo la posicion +1 del gameobject para asi poder buscarlo y borrarlo
        /*if (actualTeam.Count < 8)
        {
            actualTeam.Add(true);

            var notOnTeam = true;
            for (var j = 0; j < positionOcupated.Count; j++)
            {
                if (!positionOcupated[j] && notOnTeam)
                {
                    positionOcupated[j] = true;
                    GameObject soldier = Instantiate(unitPrefab, unitsPosiblePositon[j].transform.position, Quaternion.identity);
                    soldier.transform.parent = unitsPosiblePositon[j].transform;
                    notOnTeam = false;
                }
            }
        }
        else
        {
            if (backUpTeam < 10)
            {
                backUpTeam++;
            }
            else
            {
                Debug.Log("Buff o algo en el futuro");
            }

        }*/
    }

    void GatherInput()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private void Update()
    {
        GatherInput();
        if (Input.GetKeyDown("space"))
        {
            AddUnitsToTeam(1);
        }


        if(actualTeam.Count <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Perdiste");
    }

    void Move()
    {
        rb.MovePosition(transform.position +  input * speed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        Move();
    }

    //Cuando se agarré un drop que aumente o un debuff que disminuya la velocidad, esta funcion se va a encargar de cambiarla
    /*void ChangeSpeed()
    {

    }*/
}
