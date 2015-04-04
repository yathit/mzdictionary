Web services are new ways of doing modular web application in Web 2.0 arena. The English-Burmese dictionary lookup web service is perfectly fit with the concept. A small unit of this lookup service could be integrated to form more useful applications. A lot of applications could be imagined such as browser widget for mouse over lookup, handy desktop webget, online translation and conversation agent like [tartar](http://code.google.com/p/tartar/).

## Supported Protocols ##
  1. SOAP 1.1
  1. SOAP 1.2

## Usages ##
#### ASP.NET Server/Client ####
  1. Download cs source codes and create a web service project in VS2008
  1. Generate dictionary data for MS SQL database using toMsSql.xls VBA script and A-Z.xls data
  1. Run generated SQL commands in your database
  1. Supply data access layer
  1. Generate disco web service client module (generate by VS IDE)
  1. Code ASPX

#### ASP.NET Client ####
  1. Generate disco web service client module using publically available web service such as from http://www.hnandar.com/DictionaryService.asmx(generate by VS IDE)
  1. Code ASPX

```
// aspx
<asp:TextBox ID="txtEnglishWord" runat="server" Width="80%" AutoPostBack="true" OnTextChanged="btnFind_Click" />
<asp:Button ID="btnFind" runat="server" Text="Find" onclick="btnFind_Click" />
<p><asp:Literal ID="litDefination" runat="server" /></p>
Number of words: <asp:Literal ID="litNum" runat="server" />
```
```
// C# code behind
protected void btnFind_Click(object sender, EventArgs e)
  {
    com.hnandar.ENMMDictionarylookupservice dicService = new com.hnandar.ENMMDictionarylookupservice();
    litNum.Text = dicService.NumberOfWords();
    litDefination.Text = dicService.Look(txtEnglishWord.Text);
  }
```

#### PHP Client ####
This implimentation require [NuSOAP](http://sourceforge.net/projects/nusoap/) PHP module.
```
<?
  require_once('nusoap.php'); 
  $wsdl = "http://www.hnandar.com/DictionaryService.asmx?WSDL";
  $client = new soapclient($wsdl, 'wsdl'); 
  $word = 'cafeteria';
  echo $client->call('Look', $param);
?>
```

_Note:_ Server source code for data access layer is not included, because it has security issue. Futhermore most web site will already have some form of data access layer. Basically the following code will help how it works:
```
// Web service call 
DataTable table = DataAccess.ExecuteSelect(
            "SELECT TOP 25 * FROM " + dicTable + " " +
            "WHERE Word LIKE @Word;",
            DataAccess.CreateParameterString(enWord));
```
```
// Data access layer implimentation
// Note: database access layer is implimented elsewhere
public class DataAccess {
  public static DataTable ExecuteSelect(string sqlString, params DbParameter[] parameters )
  {
    DbCommand command = CreateTextCommand();
    command.CommandText = sqlString;
    command.Connection.Open();
    DataTable table = ExecuteSelectImplementation( command, parameters );
    return table;
   }
}
```

## Demos ##
  * Publically available web service: http://www.hnandar.com/DictionaryService.asmx
  * Client demo: http://www.zawgyi.org/English_Myanmar_Online_Dictionary.aspx

## Future ##
Updating to dictionary data by authenticated web service user. This will be foundation for community based dictionary data building.

## Bug Report ##
[mailto:kyawtun@hnandar.com](mailto:kyawtun@hnandar.com)

## Authors ##
  1. Kyaw Tun - Hnandar Developer Network ([HDN](http://www.hnandar.com/))
  1. Mark Soe Min - [MMGeeks](http://www.mmgeeks.org) VBA for SQL generation