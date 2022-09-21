using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using Unity.MLAgents.Sensors;

public class AgentMovementWSAD : Agent
{
    public float rotateSpeed = 180f;
    public float forwardSpeed = 600f;
    public float backSpeed = 450f;
    private float speedModifier = 1f;
    public GameObject[] agents;

    private int numberOfRays = 17;
    private float RayDistance = 500.0f;

    private GameObject ownBase;
    private GameObject enemyBase;

    [HideInInspector]
    public bool isAgentSet;

    private float latestDistanceOwn;
    private float latestDistanceEnemy;
    private bool latestPurposeWasEnemyFlag;
    private int decisionPeriod;

    public bool weGotFlag = false;

    void Awake()
    {
        isAgentSet = false;
    }

    void Start()
    {

        if (gameObject.GetComponent<AgentComponentsScript>().color == "blue")
        {
            enemyBase = GameObject.Find("Red Base(Clone)");
            ownBase = GameObject.Find("Blue Base(Clone)");
        }
        else
        {
            enemyBase = GameObject.Find("Blue Base(Clone)");
            ownBase = GameObject.Find("Red Base(Clone)");
        }

        latestDistanceOwn = Vector3.Distance(gameObject.transform.position, ownBase.transform.position);
        latestDistanceEnemy = Vector3.Distance(gameObject.transform.position, enemyBase.transform.position);
        latestPurposeWasEnemyFlag = true;
        decisionPeriod = gameObject.GetComponent<DecisionRequester>().DecisionPeriod;

        isAgentSet = true;
    }

    private void FixedUpdate()
    {
        // FixUpdDistanceStandard();
        if (!weGotFlag || gameObject.GetComponent<AgentComponentsScript>().AgentFlag.activeSelf)    // jeśli moja drużyna nie ma flagi lub ja mam flagę
            DistanceRewardKacpraPoKonsultacji();
    }

    private void FixUpdDistanceStandard()
    {
        RewardValuesScript.getRewardValues();
        float distance;
        if (GameManager.EnvContr.GetSteps() % 200 == 0 && GameManager.EnvContr.GetSteps() != 0)
        {
            if (gameObject.GetComponent<AgentComponentsScript>().AgentFlag.activeSelf)
            {
                distance = Vector3.Distance(gameObject.transform.position, ownBase.transform.position);
            }
            else
            {
                distance = Vector3.Distance(gameObject.transform.position, enemyBase.transform.position);
            }
            float reward = FlagDistanceReward(distance);
            AddRewardAgent(reward);
            UnityEngine.Debug.Log(gameObject.ToString() + " ::: NAGRODA " + reward);
        }
        
    }

    private void FixUpdDistanceRewardsCloserBetter()
    {
        RewardValuesScript.getRewardValues();
        if (GameManager.EnvContr.GetSteps() % 200 == 0 && GameManager.EnvContr.GetSteps() != 0)
        {
            float distance;
            if (gameObject.GetComponent<AgentComponentsScript>().AgentFlag.activeSelf)
            {
                distance = Vector3.Distance(gameObject.transform.position, ownBase.transform.position);
                if (distance < latestDistanceOwn)
                {
                    float reward = FlagDistanceReward(distance);
                    AddRewardAgent(reward);
                    UnityEngine.Debug.Log(gameObject.ToString() + " ::: NAGRODA " + reward);
                }
                else
                {
                    float reward = -0.5f * FlagDistanceReward(distance);
                    AddRewardAgent(reward);
                    UnityEngine.Debug.Log(gameObject.ToString() + " ::: NAGRODA " + reward);
                }
            }
            else
            {
                distance = Vector3.Distance(gameObject.transform.position, enemyBase.transform.position);
                if (distance < latestDistanceEnemy)
                {
                    float reward = FlagDistanceReward(distance);
                    AddRewardAgent(reward);
                    UnityEngine.Debug.Log(gameObject.ToString() + " ::: NAGRODA " + reward);
                }
                else
                {
                    float reward = -0.5f * FlagDistanceReward(distance);
                    AddRewardAgent(reward);
                    UnityEngine.Debug.Log(gameObject.ToString() + " ::: NAGRODA " + reward);
                }
            }


            latestDistanceOwn = Vector3.Distance(gameObject.transform.position, ownBase.transform.position);
            latestDistanceEnemy = Vector3.Distance(gameObject.transform.position, ownBase.transform.position);

        }
    }

