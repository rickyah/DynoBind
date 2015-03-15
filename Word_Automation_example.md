Example: using LateBindingHelper library to control MS Word using automation:

```
IDynamic wordApp = BindingFactory.CreateAutomationBinding("Word.Application");

//Get Word objects to interop operations
IDynamic document = wordApp.Property("Documents").Get();

document
	.Method("Add")
	.Invoke();
IDynamic selection = wordApp.Property("Selection").Get();

string str = "Hello World!";

//Make workd visible
wordApp.Property("Visible").Set(true);

//Activate bold
selection.Method("BoldRun").Invoke();

//Change font size
selection.Property("Font").Get().Property("Size").Set(20);

foreach (char c in str)
{
	selection.Method("TypeText").AddParameter(c.ToString()).Invoke();
	System.Threading.Thread.Sleep(200);
}

//Hide Word
bool visibility = wordApp.Property("Visible").Get<bool>();
visibility = !visibility;
wordApp.Property("Visible").Set(visibility);

//We need to get the type of the enumeration, that breaks the Late Binding approach as we need
//a way to know a specific type, and it's aseembly
//Type enumType = Assembly
//      .LoadWithPartialName("Microsoft.Office.Interop.Word")
//      .GetType("Microsoft.Office.Interop.Word.WdSaveOptions", false);
//word.Call("Quit", Activator.CreateInstance(enumType)));

//But if we know the equivalent int to the enum value it will work
//WdSaveOptions.wdDoNotSaveChanges == 0

//Quit
wordApp.Method("Quit").AddParameter(0).Invoke();
```