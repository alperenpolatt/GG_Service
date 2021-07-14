using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace GG_Service.Context
{
 
    public class GG_Orders_Context
    {
        [XmlRoot(ElementName = "neighborhoodType")]
        public class NeighborhoodType
        {
            [XmlElement(ElementName = "neighborhoodId")]
            public string? NeighborhoodId { get; set; }
            [XmlElement(ElementName = "neighborhoodName")]
            public string? NeighborhoodName { get; set; }
            [XmlElement(ElementName = "districtId")]
            public string? DistrictId { get; set; }
            [XmlElement(ElementName = "active")]
            public string? Active { get; set; }
        }

        [XmlRoot(ElementName = "buyerInfo")]
        public class BuyerInfo
        {
            [XmlElement(ElementName = "username")]
            public string Username { get; set; }
            [XmlElement(ElementName = "name")]
            public string Name { get; set; }
            [XmlElement(ElementName = "surname")]
            public string Surname { get; set; }
            [XmlElement(ElementName = "phone")]
            public string Phone { get; set; }
            [XmlElement(ElementName = "mobilePhone")]
            public string MobilePhone { get; set; }
            [XmlElement(ElementName = "address")]
            public string Address { get; set; }
            [XmlElement(ElementName = "district")]
            public string District { get; set; }
            [XmlElement(ElementName = "city")]
            public string City { get; set; }
            [XmlElement(ElementName = "neighborhoodType")]
            public NeighborhoodType NeighborhoodType { get; set; }
            [XmlElement(ElementName = "zipCode")]
            public string? ZipCode { get; set; }
        }

        [XmlRoot(ElementName = "combinedSaleCodes")]
        public class CombinedSaleCodes
        {
            [XmlElement(ElementName = "saleCode")]
            public string? SaleCode { get; set; }
        }

        [XmlRoot(ElementName = "shippingInfo")]
        public class ShippingInfo
        {
            [XmlElement(ElementName = "deliveryOption")]
            public string? DeliveryOption { get; set; }
            [XmlElement(ElementName = "shippingFirmName")]
            public string? ShippingFirmName { get; set; }
            [XmlElement(ElementName = "shippingFirmId")]
            public string? ShippingFirmId { get; set; }
            [XmlElement(ElementName = "combinedShipping")]
            public string? CombinedShipping { get; set; }
            [XmlElement(ElementName = "shippingPaymentType")]
            public string? ShippingPaymentType { get; set; }
            [XmlElement(ElementName = "cargoCode")]
            public string? CargoCode { get; set; }
            [XmlElement(ElementName = "shippingNotice")]
            public string? ShippingNotice { get; set; }
            [XmlElement(ElementName = "combinedSaleCodes")]
            public CombinedSaleCodes CombinedSaleCodes { get; set; }
            [XmlElement(ElementName = "shippingExpireDate")]
            public string ShippingExpireDate { get; set; }
        }

        [XmlRoot(ElementName = "sale")]
        public class Sale
        {
            [XmlElement(ElementName = "saleCode")]
            public string SaleCode { get; set; }
            [XmlElement(ElementName = "status")]
            public string Status { get; set; }
            [XmlElement(ElementName = "statusCode")]
            public string StatusCode { get; set; }
            [XmlElement(ElementName = "productId")]
            public string ProductId { get; set; }
            [XmlElement(ElementName = "productTitle")]
            public string ProductTitle { get; set; }
            [XmlElement(ElementName = "price")]
            public double Price { get; set; }
            [XmlElement(ElementName = "cargoPayment")]
            public string CargoPayment { get; set; }
            [XmlElement(ElementName = "cargoCode")]
            public string CargoCode { get; set; }
            [XmlElement(ElementName = "amount")]
            public string Amount { get; set; }
            [XmlElement(ElementName = "endDate")]
            public string EndDate { get; set; }
            [XmlElement(ElementName = "buyerInfo")]
            public BuyerInfo BuyerInfo { get; set; }
            [XmlElement(ElementName = "thumbImageLink")]
            public string ThumbImageLink { get; set; }
            [XmlElement(ElementName = "lastActionDate")]
            public string LastActionDate { get; set; }
            [XmlElement(ElementName = "variantId")]
            public string VariantId { get; set; }
            [XmlElement(ElementName = "moneyDate")]
            public string MoneyDate { get; set; }
            [XmlElement(ElementName = "shippingInfo")]
            public ShippingInfo ShippingInfo { get; set; }
            [XmlElement(ElementName = "invoiceInfo")]
            public InvoiceInfo InvoiceInfo { get; set; }
            [XmlElement(ElementName = "commissionRate")]
            public double CommissionRate { get; set; }
        }

        [XmlRoot(ElementName = "invoiceInfo")]
        public class InvoiceInfo
        {
            [XmlElement(ElementName = "fullname")]
            public string? Fullname { get; set; }
            [XmlElement(ElementName = "address")]
            public string? Address { get; set; }
            [XmlElement(ElementName = "district")]
            public string? District { get; set; }
            [XmlElement(ElementName = "cityCode")]
            public string? CityCode { get; set; }
            [XmlElement(ElementName = "phoneNumber")]
            public string? PhoneNumber { get; set; }
            [XmlElement(ElementName = "companyTitle")]
            public string? CompanyTitle { get; set; }
            [XmlElement(ElementName = "tcCertificate")]
            public string? TcCertificate { get; set; }
        }

        [XmlRoot(ElementName = "sales")]
        public class Sales
        {
            [XmlElement(ElementName = "sale")]
            public List<Sale> Sale { get; set; }
        }

        [XmlRoot(ElementName = "return")]
        public class Return
        {
            [XmlElement(ElementName = "ackCode")]
            public string AckCode { get; set; }
            [XmlElement(ElementName = "responseTime")]
            public string ResponseTime { get; set; }
            [XmlElement(ElementName = "timeElapsed")]
            public string TimeElapsed { get; set; }
            [XmlElement(ElementName = "saleCount")]
            public string SaleCount { get; set; }
            [XmlElement(ElementName = "sales")]
            public Sales Sales { get; set; }
            [XmlElement(ElementName = "sellerPromotionInfos")]
            public string SellerPromotionInfos { get; set; }
            [XmlElement(ElementName = "nextPageAvailable")]
            public string NextPageAvailable { get; set; }
        }

        [XmlRoot(ElementName = "getSalesResponse", Namespace = "http://sale.individual.ws.listingapi.gg.com")]
        public class GetSalesResponse
        {
            [XmlElement(ElementName = "return")]
            public Return Return { get; set; }
            [XmlAttribute(AttributeName = "sale", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Sale { get; set; }
        }

        [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Body
        {
            [XmlElement(ElementName = "getSalesResponse", Namespace = "http://sale.individual.ws.listingapi.gg.com")]
            public GetSalesResponse GetSalesResponse { get; set; }
        }

        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {
            [XmlElement(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public string Header { get; set; }
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body Body { get; set; }
            [XmlAttribute(AttributeName = "env", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Env { get; set; }
        }
    }

}
