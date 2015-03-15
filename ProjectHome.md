Simple library which aims to simplify late binding calls with C#

Make simple late binding calls using a [fluent interface](http://www.martinfowler.com/bliki/FluentInterface.html):

```
IDynamic myObject = BindingFactory.CreateObjectBinding("CoreAssembly", "CExecutionManager");
int size = myObject.Property("CommandExecuter").Get<IDynamic>()
       .Method("Execute").AddParameter("GET SIZE").Invoke<int>();
```

Please check the [Usage](http://code.google.com/p/latebindinghelper/wiki/Usage) page for the help on using this library.

A deeply description of the 1.**version of the library is available at CodeProject:
[Late Binding Helper Library in CodeProject](http://www.codeproject.com/KB/cs/LateBindingHelper.aspx)**
