using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangeEventArgs> OnSelectedCounterChange;
    [SerializeField] private Transform holdingPoint;
    private KitchenObject kitchenObject;



    public class OnSelectedCounterChangeEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] GameInput gameInput;
    [SerializeField] LayerMask countersLayerMask;

    private BaseCounter baseCounter;
    private bool isWalking;
    private Vector3 lastInteractionDir;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is another instance");
        }
        Instance = this;
    }
    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (baseCounter != null)
        {
            baseCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (baseCounter != null)
        {
            baseCounter.Interact(this);
        }
    }

    private void Update()
    {
        MovePlayer();
        HandleInteract();
    }

    private void HandleInteract()
    {
        Vector2 inputVector = gameInput.GetGameInputNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractionDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                //clearCounter.Interact();
                if (baseCounter != this.baseCounter)
                {
                    SelectedCounter(baseCounter);
                }
            }
            else
            {
                SelectedCounter(null);
            }

        }
        else
        {
            SelectedCounter(null);

        }

    }

    private void MovePlayer()
    {
        Vector2 inputVector = gameInput.GetGameInputNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float playerRadius = 0.7f;
        float playHeight = 2f;
        float maxDistance = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playHeight, playerRadius, moveDir, maxDistance);

        if (!canMove)
        {
            //Cant move toward moveDir
            //Try to move on X dir

            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playHeight, playerRadius, moveDirX, maxDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                //Cant move on X dir
                //Try to move on Z dir
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playHeight, playerRadius, moveDirZ, maxDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {
                    //Cant move
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;

        }

        isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void SelectedCounter(BaseCounter baseCounter)
    {
        this.baseCounter = baseCounter;
        OnSelectedCounterChange?.Invoke(this, new OnSelectedCounterChangeEventArgs
        {
            selectedCounter = baseCounter
        });
    }

    public Transform GetParentTopPos()
    {
        return holdingPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }

    public void RemoveKitchenObject()
    {
        kitchenObject = null;
    }
}
