using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GG_Service.Context;
using GG_Service.Models;
using GG_Service.DBContext;
using System.Xml.Serialization;
using System.IO;
using ExtendedXmlSerializer;
using System.Xml.Linq;
using static GG_Service.Context.GG_Orders_Context;
using System.Security.Cryptography;
using System.Text;

namespace GG_Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }
        public string EncryptWithMD5(string clearString)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(clearString);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString());
            }
            return sb.ToString();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                string ApiKey = "nXudnTDhCAvrdFA2Z7GmBQjns8Du7yCg";
                string SecretKey = "vMHCREKwhr7XG3e8";
                var time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var sign = EncryptWithMD5(String.Concat(ApiKey, SecretKey, time));
                var client = new RestClient("http://dev.gittigidiyor.com:8080/listingapi/ws/IndividualSaleService");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "text/xml");
                request.AddHeader("Authorization", "Basic dGVrbm9yYWtzLTpITlJNSnZra1BnUmpVUXBOZW12a3JYU3VFYUt4Z3FDVw==");
                request.AddHeader("Cookie", "JSESSIONID=15CFB70ABFAEF8445BD77D04B8F8225F");
                request.AddParameter("text/xml", "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sale=\"http://sale.individual.ws.listingapi.gg.com\">\r\n   <soapenv:Header/>\r\n   <soapenv:Body>\r\n      <sale:getSales>\r\n          <apiKey>wvASu88Mh3QTd2jY2nKhSwsMMsZzUbkS</apiKey>\r\n         <sign>"+sign+"</sign>\r\n         <time>"+time+"</time>\r\n         <withData>true</withData>\r\n         <byStatus>S</byStatus>\r\n         <byUser></byUser>\r\n         <orderBy>P</orderBy>\r\n         <orderType>A</orderType>\r\n         <pageNumber>1</pageNumber>\r\n         <pageSize>10</pageSize>\r\n         <lang>tr</lang>\r\n      </sale:getSales>\r\n   </soapenv:Body>\r\n</soapenv:Envelope>", ParameterType.RequestBody);


                IRestResponse response = client.Execute(request);
                XElement xElement = XElement.Parse(response.Content);
                List<XElement> items = xElement.Descendants("sales").ToList();

                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Sales), new XmlRootAttribute("sales"));
                    using (StringReader stringReader = new StringReader(items[0].ToString()))
                    {
                        GG_Orders_Context.Sales sprList = (GG_Orders_Context.Sales)serializer.Deserialize(stringReader);

                        var siparis = new GG_Orders();
                        using (var dbContext = new GG_DB_Context())
                        {
                            foreach (var spr in sprList.Sale)
                            {
                                if (!dbContext.GGOrders.Any(x => x.saleCode == (spr.SaleCode)))
                                {
                                    try
                                    {
                                        if (spr.BuyerInfo.NeighborhoodType == null)
                                        {
                                            var dn = new NeighborhoodType();
                                            dn.NeighborhoodId = "0";
                                            dn.NeighborhoodName = "0";
                                            dn.DistrictId = "0";
                                            dn.Active = "0";
                                            spr.BuyerInfo.NeighborhoodType = dn;
                                        }

                                        if (spr.InvoiceInfo==null)
                                        {
                                            var dnm = new InvoiceInfo();
                                            dnm.Address = "null";
                                            dnm.Fullname = "null";
                                            dnm.District = "null";
                                            dnm.PhoneNumber = "null";
                                            dnm.CityCode = "null";
                                            dnm.CompanyTitle = "null";
                                            dnm.TcCertificate = "null";
                                            spr.InvoiceInfo = dnm;
                                        }

                                        siparis = new GG_Orders();
                                        _ = dbContext.Add(new GG_Orders
                                        {
                                            saleCode = spr.SaleCode,
                                            status = spr.Status,
                                            statusCode =spr.StatusCode,
                                            productId = Convert.ToInt64(spr.ProductId),
                                            productTitle = spr.ProductTitle,
                                            price = spr.Price,
                                            cargoPayment = spr.CargoPayment ,
                                            cargoCode = spr.CargoCode ,
                                            amount = Convert.ToInt64(spr.Amount),
                                            endDate = Convert.ToDateTime(spr.EndDate),
                                            username_buyerinfo = spr.BuyerInfo.Username,	
                                            name_buyerinfo = spr.BuyerInfo.Name,
                                            surname_buyerinfo = spr.BuyerInfo.Surname,
                                            phone_buyerinfo = spr.BuyerInfo.Phone,
                                            mobilePhone_buyerinfo = spr.BuyerInfo.MobilePhone,
                                            address_buyerinfo = spr.BuyerInfo.Address,
                                            district_buyerinfo = spr.BuyerInfo.District,
                                            city_buyerinfo = spr.BuyerInfo.City,
                                            neighborhoodId = spr.BuyerInfo.NeighborhoodType.NeighborhoodId,
                                            neighborhoodName = spr.BuyerInfo.NeighborhoodType.NeighborhoodName,
                                            districtId =spr.BuyerInfo.NeighborhoodType.DistrictId,
                                            active =spr.BuyerInfo.NeighborhoodType.Active,  
                                            zipCode = spr.BuyerInfo.ZipCode,
                                            thumbImageLink =spr.ThumbImageLink,
                                            lastActionDate = Convert.ToDateTime(spr.LastActionDate),
                                            variantId = Convert.ToInt64(spr.VariantId),
                                            moneyDate =Convert.ToDateTime(spr.MoneyDate),
                                            fullname_invoiceInfo = spr.InvoiceInfo.Fullname,
                                            address_invoiceInfo = spr.InvoiceInfo.Address ,
                                            district_invoiceInfo = spr.InvoiceInfo.District,
                                            cityCode_invoiceInfo = spr.InvoiceInfo.CityCode,
                                            phoneNumber_invoiceInfo = spr.InvoiceInfo.PhoneNumber,
                                            companyTitle_invoiceInfo = spr.InvoiceInfo.CompanyTitle,
                                            tcCertificate_invoiceInfo =spr.InvoiceInfo.TcCertificate,
                                            deliveryOption = spr.ShippingInfo.DeliveryOption,
                                            shippingFirmName = spr.ShippingInfo.ShippingFirmName,
                                            shippingFirmId = spr.ShippingInfo.ShippingFirmId,
                                            combinedShipping = spr.ShippingInfo.CombinedShipping,
                                            shippingPaymentType = spr.ShippingInfo.ShippingPaymentType,
                                            cargoCode_shippinginfo = spr.ShippingInfo.CargoCode,
                                            shippingNotice = spr.ShippingInfo.ShippingNotice,
                                            shippingExpireDate = Convert.ToDateTime(spr.ShippingInfo.ShippingExpireDate),
                                            saleCode_combinedSaleCodes = spr.ShippingInfo.CombinedSaleCodes.SaleCode,
                                            commissionRate = spr.CommissionRate,
                                        });
                                        dbContext.SaveChanges();
                                    }
                                    catch (Exception ex)
                                    {
                                        throw;
                                    }
                                    siparis.saleCode = spr.SaleCode;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                Console.WriteLine(response.Content);
                await Task.Delay(3 * 1000, stoppingToken);
            }

        }
    }
}
