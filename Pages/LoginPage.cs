using Microsoft.VisualBasic;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample_NUnit.Utilities;

namespace Sample_NUnit.Pages
{
    public class LoginPage
    {
        // Driver declartion
        IWebDriver driver;
        
        // Constructor
        
        public LoginPage(IWebDriver dr)
        {
            // Assigning the incoming driver from Admin page to parameter dr
            driver = dr;

            // Implicit wait declaration
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            // Page factory class to intialize elements
            PageFactory.InitElements(driver, this);
            
            


        }
        // Declaring elements using pagefactory

        [FindsBy(How = How.Id, Using = "user-name")]
        IWebElement UserName;

        [FindsBy(How = How.Id, Using = "password")]
        IWebElement Password;

        [FindsBy(How = How.Id, Using = "login-button")]
        IWebElement Login_Btn;

        // Login method
        public void Login()
        {
            // passing the user name
            UserName.SendKeys(constant.User);
            // passing the password
            Password.SendKeys(constant.Password);
            // Clicking the button
            Login_Btn.Click();
        }


    }
}
