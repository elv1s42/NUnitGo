[![Windows Build status](https://ci.appveyor.com/api/projects/status/github/elv1s42/nunitgo?branch=master&svg=true)](https://ci.appveyor.com/project/elv1s42/nunitgo/branch/master)
[![Build Status](https://travis-ci.org/elv1s42/NUnitGo.svg?branch=master)](https://travis-ci.org/elv1s42/NUnitGo)
[![NuGet Downloads](https://img.shields.io/nuget/dt/NUnitGo.svg)](https://www.nuget.org/packages/NUnitGo/) 
[![NuGet Version](https://img.shields.io/nuget/v/NUnitGo.svg)](https://www.nuget.org/packages/NUnitGo/)
[![Issue Count](https://codeclimate.com/github/elv1s42/NUnitGo/badges/issue_count.svg)](https://codeclimate.com/github/elv1s42/NUnitGo)
[![Lisence Information](https://img.shields.io/npm/l/express.svg)](https://github.com/elv1s42/NUnitGo/blob/master/LICENSE.txt)
[![Website](https://img.shields.io/badge/Website-visit-brightgreen.svg)](http://elv1s42.github.io/NUnitGo/)

# NUnitGo
NUnit 3.0 HTML reports

##  Installation

To install NUnitGo, run the following command in the [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console) 

**PM> Install-Package NUnitGo**

or download [latest release](https://github.com/elv1s42/NUnitGo/releases)

## Demo report and Project srte

Click [here](http://elv1s42.github.io/NUnitGo/ReportExample/) to view demo report (without screenshots).

Click [here](http://elv1s42.github.io/NUnitGo/) to visit site.

##  Usage

###  Generating report

#### Using NUnitGo with NUnit **Test** Attribute. 

The most simple way to add your test to HTML report is to add *NunitGoAction* Attribute for your Test method:

```csharp
[TestFixture]
public class TestClass1
{
    [Test, NunitGoAction]
    public void SimpleTest()
    {
        Assert.AreEqual(1, 1);
    }
}
```

You can also specify TestProject name and TestClass name, and set Guid string for your test. This names are used to generate hierachical test list. **Each test must have unique Guid string**.

```csharp
[TestFixture]
public class TestClass1
{
    [Test, NunitGoAction(
            "11111111-1111-1111-1111-111111111111", 
            "Project name", 
            "Class name", 
            "Test name")]
    public void SimpleTest()
    {
        Assert.AreEqual(1, 1);
    }
}
```
 
