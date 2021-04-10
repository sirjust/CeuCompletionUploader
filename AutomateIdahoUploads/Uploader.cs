using CommonCode.Helpers;
using CommonCode.Models;
using CommonCode.StaticData;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AutomateIdahoUploads
{
    public class Uploader : IUploader
    {
        IWebDriver _driver;
        ILoginInfo _loginInfo;
        WebDriverWait _wait;
        IErrorHelper _errorHelper;
        ILogger _logger;

        public Uploader(IWebDriver driver, ILoginInfo loginInfo, IErrorHelper errorHelper, ILogger logger)
        {
            _driver = driver;
            _loginInfo = loginInfo;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            _errorHelper = errorHelper;
            _logger = logger;
        }

        public void InputCompletions(IEnumerable<Completion> completions)
        {
            LoginToWebsite();

            IWebElement courseButton = _wait.Until(d => d.FindElement(By.XPath("//aside[@id='nav']//section//span[@data-bind='html: title'][contains(text(),'Courses')]")));
            courseButton.Click();

            foreach (Completion completion in completions)
            {
                // from here we loop through each completion
                string courseNumber = completion.Course;
                string license = completion.License;
                string dateString = completion.Date;
                string[] splitUpDate = dateString.Split('-');
                int year = int.Parse(splitUpDate[0]);
                int month = int.Parse(splitUpDate[1]);
                int day = int.Parse(splitUpDate[2]);

                DateTime completionDate = new DateTime(year, month, day);

                try
                {
                    IWebElement courseField = _wait.Until(d => d.FindElement(By.CssSelector(".input-sm.form-control.input-s-sm")));
                    courseField.SendKeys(courseNumber);
                    Thread.Sleep(3000);
                    courseField.SendKeys(Keys.Return);
                    Thread.Sleep(3000);
                    IWebElement firstVisibleCourse = _wait.Until(d => d.FindElement(By.XPath("/html[1]/body[1]/div[1]/div[1]/section[1]/section[1]/section[1]/section[1]/section[1]/section[1]/div[1]/section[1]/section[1]/section[1]/section[1]/section[1]/div[1]/ul[1]/li[1]/small[1]/span[2]")));
                    if (courseNumber == firstVisibleCourse.GetAttribute("innerHTML"))
                    {
                        _wait.Until(d => d.FindElement(By.PartialLinkText("Manage"))).Click();
                        _wait.Until(d => d.FindElement(By.LinkText("Add"))).Click();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex, completion);
                    //then go back to the previous page
                    _driver.Navigate().Refresh();
                    Thread.Sleep(3000);
                    continue;
                }

                try
                {
                    Thread.Sleep(3000);
                    IWebElement stateField = _wait.Until(d => d.FindElement(By.Id("state")));
                    stateField.Click();
                    stateField.SendKeys(Keys.ArrowDown);
                    stateField.SendKeys(Keys.Return);
                    Thread.Sleep(3000);

                    _wait.Until(d => d.FindElement(By.Id("licenseNumber"))).SendKeys(license);
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex, completion);
                    //then go back to the previous page
                    IWebElement goBack = _driver.FindElement(By.Id("btnPrev"));
                    courseButton.Click();
                    Thread.Sleep(3000);
                    continue;
                }

                try
                {
                    IWebElement dateCompletedField = _wait.Until(d => d.FindElement(By.Id("dateCompleted")));
                    dateCompletedField.Click();
                    int dateLength = 10;
                    while (dateLength > 0)
                    {
                        dateCompletedField.SendKeys(Keys.Backspace);
                        dateLength--;
                    }

                    dateCompletedField.SendKeys(string.Format("{0:MM/dd/yyyy}", completionDate));
                    dateCompletedField.SendKeys(Keys.Enter);

                    IWebElement scrollField = _wait.Until(d => d.FindElement(By.Id("formNav")));
                    scrollField.Click();

                    Actions actions = new Actions(_driver);
                    actions.SendKeys(Keys.End).Build().Perform();

                    Thread.Sleep(3000);

                    IWebElement saveButton = _wait.Until(d => d.FindElement(By.CssSelector(".btn.btn-primary.btn-s-xs.btn-sm")));
                    saveButton.Click();

                    Thread.Sleep(3000);

                    IWebElement postButton = _wait.Until(d => d.FindElement(By.LinkText("Post")));
                    postButton.Click();

                    Thread.Sleep(3000);

                    courseButton.Click();
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex, completion);
                    //then go back to the previous page
                    IWebElement goBack = _driver.FindElement(By.Id("btnPrev"));
                    courseButton.Click();
                    Thread.Sleep(3000);
                    continue;
                }
                finally
                {
                    courseButton.Click();
                    Thread.Sleep(3000);
                }
            }
        }

        public void LoginToWebsite()
        {
            _driver.Url = _loginInfo.IdahoLoginUrl;
            _driver.Manage().Window.Maximize();
            
            _driver.FindElement(By.Id("username")).SendKeys(_loginInfo.IdahoId);
            _driver.FindElement(By.Id("password")).SendKeys(_loginInfo.IdahoPassword);

            _wait.Until(d => d.FindElement(By.XPath("//button[@type='submit']//div[@class='btn-content']"))).Click();
        }
    }
}
