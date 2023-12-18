using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetParentTopPos();


    public void SetKitchenObject(KitchenObject kitchenObject);


    public KitchenObject GetKitchenObject();


    public bool HasKitchenObject();


    public void RemoveKitchenObject();

}
