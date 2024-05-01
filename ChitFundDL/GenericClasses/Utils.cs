using System;
using Microsoft.VisualBasic;
using System.Xml;
using SQL;
using SQL1;
public class Utils
{
    //    Public Static bool LogError(Exception Ex, String Message = "")
    //    {
    //        Try
    //        {
    //            String msg = String.Empty;
    //            if (!Ex == null)
    //                msg = "Exception : " + Ex.Message + Constants.vbCrLf;
    //            Interaction.MsgBox(msg);
    //        }
    //        Catch (Exception ex1)
    //        {
    //        }
    //        Return True;
    //    }
    
    public static void ReadConfigFile(bool boolNetworkLibrary = true)
    {
        try
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNodeList nodes;
            XmlNodeList bnodes;
            String strServerName = "";
            String strDataBaseName = "";
            String strUserName = "";
            String strPassword = "";
            xmlDoc.Load(System.Environment.CurrentDirectory + @"\config.xml");
            nodes = xmlDoc.GetElementsByTagName("DataBaseConnection");
            foreach (XmlNode node in nodes)
            {
                bnodes = node.ChildNodes;
                foreach (XmlNode bnode in bnodes)
                {
                    if(bnode.Name == "ServerName")
                        strServerName = bnode.InnerText;
                    if(bnode.Name == "DataBaseName")
                        strDataBaseName = bnode.InnerText;
                    if(bnode.Name == "UserName")
                        strUserName = bnode.InnerText;
                    if(bnode.Name == "Password")
                        strPassword = bnode.InnerText;
                }
            }
            ConnectionSettings.SQLServerName = strServerName;
            ConnectionSettings.SQLUserID = strUserName;
            ConnectionSettings.SQLPassword = strPassword;
            ConnectionSettings.SQLDatabaseName = strDataBaseName;

            // ConnectionSettings.cnString = "initial catalog=" & strDataBaseName & ";data source=" & strServerName & ";user id=" & strUserName & ";pwd=" & strPassword & ";Connect Timeout=200"
            if(boolNetworkLibrary)
            {
                ConnectionSettings.SQLHasNetworkLibrary = true;
                ConnectionSettings.cnString = "data source=" + strServerName + ";Network Library=DBMSSOCN;initial catalog=" + strDataBaseName + ";user id=" + strUserName + ";pwd=" + strPassword + ";Connect Timeout=200";
            }
            else
            {
                ConnectionSettings.SQLHasNetworkLibrary = false;
                ConnectionSettings.cnString = "data source=" + strServerName + ";initial catalog=" + strDataBaseName + ";user id=" + strUserName + ";pwd=" + strPassword + ";Connect Timeout=200";
            }
        }
        catch(Exception)
        {
            //essageBox("ReadConfigFile: Error reading from config.xml");
        }
    }
    //    Public Static String GetConnectionStringByDatabase(String strDatabaseName)
    //    {
    //        String strConnectionString = String.Empty;
    //        if (Len(SQLServerName) > 0 && Len(SQLUserID) > 0 && Len(SQLPassword) > 0)
    //        {
    //            if (SQLHasNetworkLibrary)
    //                strConnectionString = "data source=" + SQLServerName + ";Network Library=DBMSSOCN;initial catalog=" + strDatabaseName + ";user id=" + SQLUserID + ";pwd=" + SQLPassword + ";Connect Timeout=200";
    //            Else
    //                strConnectionString = "data source=" + SQLServerName + ";initial catalog=" + strDatabaseName + ";user id=" + SQLUserID + ";pwd=" + SQLPassword + ";Connect Timeout=200";
    //        }
    //        Return strConnectionString;
    //    }

    //    Private Static String DeCrypt(String Text)
    //    {
    //        String strTempChar = String.Empty;
    //        // Encrypts/decrypts the passed string using 
    //        // a simple ASCII value-swapping algorithm
    //        int i;
    //        For (i = 1; i <= Strings.Len(Text); i++)
    //        {
    //            strTempChar = System.Convert.ToString(Strings.AscW(Mid(Text, i, 1)) - 128);
    //            ;/* Cannot convert AssignmentStatementSyntax, CONVERSION ERROR: Conversion for MidExpression Not implemented, please report this issue in 'Mid$(Text, i, 1)' at character 4068
    //   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.DefaultVisit(SyntaxNode node)
    //   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitMidExpression(MidExpressionSyntax node)
    //   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MidExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
    //   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
    //   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
    //   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitMidExpression(MidExpressionSyntax node)
    //   at Microsoft.CodeAnalysis.VisualBasic.Syntax.MidExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
    //   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitAssignmentStatement(AssignmentStatementSyntax node)
    //   at Microsoft.CodeAnalysis.VisualBasic.Syntax.AssignmentStatementSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
    //   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
    //   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
    //   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

    //Input: 
    //            Mid$(Text, i, 1) = ChrW(CType(strTempChar, Integer))

    // */
    //        }
    //        Return Text;
    //}
}
