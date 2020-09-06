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
        select user
        add user
        remove user

        read user sessions
        add session*
        remove session
        
        read user sets
        add sets
        remove sets
        update sets

        read user exercise
        select exercises
        add exercises**
        delete exercises
        read all exercises

        read muscle groups
        select muscle groups




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