namespace WebApiOrderFoodClean.Application.Strategy;

public delegate IOrderDeliveryTypeStrategy ServiceResolver(string key);