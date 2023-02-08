// See https://aka.ms/new-console-template for more information

using SmartEnumExample.Domain.Entities;

var from = OrderStatus.From(5);

var fromValue = OrderStatus.FromValue<OrderStatus>(2);

var FromName = OrderStatus.FromName("Submitted");



Console.WriteLine("Hello, World!");
