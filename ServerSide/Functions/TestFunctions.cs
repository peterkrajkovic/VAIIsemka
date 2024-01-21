using Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerApi.Database;
using System;

namespace ServerApi.Functions
{
    public class TestFunctions
    {
        public static void LogTest(string testId, int duration, string testName, string testCategory, string result, string log, string assertionMessage, string parameters)
        {
            string dateTimeString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime parsedDateTime = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", null);
            Test newTest = new Test() { TestId = testId, Duration = duration, Name = testName, Category = testCategory, Result = result, Log = log, AssertionMessage = assertionMessage, Date = parsedDateTime, Parameters = parameters };
            using (var context = new DataContext())
            {
                context.Test.Add(newTest);
                context.SaveChanges();
            }
        }

        public static List<Test> GetTests()
        {
            using (var context = new DataContext())
            {
                var tests = context.Test.OrderByDescending(x => x.Date).ToList();
                return tests;
            }
        }
    }
}
