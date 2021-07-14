using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace GG_Service.Models
{
    public class GG_Orders
    {
            [Key]    
            public Int64 gg_orders_ID {get; set;}

            public string saleCode { get; set; }
            public string status { get; set; }
            public string statusCode { get; set; }
            public Int64 productId { get; set; }
            public string productTitle { get; set; }
            public double price { get; set; }
            public string cargoPayment { get; set; }
            public string cargoCode { get; set; }
            public Int64 amount { get; set; }
            public DateTime endDate { get; set; }

            public string username_buyerinfo { get; set; }
            public string name_buyerinfo { get; set; }
            public string surname_buyerinfo { get; set; }
            public string phone_buyerinfo { get; set; }
            public string mobilePhone_buyerinfo { get; set; }
            public string address_buyerinfo { get; set; }
            public string district_buyerinfo { get; set; }
            public string city_buyerinfo { get; set; }

            public string? neighborhoodId { get; set; }
            public string? neighborhoodName { get; set; }
            public string? districtId { get; set; }
            public string? active { get; set; }
            
            public string? zipCode { get; set; }
            
            public string thumbImageLink { get; set; }
            public DateTime lastActionDate { get; set; }
            public Int64 variantId { get; set; }
            public DateTime moneyDate { get; set; }

           
            public string fullname_invoiceInfo { get; set; }
            public string address_invoiceInfo { get; set; }
            public string district_invoiceInfo { get; set; }
            public string cityCode_invoiceInfo { get; set; }
            public string phoneNumber_invoiceInfo { get; set; }
            public string companyTitle_invoiceInfo { get; set; }
            public string tcCertificate_invoiceInfo { get; set; }

           
            public string deliveryOption { get; set; }
            public string shippingFirmName { get; set; }
            public string shippingFirmId { get; set; }
            public string combinedShipping { get; set; }
            public string shippingPaymentType { get; set; }
            public string cargoCode_shippinginfo { get; set; }
            public string shippingNotice { get; set; }
           
            public DateTime shippingExpireDate { get; set; }

            public string saleCode_combinedSaleCodes { get; set; }
            
            public double commissionRate { get; set; }












    }
}
