using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using GymTrackerModel;
using GymTrackerBusiness;
using NUnit.Framework;

namespace GymTrackerTests
{
    public class Tests
    {
        CRUDManager crudManager = new CRUDManager();

        /* so i need to test 
        read users
        read sessions
        read exercises
        read all exercise
        read muscle groups
        read 6 week overview
        read exersice overview
        create users
        create session
        create exercise
        create sets
        remove sets
        remove session (remove exercies?) (removes sets)
        remove user  (removes sessions and sets)
        update user name
        update session date?
        update set detials

        */



        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}