    private void DistanceRewardKacpraPoKonsultacji()
    {
        RewardValuesScript.getRewardValues();
        float distanceOwnNow = Vector3.Distance(gameObject.transform.position, ownBase.transform.position);
        float distanceEnemyNow = Vector3.Distance(gameObject.transform.position, enemyBase.transform.position);

        // MOŻLIWE ŻE LEPIEJ WYJDZIE JAK CO 5 STEPÓW BĘDZIE (CZY RACZEJ CO TYLE ILE MAMY decision interval (u nas chyba 5))
        if (GameManager.EnvContr.GetSteps() % decisionPeriod == 0 && GameManager.EnvContr.GetSteps() != 0)
        {
            if (gameObject.GetComponent<AgentComponentsScript>().AgentFlag.activeSelf)
            {
                if (!weGotFlag) // jeśli jeszcze nie wiemy że mamy flage, musze powiedzieć o tym kompanom
                    GameManager.EnvContr.TeamGotFlag(gameObject.GetComponent<AgentComponentsScript>().color);

                if (latestPurposeWasEnemyFlag)  // ten if jest żeby po zdobyciu flagi nie miał od razu kary z rozpędu za oddalanie się od swojej flagi i miał szansę się wycofać
                {
                    latestDistanceOwn = distanceOwnNow;
                    latestDistanceEnemy = distanceEnemyNow;
                    latestPurposeWasEnemyFlag = false;
                    return;
                }
                AddFlagDistanceRewardBasedOnDistanceDifference(distanceOwnNow, latestDistanceOwn);
            }
            else
            {
                if (weGotFlag) // jeśli jeszcze nie wiemy że straciliśmy flage, musze powiedzieć o tym kompanom
                    GameManager.EnvContr.TeamLostFlag(gameObject.GetComponent<AgentComponentsScript>().color);

                if (!latestPurposeWasEnemyFlag)  // ten if jest żeby po straceniu flagi nie miał od razu kary z rozpędu za oddalanie się od flagi wroga i miał szansę się wycofać
                {
                    latestDistanceOwn = distanceOwnNow;
                    latestDistanceEnemy = distanceEnemyNow;
                    latestPurposeWasEnemyFlag = true;
                    return;
                }
                AddFlagDistanceRewardBasedOnDistanceDifference(distanceEnemyNow, latestDistanceEnemy);
            }
        }
        latestDistanceOwn = distanceOwnNow;
        latestDistanceEnemy = distanceEnemyNow;
    }

    private float FlagDistanceReward(float distance)
    {
        float max = RewardValuesScript.rewards["agentCloseToFlag"];
        float y = (-distance * max / 400) + max;
        return y > 0 ? y : 0;
    }

