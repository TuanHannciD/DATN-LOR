﻿namespace AuthDemo.Models.VnPay
{
    public class PaymentInformationModel
    {
        public Guid OrderId { get; set; }
        public string OrderType { get; set; }
        public double Amount { get; set; }
        public string OrderDescription { get; set; }
        public string Name { get; set; }

    }
}
