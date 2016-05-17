[![Windows Build status](https://ci.appveyor.com/api/projects/status/github/elv1s42/nunitgo?branch=master&svg=true)](https://ci.appveyor.com/project/elv1s42/nunitgo/branch/master)
[![NuGet Version](https://img.shields.io/nuget/v/NUnitGo.svg)](https://www.nuget.org/packages/NUnitGo/)
[![Lisence Information](https://img.shields.io/npm/l/express.svg)](https://github.com/elv1s42/NUnitGo/blob/master/LICENSE.txt)
[![Website](https://img.shields.io/badge/Website-visit-brightgreen.svg)](http://elv1s42.github.io/NUnitGo/)
[![NuGet Version and Downloads count](https://buildstats.info/nuget/NUnitGo)](https://www.nuget.org/packages/NUnitGo/)


# NUnitGo
###Newest version - 2.1.5, available in Nuget
###Creating HTML reports for NUnit tests
Main report page:
<p align="center">
  <img src="https://github.com/elv1s42/NUnitGo/blob/master/ReportScreenshots/mainPage.png?raw=true" alt="Main page screenshot">
</p>
Tests list page:
<p align="center">
  <img src="https://github.com/elv1s42/NUnitGo/blob/master/ReportScreenshots/testListPage.png?raw=true" alt="Test list page screenshot">
</p>
Test page:
<p align="center">
  <img src="https://github.com/elv1s42/NUnitGo/blob/master/ReportScreenshots/testPage.png?raw=true" alt="Test page screenshot">
</p>

## Project Wiki

Click [here](https://github.com/elv1s42/NUnitGo/wiki) to read project wiki.

## Demo report and Project site

Click [here](http://elv1s42.github.io/NUnitGo/ReportExample/) to view demo report (without screenshots).

Click [here](http://elv1s42.github.io/NUnitGo/) to visit site.

##  Usage example

The most simple way to add your test to NUnitGo HTML report is to add **`NunitGoAction`** Attribute for your Test method. To receive Emails with test result add **`Subsciption`** or **`SingleTestSubscription`** Attribute. For more information please read [wiki](https://github.com/elv1s42/NUnitGo/wiki) documentation.

```csharp
[TestFixture]
public class TestClass
{
    [Test, NunitGoAction]
    [Subsciption(Name = "TestSubscriptionName")]
    public void SimpleTest()
    {
        Assert.AreEqual(1, 1);
    }
}
```


[![Bitdeli Badge](https://d2weczhvl823v0.cloudfront.net/elv1s42/nunitgo/trend.png)](https://bitdeli.com/free "Bitdeli Badge")

