# NUnitGo
Real-time genereted HTML reports with screenshots for NUnit 2.6.4

[![Build Status](https://travis-ci.org/elv1s42/NUnitGo.svg?branch=master)](https://travis-ci.org/elv1s42/NUnitGo)

## Installation and Usage

 1. Download NUnit **2.6.4** from [official site](http://www.nunit.org/).
 2. Download latest release from [NUnitGo releases](https://github.com/elv1s42/NUnitGo/releases).
 3. Unpack binaries to **%NUnit_installation_directory%\bin\addins**.
 4. In **%NUnit_installation_directory%\bin\addins\config.xml** specify absolute path to any folder (this folder will be created automatically) where **HTML** report will be generated (e.g. **&lt;output-path>C:\test-results\NUnitGoReport&lt;/output-path>** or **&lt;output-path>/home/user/test-results/NUnitGoReport&lt;/output-path>**). You can also specify in configuration whether you want to have test output to be written to attachments and whether you want to generate report after each test finished or after each test suite finished.
 6. Run your tests with **NUnit GUI** or **nunit-console** using .NET 4.0 (e.g. nunit-console YourAssembly.dll /framework=net-4.0).
 7. After all tests finish you'll see new folder that you specified on step 5 with generated **HTML** report.

## Demo report

Click [here](http://elv1s42.github.io/NUnitGo/reportexample/) to view demo report (without screenshots).

## Project site

Click [here](http://elv1s42.github.io/NUnitGo/) to visit site.
