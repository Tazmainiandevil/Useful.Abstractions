# Useful.Abstractions
Abstractions for C# .NET 4.5+ code to aid testable code

<image src="https://ci.appveyor.com/api/projects/status/github/Tazmainiandevil/Useful.Abstractions?branch=master&svg=true">
[![NuGet version](https://badge.fury.io/nu/Useful.Abstractions.svg)](https://badge.fury.io/nu/Useful.Abstractions)

The current version only has abstractions for the ```C# System.ConfigurationManager``` and ```C# System.Configuration``` classes but I have added some additional methods to the interface.

Usually the configuration is injected in but for the examples below it is instantiated.

_HasSetting_ - Checks to see if the configuration has a given setting

e.g. 
```C#
 var wrapper = new ConfigurationManagerWrapper();
 if(wrapper.HasSetting("IsValue"))
 {
    // Do some logic
 }
```

this can also be used on a section

e.g. 
```C#
 var wrapper = new ConfigurationManagerWrapper();
 if(wrapper.HasSetting("IsValue", "mySection"))
 {
    // Do some logic
 }
```

_HasConnectionString_ - The similar to HasSetting but for the connection strings

e.g. 
```C#
 var wrapper = new ConfigurationManagerWrapper();
 if(wrapper.HasConnectionString("default"))
 {
    // Do some logic
 }
```

_GetSetting_ - Get the setting from the configuration and convert it to the desired type

e.g. if the configuration had 
```XML
 <appSettings>
   <add key="Timeout" value="10"/>
 </appSettings>
```
```C#
 var wrapper = new ConfigurationManagerWrapper();
 var timeout = wrapper.GetSetting<int>("Timeout");
```
timeout would be 10

_GetSettingOrDefault_ - Get the setting from the configuration but if there is no entry or no value return the default

e.g. with no configuration entry
```C#
 var wrapper = new ConfigurationManagerWrapper();
 var timeout = wrapper.GetSettingOrDefault<int>("Timeout");
```
timeout would be 0

The default can also be a given value

e.g.
```C#
 var wrapper = new ConfigurationManagerWrapper();
 var timeout = wrapper.GetSettingOrDefault<int>("Timeout", -1);
```
timeout would be -1
