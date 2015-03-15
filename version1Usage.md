# **Note: this doc is deprecated, and it's only useful if you are using the 1.** version of the library, which is something you definitely don't want **#**

# Introduction #

Short description with usage. For More information [check this CodeProject article](http://www.codeproject.com/KB/cs/LateBindingHelper.aspx)

# Details #

All late binding functionality is based on the interface 'LateBindingHelper.ILateBindingFacade'

Adquisition of 'ILateBinding' interfaces is done using a Factory static class:
The 'LateBindingFactory.CreateObjectBinding' allows creation of a 'ILateBindingFacade' given an instance of an object, or using the 'System.Type' for the object:
```
MyClass myClassInstance = new MyClass();

//Late Binding calls will be dispatched to the myClassInstance object

ILateBindingFactory lbf1 = LateBindingFactory.CreateObjectLateBinding( myClassInstance );

//Creates a new MyClass object instance where the late binding call will be dispatched 
//using the default constructor
ILateBindingFactory lbf2 = LateBindingFactory.CreateObjectLateBinding( typeof(MyClass) );

int integerArgument = 2008;
//Creates a new MyClass object instance where the late binding call will be dispatched passing
//arguments to the constructor
ILateBindingFactory lbf3 = LateBindingFactory.CreateObjectLateBinding( typeof(MyClass),  integerArgument );
```

The 'LateBindingObject.CreateAutomationLateBinding(') factory makes a binding to an Automation aplication given its Running Object Table (ROT) name:
```
ILateBindingFactory word = LateBindingFactory.CreateAutomationLateBinding("word.application");
```

Once we have an instance of an ILateBindingFacade inteface, we can send any operation we want
to the object using late binding:

**Call a method:**

```
//Method call without parameters and no return value
myLateBindingFacade.Call("Method1");

//Method call with parameters and no return value (or we don't need the return value)
int arg1 = 1;
double arg2 = 2.3;
myLateBindingFacade.Call( "Method2", Args.Build(arg1, arg2) );

//Method call without parameters and a return value
int result;
result = myLateBindingFacade.Call<int>("Method3");
result = (int)myLateBindingFacade.Call<object>("Method3");

//Method call with parameters and a return value
int result;
int arg1 = 1;
double arg2 = 2.3;
result = myLateBindingFacade.Call<int>("Method2", Args.Build(arg1, arg2));
result = (int)myLateBindingFacade.Call<object>("Method2", Args.Build(arg1, arg2));
```

**Call a method with reference parameters:
```
int refParam = 10;
double notRefParam1 = 20;
bool notRefParam2 = true;

//Save the arguments
object[] args = Args.Build(notRefparam1, notRefParam2, refValue);

//Make the call. Args.ByRefIndexs indicates the index number of the arguments
//which are passed as reference
myLateBindingFacade.Call("MultiplyByFive", Args.ByRefIndexs(2), args);

//Recover the new value for the parameter passed by reference
refValue = (int)args[2];
```**

**Property or field access:
```
//Get property value
bool boolResult;
boolResult = myLateBindingFacade.Get<bool>("Property1");
boolResult = (bool)myLateBindingFacade.Get("Property1");

//Get field value
int intResult;
intResult = myLateBindingFacade.Get<int>("field1");
intResult = (int)myLateBindingFacade.Get("field1");

//Set property value
myLateBindingFacade.Set("Property1", true);

//Set field value
myLateBindingFacade.Set("field1", 10);
```**

