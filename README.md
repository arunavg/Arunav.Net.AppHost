Arunav.Net.AppHost
--------

Arunav.Net.AppHost is an open source C# web server for self-hosting asp.net applications that are built with http handlers. Such asp.net web applications can ofcourse be hosted on Apache server with mod_mono or IIS with .Net Framework, however hardware constrained environment such as small IoT gateways where installing a webserver is not feasible AppHost can be the server.


Documentation
--------

Please have a look at [Arunav.Net](http://www.arunav.net/) where each project got a clear introduction and explanation about its functionnality.

AppHost Features
--------
* Supports Mono 6.+ and .NET Framework 2.0/3.0/3.5/4.0/4.5 up to 4.7.2
* Supports hosting **static and dynamic website**
* Supports hosting **wcf services**
* Supports **GET and POST methods**  
* Extensible plugin API for custom IoT services


Usage
-----

Develop ASP.NET web project with http modules and http handlers however instead of using `HttpContext` use `AppContext` with corresponding Request and Response.
The AppContext serves as an Adapter that provides a generalized API for accessing underlying context and corresponding family of objects.


Bug Report
----------

See the [Issues Report](https://github.com/arunavg/Arunav.Net.AppHost/issues) section of this repository.


License
-------

See LICENSE file for details.


Latest Update 
-------

**Initialize Public Release**