using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using WebCrawlerAPI.Controllers;
using WebCrawlerAPI.Models;


namespace WebCrawlerAPI
{
    public static class WebsiteCrawler
    {
        public static void GetAllDiscounts()
        {
            #region Fields and Properties
            string allGamesUrl = "https://www.nintendo.co.uk/Search/Search-299117.html?f=147394-5-81";
            string discountedGamesUrl = "https://www.nintendo.co.uk/Search/Search-299117.html?f=147394-5-81-6956";
            List<GameModel> titles = new List<GameModel>();
            IWebElement _name;
            IWebElement _originalPrice;
            IWebElement _discountedPrice;
            string ogPrice = "";
            string dcPrice = "";
            int count = 1;
            #endregion
            
           

        #region ChromeDriver Setup
     
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--ignore-certificate-errors");
            chromeOptions.AddArgument("--disable-gpu");
            
            chromeOptions.AddArgument("--nogpu");
            chromeOptions.AddArgument("--disable-dev-shm-usage");
            chromeOptions.AddArgument("--disable-extensions");
            chromeOptions.AddArgument("--window-size=1920,1080");
            chromeOptions.AddArgument("--allow-running-insecure-content");
            chromeOptions.AddArgument("--allow-insecure-localhost");
            chromeOptions.AddAdditionalCapability("CapabilityType.AcceptSslCertificates", true, true);
            chromeOptions.AddAdditionalCapability("CapabilityType.AcceptInsecureCerts", true, true);
            string location = AppDomain.CurrentDomain.BaseDirectory + "Chrome\\Application\\chrome.exe";

            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            chromeOptions.BinaryLocation = location;
            #endregion

            #region Crawling
            using (var browser = new ChromeDriver(service, chromeOptions, TimeSpan.FromMinutes(3)))
            {
                browser.Manage().Timeouts().PageLoad.Add(System.TimeSpan.FromSeconds(30));
                browser.Url = discountedGamesUrl;
                browser.FindElementByXPath("//a[@class='pla-btn pla-btn--region-store pla-btn--block plo-cookie-overlay__accept-btn']").Click();
                Thread.Sleep(2000);
                var goodList = browser.FindElementsByXPath("//div[@class='search-result-txt col-xs-9 col-sm-10']//p[@class='page-title']");
               
                //Loop that finds discounted games on European Nintendo Eshop site. Loops through html elements.
                do
                {
                    goodList = browser.FindElementsByXPath("//div[@class='search-result-txt col-xs-9 col-sm-10']");
                    foreach (var wElement in goodList)
                    {
                        ogPrice = "";
                        dcPrice = "";
                        _name = wElement.FindElement(By.XPath(".//p[@class='page-title']"));
                        try
                        {
                            _originalPrice = wElement.FindElement(By.XPath(".//span[@class='original-price']"));
                            ogPrice = _originalPrice.Text;
                        }
                        catch (Exception)
                        {
                            _originalPrice = null;
                        }

                        try
                        {
                            _discountedPrice = wElement.FindElement(By.XPath(".//span[@class='discount']"));
                            dcPrice = _discountedPrice.Text;
                        }
                        catch (Exception)
                        {
                            _discountedPrice = null;

                        }
                        titles.Add(new GameModel
                        {
                            Title = _name.Text,
                            OriginalPrice = ogPrice,
                            DiscountPrice = dcPrice

                        });
                    }
                    var buttons = browser.FindElementsByXPath("//button[@class='btn btn-primary']");
                    try
                    {
                        buttons[5].Click();
                    }
                    catch(ArgumentOutOfRangeException)
                    {
                        break;
                    }
                    count++;
                    Thread.Sleep(2000);
                } while (count <= 25);
                browser.Close();
            }
            
            #endregion
            GameModelsController controller = new GameModelsController();
            
            foreach (GameModel g in titles)
            {
                controller.PostGameModel(g);
            }
            
           
        }
    }
    
}