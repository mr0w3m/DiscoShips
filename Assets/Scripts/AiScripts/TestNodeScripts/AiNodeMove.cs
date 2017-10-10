using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AiNodeMove : MonoBehaviour
{

    public Node[] nodeList;
    public Node startNode;
    public Node setTargetNode;
    public float movementSpeed;
    public float navTolerance;

    private List<Node> closedList = new List<Node>();

    private PathNode tempPathNode;
    private int tempNodeIndex;
    private Node selectedNode;
    private Node targetNode;

    SpriteRenderer tempSprite;
    private int numNodes;

    //Movement
    private Rigidbody2D thisRB;
    private int helpIndex = 0;
    private Vector2 testVector;
    float distBetween;

    //FindClosestNode
    public float findDistance;
    private int nodeLayer;
    public GameObject leader;

    //Looping Awareness
    private WaitForSeconds iterationTime = new WaitForSeconds(2f);

    void Awake()
    {
        thisRB = GetComponent<Rigidbody2D>();
        nodeLayer = LayerMask.GetMask("NodeLayer");
    }

    void Start()
    {
        StartCoroutine(Follow());
    }

    void Function()
    {
        //Set the starting node as the selectedNode
        selectedNode = FindClosestNode(this.gameObject);
        //Set the target node
        targetNode = FindClosestNode(leader);
        //Start the search for the target node;
        StartCoroutine(ThisCoroutine());
    }

    IEnumerator Follow()
    {
        while(true)
        {
            Function();
            yield return iterationTime;
        }
    }

    IEnumerator ThisCoroutine()
    {
        while (true)
        {
            //Add the first node in the way
            closedList.Add(selectedNode);
            //tempSprite = selectedNode.GetComponent<SpriteRenderer>();
            //tempSprite.color = Color.red;
            //at the end make the right node the selected node so when it loops around again it sets it automatically.
            if (selectedNode.index == targetNode.index)
            {
                //Start the movement with the closed list
                Debug.Log(closedList.Count);
                //Call move
                StartCoroutine(MoveToPoint(closedList[helpIndex].transform.position, closedList[closedList.Count -1].transform.position));
                yield break;

            }
            else
            {
                //find the closest node in nearbynodes
                Node newerNode = FindNearestNodeIndex(selectedNode);
                selectedNode = newerNode;
            }
        }
    }

    Node FindNearestNodeIndex(Node inputNode)
    {
        int nearestNodeIndex = 0;
        float lowestAmount = 100f;
        Node nodeSelected = inputNode;

        //this loops through the nearby nodes
        for (int i = 0; i < inputNode.nearbyNodes.Length; i++)
        {
            float amt;

            amt = FindF(inputNode.transform.position, inputNode.nearbyNodes[i]);

            if (amt < lowestAmount)
            {
                lowestAmount = amt;
                nearestNodeIndex = i;
                nodeSelected = inputNode.nearbyNodes[i];
            }
        }


        return nodeSelected;
    }

    float FindF(Vector2 location, Node nearbyNode)
    {
        float localDistance;
        float distanceToTarget;


        localDistance = Vector2.Distance(location, nearbyNode.transform.position);
        distanceToTarget = Vector2.Distance(nearbyNode.transform.position, targetNode.transform.position);
        return localDistance + distanceToTarget;
    }
    
    void ZeroIndex()
    {
        helpIndex = 0;
    }
    

    IEnumerator MoveToPoint(Vector2 target, Vector2 endPoint)
    {
        Debug.Log("movetopointcalled: " + target);
        while (Vector2.Distance(transform.position, target) > navTolerance)
        {
            //Set position
            Vector2 myPosition2D = transform.position;
            //make a Vector2 based off the position of the node we're going to and our position
            Vector2 moveDirection = (target - myPosition2D).normalized;
            //Using the moveDirection, move this Ai closer and closer to the point via addforce
            thisRB.AddForce(moveDirection * movementSpeed);
            yield return null;
        }
        Debug.Log("finishedMove");
        //call iterate next node
        if (target != endPoint)
        {
            Debug.Log("iterate");
            helpIndex++;
            //call move to new index
            StartCoroutine(MoveToPoint(closedList[helpIndex].transform.position, endPoint));
        }
        else
        {
            Debug.Log("totally done");
            ZeroIndex();
            closedList.Clear();
        }
    }

    //Find closest node in overlapped colliders
    Node FindClosestNode(GameObject obj)
    {
        float shortestDistance = 100f;
        int nearestNodeIndex = 0;
        Collider2D[] hitColliders;
        hitColliders = Physics2D.OverlapCircleAll(obj.transform.position, findDistance, nodeLayer);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            //loop through each collider and find out which is closest.
            
            float amt;

            amt = Vector2.Distance(hitColliders[i].transform.position, transform.position);

            if (amt < shortestDistance)
            {
                shortestDistance = amt;
                nearestNodeIndex = i;
            }
        }
        Debug.Log(nearestNodeIndex);
        return hitColliders[nearestNodeIndex].GetComponent<Node>();
    }
}
