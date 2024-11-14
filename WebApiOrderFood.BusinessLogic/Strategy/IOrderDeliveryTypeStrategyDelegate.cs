namespace WebApiOrderFood.BusinessLogic.Strategy
{
    public delegate IOrderDeliveryTypeStrategy ServiceResolver(string key);
}
