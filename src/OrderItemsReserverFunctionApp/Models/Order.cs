using System;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace OrderItemsReserverFunctionApp.Models;

public class Order
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public Address ShipToAddress { get; set; }
    public IList<OrderItem> OrderItems { get; set; }
}
