using NUnit.Framework;
using UnityEngine;

public class MashTunTests
{
    private GameObject _testObject;
    private MashTun _mashTun;

[SetUp]
public void SetUp()
{
    _testObject = new GameObject();
    _mashTun = _testObject.AddComponent<MashTun>();
}

[TearDown]
public void TearDown()
    {
        Object.DestroyImmediate(_testObject);
    }

[Test]
public void PerfectWaterLevelShouldGive100Points()
    {
        float result = _mashTun.CalculatePoints(70f);
        Assert.AreEqual(100f, result);
    }

[Test]
public void WaterLevelSlightlyAboveOptimalShouldGive50Points()
    {
        float result = _mashTun.CalculatePoints(85f);
        Assert.AreEqual((100f-(15f*100f)/30f), result);
    }
    
[Test]
public void WaterLevelSlightlyBelowOptimalShouldGive50Points()
    {
        float result = _mashTun.CalculatePoints(55f);
        Assert.AreEqual((100f-(15f*100f)/30f), result);
    }

[Test]
public void WaterLevelExactlyAtToleranceBoundryShouldGive0Points()
    {
        float result = _mashTun.CalculatePoints(100f);
        Assert.AreEqual(0f, result);
    }

[Test]
public void WaterLevelWayAboveOptimalShouldGive0Points()
    {
        float result = _mashTun.CalculatePoints(110f);
        Assert.AreEqual(0f, result);
    }

[Test]
public void WaterLevelWayBelowOptimalShouldGive0Points()
    {
        float result = _mashTun.CalculatePoints(20f);
        Assert.AreEqual(0f, result);
    }
}