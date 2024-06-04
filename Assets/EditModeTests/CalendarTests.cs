using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using PLu.Mars.Kernel;
using System;

public class CalendarTestsScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void DayNumberTest()
    {
        double t = Calendar.DayLength * 669 - 1;
        double t2 = Calendar.DayLength * 669;
        double t3 = Math.Floor(t2 / Calendar.DayLength % Calendar.DaysPerYear);
        Debug.Log(t3);
        Assert.AreEqual(57801599, t);
        Assert.AreEqual(57801600, t2);
        
        Assert.AreEqual(667, Calendar.DayNumber(Calendar.DayLength * 667));
        Assert.AreEqual(0, Calendar.DayNumber(Calendar.DayLength * 668));
        Assert.AreEqual(1, Calendar.DayNumber(Calendar.DayLength * 669));
        Assert.AreEqual(667, Calendar.DayNumber(Calendar.DayLength * 668 - 1));
    }

    [Test]
    public void MonthTest()
    {
        Assert.AreEqual(1, Calendar.Month(Calendar.DayLength * 668));
        Assert.AreEqual(24, Calendar.Month(Calendar.DayLength * 668 - 1));
        Assert.AreEqual(3, Calendar.Month(Calendar.DayLength * 56));
    }

    [Test]
    public void DayTest()
    {
        Assert.AreEqual(1, Calendar.Day(Calendar.DayLength * 0));
        Assert.AreEqual(28, Calendar.Day(Calendar.DayLength * 27));
        Assert.AreEqual(1, Calendar.Day(Calendar.DayLength * 28));
        Assert.AreEqual(1, Calendar.Day(Calendar.DayLength * 28 * 2));
        Assert.AreEqual(28, Calendar.Day(Calendar.DayLength * 28 * 2 - 1));
        Assert.AreEqual(1, Calendar.Day(Calendar.DayLength * 668));
        Assert.AreEqual(28, Calendar.Day(Calendar.DayLength * 667));
        Assert.AreEqual(28, Calendar.Day(Calendar.DayLength * 668 - 1));
    }

    [Test]
    public void HourTest()
    {
        Assert.AreEqual(0, Calendar.Hour(Calendar.DayLength * 669));
        Assert.AreEqual(1, Calendar.Hour(Calendar.DayLength * 669 + 3600));
        Assert.AreEqual(23, Calendar.Hour(Calendar.DayLength * 669 + 86400 - 3600));
    }
    [Test]
    public void MinutesTest()
    {
        Assert.AreEqual(0, Calendar.Minutes(Calendar.DayLength * 669));
        Assert.AreEqual(1, Calendar.Minutes(Calendar.DayLength * 669 + 60));
        Assert.AreEqual(59, Calendar.Minutes(Calendar.DayLength * 669 + 3600 - 60));
    }
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator CalendarTestsScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        yield return null;
    }
}
