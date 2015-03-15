This library aims to simplify late binding calls in C#, which are painfully complex in C# specially if we compare it with how VB.NET handles late binding calls.
With C#4.0 the **dynamic** keyword will be added so -with time- this library will be deprecated. But C# 4.0 isn't out yet, and even when it comes out, there will be still  a lot of projects using previous versions of the language which can benefit from this library.



# Introduction: Early binding & Late binding #

In a early binding call we specify an operation and, if the signature for that operation is correct, the compiler binds the code for that operation at compile time. E.g:
```
int result = calculator.Add( 15, 16 )
```
When this code is build the C# compiler checks if the calculator object instance defines an operation with the name 'Add', that takes 2 integers as parameters, and returns  another integer. If the operation with that concrete signature is found for the calculator object, the compiler _binds_ that call to the actual code which performs the operation. Otherwise a compile-time error is issued as the compiler can't find an operation matching that signature.
All of this is possible because the compiler has information about the calculator object instance type.

But, what if we don't have that information?
That could happen with interop scenarios (e.g. getting a COM object which exists outside the .NET Framework, Maybe we have the specs defining the operations the object exports, but we can't have the compiler reference that information).
How do we made calls if the compiler doesn't have that information?
That's where the _late binding_ calls make their appearance.

With a late binding call, the binding to the actual code -searching for the operation with that signature- is postponed to runtime, when we have more information about the type.

That implies that no type checks can be performed at compile time using late binding calls as we did't have type information in first place. As a consequence, if we call an operation in a object instance using late bindind and no operation matching signature is found, a failure will occur, but the important thing to remember is that **it will show up at runtime**



# Using the library #

All late binding functionality is based on the interface  `LateBindingHelper.IDynamic`

Adquisition of an `IDynamic` interface is done using a Factory: `LateBindingHelper.BindingFactory` which allows creation of an `IDynamic` binded to an object instance, using various possibilities:

  * Using an instance of an object as a "prototype"
  * sing a 'System.Type' to create a new instance of the Type at runtip (optionally arguments for the constructor can be also specified)
  * Using strings defining the Assembly and the Type of the object instance.
  * Using an string which defines an specific application ProccessID

E.g:
```
MyClass myClassInstance = new MyClass();

//Late Binding calls will be dispatched to the myClassInstance object
IDynamic lbf1 = BindingFactory.CreateObjectBinding( myClassInstance );

//Creates a new MyClass object instance where the late binding call will be dispatched 
//using the default constructor
IDynamic lbf2 = BindingFactory.CreateObjectBinding( typeof(MyClass) );

int integerArgument = 2008;
//Creates a new MyClass object instance where the late binding call will be dispatched passing
//arguments to the constructor
IDynamic lbf3 = BindingFactory.CreateObjectBinding( typeof(MyClass),  integerArgument );
```

'BindingFactory.CreateAutomationBinding()' factory makes a binding to an Automation application given its Running Object Table (ROT) name
or ProcID:
```
IDynamic word = BindingFactory.CreateAutomationBinding("word.application");
```

Once we have an instance of an IDynamic interface, we can send any operation we want
to the object using the interface, and the operation will be issued at runtime:

## Calling a method ##

```
//Method call without parameters and no return value
myLBInvoker
	.Method("Method1")
	.Invoke();

//Method call with parameters and no return value (or we don't need the return value)
int arg1 = 1;
double arg2 = 2.3;
myLBInvoker
	.Method( "Method2" )
	.AddParameter(arg1)
	.AddParameter(arg2)
	.Invoke();

//Method call without parameters and a return value of type int
int result;
result = myLBInvoker
			.Method("Method3")
			.Invoke<int>();

//Or with explicit casting			
result = (int)myLBInvoker.Method("Method3")
			.Invoke<object>()			

//Method call with parameters and a return value of type int
int result;
int arg1 = 1;
double arg2 = 2.3;
result = myLBInvoker
			.Method("Method2")
			.AddParameter(arg1)
			.AddParameter(arg2))
			.Invoke<int>()

//Or with explicit casting			
result = (int)myLBInvoker.Method("Method2")
			.AddParameter(arg1)
			.AddParameter(arg2))
			.Invoke<object>()
```

## Calling a method with reference parameters ##
```
int refValue = 10;
double notRefParam1 = 20;
bool notRefParam2 = true;

//Make the call. Args.ByRefIndexs indicates the index number of the arguments
//which are passed as reference
myLBInvoker
	.Method("MultiplyByFive")
	.AddParameter(notRefParam1)
	.AddParameter(notRefParam2)
	.AddRefParameter(refValue)
	.Invoke();

//Recover the new value for the parameter passed by reference
refValue = (int)myLBInvoker.LastCallParameters[2];
```

## Accessing Properties or fields ##
```
//Get property value
bool boolResult;
boolResult = myLBInvoker
		.Property(("Property1")
		.Get<bool>();

//With explicit cast
boolResult = (bool)myLBInvoker
			.Property("Property1")
			.Get<object>();

//Get field value
int intResult;
intResult = myLBInvoker
		.Field("field1")
		.Get<int>();
			
//With explicit cast
intResult = (int)myLBInvoker
		.Field("field1")
		.Get<object>();

//Set property value
myLBInvoker
	.Property("Property1")
	.Set(true);

//Set field value
myLBInvoker
	.Field("field1")
	.Set(10);
```

## Index accessing or assignament ##
```

//Get object at index position
string result;
result = myLBInvoker
	.Index(5)
	.Get<string>();
			
//Set value for an index position
myLBInvoker
	.Index(4)
	.Set("[myNewValue]");
	
//Works with any object as indexer:
result = myLBInvoker
	.Index("myStringIndexer")
	.Get<string>();
myLBInvoker
	.Index("anotherStringIndexer")
	.Set("[myNewValue]");
```

## One more thing: IDynamic as return type ##
When retrieving a value without specifying the return type you'll receive an working instance of an object defining the
`IDynamic` interface. That means that you can concatenate late binding call on different objects:
```
IDynamic yetAnotherOrdersManager = BindingFactory.CreateBinding("MyOrdersManager");


int firstOrderID = yetAnotherOrdersManager 
          .Property("Orders").Get()       //Here we return a list of orders
             .Index(0).Get()    //We access the first order... hoping we have at least one order stored so no nasty exception is throw.
                .Field("orderID").Get<int>();              //Access orderID field and save it.
```

# More examples #
The code includes some test units that also serves as code use examples.

A test disabled by default also includes code for creating a binding to MS Word and control the application with the library using Automation. The code is also listed in the following wiki page: Word\_Automation\_example