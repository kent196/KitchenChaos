using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;




    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            //This counter has a kitchen object
            if (player.HasKitchenObject())
            {
                //This player is holding a kitchen object
                //Do nothing
            }
            else
            {
                //This player is holding nothing
                GetKitchenObject().SetKitchenObjectParent(player);

            }
        }
        else
        {
            //This counter doesn't have a kitchen object
            if (player.HasKitchenObject())
            {
                //This player is holding a kitchen object
                //Drop to this counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //This player is holding nothing
                //Do nothing
            }
        }
    }


}
