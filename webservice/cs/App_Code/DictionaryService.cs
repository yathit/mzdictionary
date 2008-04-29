using System;
using System.Collections;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using Hnandar.DataAccessLib;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for DictionaryService
/// </summary>
[WebService(Namespace = "http://hnandar.com/", Name = "EN->MM Dictionary lookup service", Description = "Show meaning of English word in Burmese")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class DictionaryService : System.Web.Services.WebService
{

    public DictionaryService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }



    string dicTable = "em1";

    [WebMethod(EnableSession = false, Description = "Lookup dictionary")]
    public string Look(string enWord)
    {
        enWord = enWord.Trim().Replace("?", "_").Replace("*", "%");
        DataTable table = DataAccess.ExecuteSelect(
            "SELECT TOP 25 * FROM " + dicTable + " " +
            "WHERE Word LIKE @Word;",
            DataAccess.CreateParameterString(enWord));

        if (table.Rows.Count == 0)
        {
            return "No defination for " + enWord;
        }

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < table.Rows.Count; i++)
        {
            sb.Append("<p>");
            sb.Append(table.Rows[i][0].ToString()).Append(": ");
            sb.Append("(").Append(table.Rows[i][1].ToString().Trim()).Append(") ");
            sb.Append(table.Rows[i][2].ToString());
            sb.Append("</p>");
        }
        return sb.ToString();
    }

    [WebMethod(EnableSession = false, Description = "Show number of words in the dictionary.")]
    public string NumberOfWords()
    {
        return DataAccess.ExecuteScalar("SELECT COUNT(*) From " + dicTable + ";");
    }

}

