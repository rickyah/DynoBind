DynoBind aims to simplify late binding calls with C# in .NET versions that does not support the [dynamic type system](https://msdn.microsoft.com/en-us/library/dd264736.aspx)

# Introduction
Using DynoBind you can make late binding calls in a simpler way using a [fluent interface](http://www.martinfowler.com/bliki/FluentInterface.html) instead of diving into .NET powerfull but complex Reflection system.


# Usage example
Let's try with an example. Suppose we have a `CExecutionManager` class whichexposes a property named `CommandExecuter` returning an object of type `CExecuter` that exposes a method  with the signagure `object Execute(string command)` which is the one we want to call. 

Let's see how to do this with .NET's reflection. Of course we are not dealing with error checks to keep the amount of code under control:

```
// Load the assembly and get the type you want to instantiate
Type CExecutionManagerType = Assembly.Load("CoreAssembly")
                .GetType("CExecutionManager", false);

// Create an instance of the type CExecutionManager
object executionMngInstance = new Invoker(Activator.CreateInstance(CExecutionManagerType));

// Call the method/property and get 
object cExecuterInstance = CExecutionManagerType
            .InvokeMember("CommandExecuter",
                   BindingFlags.GetProperty,
                   null,
                   executionMngInstance,
                   null);


// Arguments to pass to the Execute method call
object[] args = new object[] { "GET SIZE" };
 
object result = cExecuterInstance.GetType()
    .InvokeMember("Execute",
            BindingFlags.InvokeMethod,
            null,
            cExecuterInstance,
            args);
            
// The Execute method can return multiple types so it need to return an object.
// We can now cast the result to the type you are actually expecting, 
// as a user of the library you know that the "GET SIZE" command should 
// return an integer:
int actualResult = (int)result;
```


Now let's compare how to do it with DynoBind

```
// Create the instance of CExecutionManager
IDynamic executionMngInstance = BindingFactory.CreateObjectBinding("CoreAssembly", "CExecutionManager");

try
{
    int size = executionMngInstance
            // For the property "CommandExecuter"...
            .Property("CommandExecuter")
            // get its value.
            .Get<IDynamic>()
            // Then, in the object the property returned we want to call the method...
            .Method("Execute")
            // with this parameter...
            .AddParameter("GET SIZE")
            // Here the method is invoked and we expect it to return an integer
            .Invoke<int>();
} catch (Exception ex)
{
    //something went wrong!
}
```
We even added error checks just because it is so easy! ;)

# Documentation

For more help on using this library please check the [Usage wiki page](http://code.google.com/p/latebindinghelper/wiki/Usage)

A deeply description of the 1.**version of the library is available at CodeProject:
[Late Binding Helper Library in CodeProject](http://www.codeproject.com/KB/cs/LateBindingHelper.aspx)**