    private void AddFlagDistanceRewardBasedOnDistanceDifference(float distanceNow, float lastestDistance)
    {
        float maxReward = RewardValuesScript.rewards["agentCloseToFlag"];
        float distanceDifference = lastestDistance - distanceNow;
        float reward = maxReward * distanceDifference;
        Debug.Log("Distance Difference = " + distanceDifference + "  Reward = " + reward);
        AddRewardAgent(reward);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        if(gameObject.GetComponent<BehaviorParameters>().BehaviorType.ToString() == "HeuristicOnly")
        {
            return;
        }

        var forwardAxis = actionBuffers.DiscreteActions[0];
        var rotateAxis = actionBuffers.DiscreteActions[1];

        Rigidbody rb = GetComponent<Rigidbody>();
        switch (forwardAxis)
        {
            case 0: // do tylu
                rb.AddForce(transform.rotation * Vector3.back * backSpeed * Time.deltaTime * speedModifier, ForceMode.VelocityChange);
                break;
            case 1: // brak akcji
                break;
            case 2: // do przodu
                rb.AddForce(transform.rotation * Vector3.forward * forwardSpeed * Time.deltaTime * speedModifier, ForceMode.VelocityChange);
                break;
        }

        switch (rotateAxis)
        {
            case 0: // w lewo
                transform.Rotate(0, -rotateSpeed * Time.deltaTime * speedModifier, 0, Space.World);
                break;
            case 1: // brak rotacji
                break;
            case 2: // w prawo
                transform.Rotate(0, rotateSpeed * Time.deltaTime * speedModifier, 0, Space.World);
                break;
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {

        bool agentHoldsFlag = gameObject.GetComponent<AgentComponentsScript>().AgentFlag.activeSelf;    // IS HOLDING FLAG? 1 float
        sensor.AddObservation(agentHoldsFlag ? 1.0f : 0.0f );
        float[,] arrRays = raysPerception(); //17 rays * 6 variables = 102 floats
        for (int i = 0; i < numberOfRays; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                sensor.AddObservation(arrRays[i,j]);
            }
         }
        BiomEyesScript biomEyes = (BiomEyesScript)GetComponentInChildren(typeof(BiomEyesScript));
        //Debug.Log("BIOMEYES:" + biomEyes);
        int[] arrint = biomEyes.GetBiomSensors();
       // Debug.Log("BIOMEYES array:" + arrint+ arrint.Length);
        for (int i = 0; i < arrint.Length; i++){ //44 biomEyes * 4 inputy
            float[] biomEye = { 0, 0, 0, 0 }; //0-nic/inne, 1-accelerate, 2-desert, 3-lake
            biomEye[arrint[i]] = 1;
            for(int j=0; j < biomEye.Length; j++)
                sensor.AddObservation(biomEye[j]);
        }
    }

    private float[,] raysPerception()
    {
        int layerMask = 1 << 6;
        float startDegree = -90.0f;//zawsze musi byc ujemne!
        float stepDegree = -2 * startDegree / (float)numberOfRays;
        

        float[,] outputArray = new float[numberOfRays,6]; //17 promieni, po 6 zmiennych, i,0-odwrotność dystansu, i,1-agent, i,2-baza, i,3-ściana, i,4-kolor, i,5-czy z flagą?
        RaycastHit hit;
        Ray ray;
        for (int i = 0; i < numberOfRays; i++)
        {
           ray  = new Ray(transform.position, transform.TransformDirection(Quaternion.Euler(0, startDegree+stepDegree*i, 0) * Vector3.forward));

            if (Physics.Raycast(ray, out hit, RayDistance, layerMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
                rayResponseComponent rayrespond = hit.collider.gameObject.GetComponent<rayResponseComponent>();
                //Debug.Log(this.name + "Ray" + i + "Did Hit: " + hit.collider.gameObject + " in distance: " + hit.distance + "|" + rayrespond.type + rayrespond.color + rayrespond.isFlag);
                outputArray[i, 0] = Normalize(Inverse(hit.distance));
                outputArray[i, 1] = 0;
                outputArray[i, 2] = 0;
                outputArray[i, 3] = 0;
                switch (rayrespond.type)
                {
                    case 1:
                        outputArray[i, 1] = 1;
                        break;
                    case 2:
                        outputArray[i, 2] = 1;
                        break;
                    case 3:
                        outputArray[i, 3] = 1;
                        break;
                }
                float col = rayrespond.color;
                outputArray[i, 4] = col;
                outputArray[i, 5] = rayrespond.isFlag;

            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction * RayDistance, Color.green);
                //Debug.Log(this.name + "Ray"+i+ "Did not Hit");
            }

        }
        return outputArray;
    }

    private float Inverse(float distance)
    {
        return RayDistance - distance;
    }

    private float Normalize(float inverseDistance)
    {
        return inverseDistance/RayDistance;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    //public void FixedUpdate()
    {
        bool W, S, A, D;
        if (AiTrainer.GetAITrainerMode())
            (W, S, A, D) = AiTrainer.Run();
        else
        {
            W = Input.GetKey(KeyCode.W);
            S = Input.GetKey(KeyCode.S);
            A = Input.GetKey(KeyCode.A);
            D = Input.GetKey(KeyCode.D);
        }
        Walking(W, S, A, D);
    }

    public void Walking(bool W, bool S, bool A, bool D)
    {
        // Faster forward than back
        Rigidbody rb = GetComponent<Rigidbody>();
        if (W)
            rb.AddForce(transform.rotation * Vector3.forward * forwardSpeed * Time.deltaTime * speedModifier, ForceMode.VelocityChange);
        else if (S)
            rb.AddForce(transform.rotation * Vector3.back * backSpeed * Time.deltaTime * speedModifier, ForceMode.VelocityChange);
        else
            rb.velocity = Vector3.zero;


        if (A)
            transform.Rotate(0, -rotateSpeed * Time.deltaTime * speedModifier, 0, Space.World);
        if (D)
            transform.Rotate(0, rotateSpeed * Time.deltaTime * speedModifier, 0, Space.World);


        speedModifier = 1f;
        //dla widzenia promieni
       // raysPerception();
        //BiomEyesScript bes = (BiomEyesScript)GetComponentInChildren(typeof(BiomEyesScript));
        //bes.GetBiomSensors();
    }

    public void ChangeSpeedModifier(float newModified)
    {
        speedModifier = newModified;
    }

    public void AddRewardAgent(float reward)
    {
        AddReward(reward);
    }

    public override void OnEpisodeBegin()
    {
        Debug.Log("EPISODE BEGIN");
    }
}
