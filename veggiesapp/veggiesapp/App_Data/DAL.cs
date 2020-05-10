using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;


/// <summary>
/// Summary description for DAL
/// </summary>
public class DAL
{
    //private readonly static string dbconnectionstring = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
    public DAL()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Main Datbase Related Function

    /// <summary>
    /// Use null for Parameters if you dont have any parameters to pass
    /// </summary>
    //public DataSet GetDataSet(String cmdText, CommandType cmdType, MySqlParameter[] parameters)
    //{
    //    try
    //    {
    //        string conString = dbconnectionstring;
    //        using (MySqlConnection con = new MySqlConnection(conString))
    //        {
    //            con.Open();
    //            using (MySqlCommand cmd = new MySqlCommand(cmdText, con))
    //            {
    //                cmd.CommandType = cmdType;
    //                if (parameters != null)
    //                {
    //                    foreach (MySqlParameter parameter in parameters)
    //                    {
    //                        if (null != parameter) cmd.Parameters.Add(parameter);
    //                    }
    //                }
    //                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
    //                {
    //                    DataSet ds = new DataSet();
    //                    da.Fill(ds);
    //                    return ds;
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        throw ex;
    //    }
    //}

    /// <summary>
    /// Use null for Parameters if you dont have any parameters to pass
    /// </summary>
    //public DataTable GetDataTable(String cmdText, CommandType cmdType, MySqlParameter[] parameters)
    //{
    //    try
    //    {
    //        string conString = dbconnectionstring;
    //        using (MySqlConnection con = new MySqlConnection(conString))
    //        {
    //            con.Open();
    //            using (MySqlCommand cmd = new MySqlCommand(cmdText, con))
    //            {
    //                cmd.CommandType = cmdType;
    //                if (parameters != null)
    //                {
    //                    foreach (MySqlParameter parameter in parameters)
    //                    {
    //                        if (null != parameter) cmd.Parameters.Add(parameter);
    //                    }
    //                }
    //                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
    //                {
    //                    DataTable dt = new DataTable();
    //                    da.Fill(dt);
    //                    return dt;
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }

    //}

    ///// <summary>
    ///// Use null for Parameters if you dont have any parameters to pass
    ///// </summary>    
    //public int ExecuteNonQuery(String cmdText, CommandType cmdType, MySqlParameter[] parameters)
    //{
    //    //MySqlCommand cmd = null;
    //    MySqlCommand cmd = null;
    //    int queryResult = 0;
    //    try
    //    {
    //        string conString = dbconnectionstring;
    //        //using (MySqlConnection con = new MySqlConnection(conString))
    //        //{
    //        //    con.Open();
    //        //    cmd = new MySqlCommand(cmdText, con);
    //        //    cmd.CommandType = cmdType;
    //        //    if (parameters != null)
    //        //    {
    //        //        foreach (MySqlParameter parameter in parameters)
    //        //        {
    //        //            if (null != parameter) cmd.Parameters.Add(parameter);
    //        //        }
    //        //    }
    //        //    return (queryResult = cmd.ExecuteNonQuery());
    //        //}
    //        using (MySqlConnection con = new MySqlConnection(conString))
    //        {
    //            con.Open();
    //            //cmd = new MySqlCommand(cmdText, con);
    //            cmd = new MySqlCommand(cmdText, con);
    //            cmd.CommandType = cmdType;
    //            if (parameters != null)
    //            {
    //                foreach (MySqlParameter parameter in parameters)
    //                {
    //                    if (null != parameter) cmd.Parameters.Add(parameter);
    //                }
    //            }
    //            return (queryResult = cmd.ExecuteNonQuery());
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //HttpContext.Current.Response.Write(ex.StackTrace + "<BR>" + ex.Message);
    //        //return -100;
    //        throw ex;
    //    }
    //    finally
    //    {
    //        if (cmd != null)
    //        {
    //            cmd.Parameters.Clear();
    //            cmd.Dispose();
    //        }
    //    }
    //}

    ///// <summary>
    ///// Use null for Parameters if you dont have any parameters to pass
    ///// </summary>
    //public string ExecuteScalar(String cmdText, CommandType cmdType, MySqlParameter[] parameters)
    //{
    //    try
    //    {
    //        string RetStr = "";
    //        string conString = dbconnectionstring;
    //        using (MySqlConnection con = new MySqlConnection(conString))
    //        {
    //            con.Open();
    //            using (MySqlCommand cmd = new MySqlCommand(cmdText, con))
    //            {
    //                cmd.CommandType = cmdType;
    //                if (parameters != null)
    //                {
    //                    foreach (MySqlParameter parameter in parameters)
    //                    {
    //                        if (null != parameter) cmd.Parameters.Add(parameter);
    //                    }
    //                }

    //                object obj = cmd.ExecuteScalar();
    //                if (obj != null) RetStr = obj.ToString();
    //                return RetStr;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return "";
    //        throw ex;
    //    }

    //}

    //public int MailToSendSite(string tomail, string mailsubject, string frommail, string bodymail, string ccmail, string bccmail, string fromname)
    //{
    //    try
    //    {
    //        bool adminflag = false;
    //        DataTable dt = new DataTable();
    //        string HostName = "", password = "", User = "", DBCCEmail = "", DBBCCEmail = "";
    //        dt = (new DAL()).GetDataTable("select * from siteprofile", CommandType.Text, null);
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            HostName = dt.Rows[0]["mshost"].ToString();
    //            User = dt.Rows[0]["msusername"].ToString();
    //            password = dt.Rows[0]["mspass"].ToString();
    //            DBCCEmail = dt.Rows[0]["contccemail"].ToString();
    //            DBBCCEmail = dt.Rows[0]["contbccemail"].ToString();
    //            if (tomail == null || tomail.ToString() == "")
    //            {
    //                adminflag = true;
    //                tomail = dt.Rows[0]["contemail"].ToString();
    //            }
    //            if (frommail == null || frommail.ToString() == "")
    //                frommail = dt.Rows[0]["contemail"].ToString();
    //            if (fromname == null || fromname.ToString() == "")
    //                fromname = ConfigurationManager.AppSettings["SiteName"].ToString();
    //        }

    //        SmtpClient smtpClientC = new SmtpClient();
    //        MailMessage objMailC = new MailMessage();
    //        MailAddress objMailC_fromaddress = new MailAddress(frommail, fromname);
    //        MailAddress objMailC_toaddress = new MailAddress(tomail);

    //        objMailC.From = objMailC_fromaddress;
    //        objMailC.To.Add(objMailC_toaddress);

    //        objMailC.IsBodyHtml = true;

    //        objMailC.Subject = mailsubject;

    //        objMailC.Body = bodymail;

    //        if (ccmail != null && ccmail != "")
    //            objMailC.CC.Add(ccmail);
    //        else if (DBCCEmail != null && DBCCEmail != "" && adminflag == true)
    //        {
    //            objMailC.CC.Add(DBCCEmail);
    //            //objMailC.CC.Add("auropumps84@bsnl.in");
    //        }

    //        if (bccmail != null && bccmail != "")
    //            objMailC.Bcc.Add(bccmail);
    //        else if (DBBCCEmail != null && DBBCCEmail != "" && adminflag == true)
    //            objMailC.Bcc.Add(DBBCCEmail);

    //        smtpClientC.Host = HostName;
    //        smtpClientC.Credentials = new System.Net.NetworkCredential(User, password);
    //        smtpClientC.Port = 99;
    //        smtpClientC.Send(objMailC);
    //        return 1;
    //    }
    //    catch (Exception ex)
    //    {
    //        return 0;
    //    }
    //}

    #endregion

    //#region Page Content
    //public DataTable getFullPageDetails(string pagename)
    //{
    //    DataTable dt = (new DAL()).GetDataTable("Select * from insidepage where pagename='" + pagename + "'", CommandType.Text, null);
    //    return dt;
    //}

    //public DataTable getFullPageDetails(int pageid)
    //{
    //    DataTable dt = (new DAL()).GetDataTable("Select * from insidepage where id=" + pageid, CommandType.Text, null);
    //    return dt;
    //}

    //public DataTable getFullPageDetails2(string pagename)
    //{
    //    DataTable dt = (new DAL()).GetDataTable("Select * from insidepage2 where pagename='" + pagename + "'", CommandType.Text, null);
    //    return dt;
    //}

    //public DataTable getFullPageDetails2(int pageid)
    //{
    //    DataTable dt = (new DAL()).GetDataTable("Select * from insidepage2 where id=" + pageid, CommandType.Text, null);
    //    return dt;
    //}

    //#endregion


    public string GetMailTemplates(string templatePath)
    {
        string mailTemplate = "";
        if (System.IO.File.Exists(GetRootPath() + templatePath))
        {
            mailTemplate = System.IO.File.ReadAllText(GetRootPath() + templatePath);
        }
        return mailTemplate;
    }

    public bool IsSQLInjection(string str)
    {
        if (str.Contains("'") == true || str.Contains(" ") == true || str.Contains("(") == true || str.Contains(")") == true || str.Contains("\"") == true || str.Contains("*") == true || str.Contains("$") == true || str.Contains("&") == true || str.Contains("-") == true || str.Contains(";") == true || str.Contains("[") == true || str.Contains("]") == true || str.Contains("}") == true || str.Contains("{") == true || str.Contains("<") == true || str.Contains(">") == true || str.Contains("=") == true)
            return true;
        else
            return false;
    }

    public static string GetCMSPath()
    {
        return ConfigurationManager.AppSettings["CMSPath"].ToString();
    }

    public static string GetPostBackControl(Page page)
    {
        Control control = null;

        string ctrlname = page.Request.Params.Get("__EVENTTARGET");
        if (ctrlname != null && ctrlname != string.Empty)
        {
            control = page.FindControl(ctrlname);
        }
        else
        {
            foreach (string ctl in page.Request.Form)
            {
                Control c = page.FindControl(ctl);
                if (c is System.Web.UI.WebControls.Button)
                {
                    control = c;
                    break;
                }
            }
        }
        return control.ID;
    }

    public static string GetRootPath()
    {
        string completePath = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath).Replace("/", "\\");
        string rootPath = completePath + "\\";
        return rootPath;
    }

    public static string getIP()
    {
        string strHostName = "";
        strHostName = HttpContext.Current.Request.UserHostAddress;
        return strHostName;
    }

    public static bool ValidatePrice(string input)
    {
        int nnum = 0;
        foreach (char c in input)
        {

            if (c.ToString() != ".")
            {
                if (!Char.IsNumber(c))
                {
                    return false;
                    break;
                }
            }
            else
            {
                nnum = nnum + 1;
            }
        }

        if (nnum > 1)
        {
            return false;
        }
        else
        {

            return true;
        }
    }

    public static bool IsNumeric(string str)
    {
        if (str == null)
            return false;
        return (System.Text.RegularExpressions.Regex.IsMatch(str, @"^\d+$"));

    }

    public static bool isEmail(string inputEmail)
    {

        string strRegex = @"^[\w-]+(?:\.[\w-]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7}$";

        Regex reg = new Regex(strRegex);
        if (reg.IsMatch(inputEmail))
            return true;
        else
            return false;
    }

    public static DateTime CurrCountryTime()
    {
        return DateTime.UtcNow.AddHours(1);
    }

    public static string GetDateWithFormat(string onlydate = "")
    {
        if (onlydate == "")
            return DateTime.UtcNow.AddHours(1).ToString("yyyy-MM-dd HH:mm:ss");
        else
            return DateTime.UtcNow.AddHours(1).ToString("dd-MMM-yyyy");
    }

    public static void CreateCart()
    {
        DataTable Cart = new DataTable("Cart"); // Create a new DataTable titled 'airCart'
        DataColumn[] primaykey = new DataColumn[1];



        DataColumn idColumn = new DataColumn();
        idColumn.DataType = System.Type.GetType("System.Int32");
        idColumn.ColumnName = "loop";
        idColumn.AutoIncrement = true;
        idColumn.AutoIncrementSeed = 1;
        idColumn.Unique = true;
        Cart.Columns.Add(idColumn);
        primaykey[0] = idColumn;
        Cart.PrimaryKey = primaykey;

        //Cart.Columns.Add("id", System.Type.GetType("System.Int32"));
        //Cart.Columns.Add("Name", System.Type.GetType("System.String"));
        //Cart.Columns.Add("Image", System.Type.GetType("System.String"));
        //Cart.Columns.Add("Model", System.Type.GetType("System.String"));
        //Cart.Columns.Add("Size", System.Type.GetType("System.String"));
        Cart.Columns.Add("pid", System.Type.GetType("System.Int32"));
        //Cart.Columns.Add("Freight", System.Type.GetType("System.Int32"));
        //Cart.Columns.Add("SellerID", System.Type.GetType("System.Int32"));
        // Cart.Columns.Add("SellerName", System.Type.GetType("System.String"));
        //Cart.Columns.Add("MinimumQty", System.Type.GetType("System.Int32"));

        // Cart.Columns.Add("PackQty", System.Type.GetType("System.Int32"));
        Cart.Columns.Add("Qty", System.Type.GetType("System.Int32"));
        // Cart.Columns.Add("price", System.Type.GetType("System.Double"));
        //Cart.Columns.Add("amount", System.Type.GetType("System.Double"));

        //Cart.Columns.Add("100weight", System.Type.GetType("System.Double"));
        //Cart.Columns.Add("500weight", System.Type.GetType("System.Double"));

        //Cart.Columns.Add("Pack1qty", System.Type.GetType("System.Int32"));
        //Cart.Columns.Add("Pack2qty", System.Type.GetType("System.Int32"));
        //Cart.Columns.Add("Pack3qty", System.Type.GetType("System.Int32"));
        //Cart.Columns.Add("Pack4qty", System.Type.GetType("System.Int32"));
        //Cart.Columns.Add("Pack5qty", System.Type.GetType("System.Int32"));

        //Cart.Columns.Add("Pack1", System.Type.GetType("System.Double"));
        //Cart.Columns.Add("Pack2", System.Type.GetType("System.Double"));
        //Cart.Columns.Add("Pack3", System.Type.GetType("System.Double"));
        //Cart.Columns.Add("Pack4", System.Type.GetType("System.Double"));
        //Cart.Columns.Add("Pack5", System.Type.GetType("System.Double"));
        HttpContext.Current.Session["PSCart"] = Cart;
    }

    public static void AddcartRow(int productid, int qty)
    {
        DataTable dt = (DataTable)HttpContext.Current.Session["PSCart"];
        DataRow dr = dt.NewRow();
        dr[1] = productid;
        dr[2] = qty;
        dt.Rows.Add(dr);
        HttpContext.Current.Session["PSCart"] = dt;
    }

    public static bool Checksessioncart()
    {
        if (HttpContext.Current.Session["PSCart"] == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //    public static void AddSessionToCart()
    //    {
    //        if (HttpContext.Current.Session["PSCart"] != null)
    //        {
    //            DataTable dt = (DataTable)HttpContext.Current.Session["PSCart"];
    //            DataTable dt1 = new Login().CurrentUser();
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {

    //                string strQuery = "select * from cartmaster inner join product on cartmaster.productid=product.id inner join productcategory on productcategory.id=product.categoryid inner join clientmaster on clientmaster.id=cartmaster.clientid where cartmaster.clientid= " + Convert.ToInt32(dt1.Rows[0]["id"].ToString()) + "";
    //                DataTable dt2 = new DAL().GetDataTable(strQuery, CommandType.Text, null);
    //                if (dt2.Rows.Count > 0)
    //                {
    //                    DataRow[] rows = dt2.Select("productid ='" + Convert.ToInt32(dt.Rows[i]["pid"]) + "'");
    //                    if (rows.Length > 0)
    //                    {
    //                        int qty = Convert.ToInt32(dt2.Rows[0]["Qty"]) + Convert.ToInt32(dt.Rows[i]["Qty"]);
    //                        strQuery = @"update cartmaster 
    //                            SET Qty=@Qty,   
    //                                modifydate=@modifydate, modifyip=@modifyip
    //                            WHERE id=@id; ";
    //                        MySqlParameter[] parameters = { new MySqlParameter("@Qty", Convert.ToInt32(qty)),
    //                                            new MySqlParameter("@modifydate", DAL.GetDateWithFormat()),
    //                                            new MySqlParameter("@modifyip", DAL.getIP()),
    //                                            new MySqlParameter("@id", Convert.ToInt32(dt2.Rows[0]["id"])  )
    //                                            };

    //                        (new DAL()).ExecuteNonQuery(strQuery, CommandType.Text, parameters);

    //                    }
    //                    else
    //                    {
    //                        strQuery = @"insert into cartmaster(productid,clientid,Qty,addeddate,addedip) 
    //                                              values(@productid,@clientid,@Qty,@addeddate,@addedip);";
    //                        MySqlParameter[] parameters = { new MySqlParameter("@productid", Convert.ToInt32(dt.Rows[i]["pid"])),
    //                                                    new MySqlParameter("@clientid", Convert.ToInt32(dt1.Rows[0]["id"].ToString())),
    //                                                    new MySqlParameter("@Qty",dt.Rows[i]["Qty"]),
    //                                                    new MySqlParameter("@addeddate", DAL.GetDateWithFormat()),
    //                                                    new MySqlParameter("@addedip", DAL.getIP()),
    //                                                  };
    //                        (new DAL()).ExecuteNonQuery(strQuery, CommandType.Text, parameters);
    //                    }
    //                }
    //                else
    //                {
    //                    strQuery = @"insert into cartmaster(productid,clientid,Qty,addeddate,addedip) 
    //                                              values(@productid,@clientid,@Qty,@addeddate,@addedip);";
    //                    MySqlParameter[] parameters = { new MySqlParameter("@productid", Convert.ToInt32(dt.Rows[i]["pid"])),
    //                                                    new MySqlParameter("@clientid", Convert.ToInt32(dt1.Rows[0]["id"].ToString())),
    //                                                    new MySqlParameter("@Qty",dt.Rows[i]["Qty"]),
    //                                                    new MySqlParameter("@addeddate", DAL.GetDateWithFormat()),
    //                                                    new MySqlParameter("@addedip", DAL.getIP()),
    //                                                  };
    //                    (new DAL()).ExecuteNonQuery(strQuery, CommandType.Text, parameters);
    //                }
    //            }
    //        }
    //    }
    //    public static void CreateImage()
    //    {
    //        DataTable Image = new DataTable("Image"); // Create a new DataTable titled 'airCart'
    //        DataColumn[] primaykey = new DataColumn[1];

    //        DataColumn idColumn = new DataColumn();
    //        idColumn.DataType = System.Type.GetType("System.Int32");
    //        idColumn.ColumnName = "loop";
    //        idColumn.AutoIncrement = true;
    //        idColumn.AutoIncrementSeed = 1;
    //        idColumn.Unique = true;
    //        Image.Columns.Add(idColumn);
    //        primaykey[0] = idColumn;
    //        Image.PrimaryKey = primaykey;

    //        //Image.Columns.Add("id", System.Type.GetType("System.Int32"));
    //        Image.Columns.Add("Image", System.Type.GetType("System.String"));
    //        HttpContext.Current.Session["Image"] = Image;
    //    }

    //    public static string RandomPassword()
    //    {
    //        string _allowedChars = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    //        Random randNum = new Random();
    //        char[] chars = new char[6];
    //        int allowedCharCount = _allowedChars.Length;

    //        for (int i = 0; i < 6; i++)
    //        {
    //            chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
    //        }
    //        return new string(chars);
    //    }   

    //    public static int Setimage(Literal ltrlCaptcha)
    //    {
    //        Random rn = new Random();
    //        int n1 = rn.Next(0, 9);
    //        if (n1 == null || n1 < 0 || n1 > 9)
    //            n1 = 1;

    //        int n2 = rn.Next(0, 9);
    //        if (n2 == null || n2 < 0 || n2 > 9)
    //            n2 = 1;

    //        int op = rn.Next(1, 2);
    //        string[] strOp = { "+", "-" };//, "X" };
    //        string curOp = strOp[op - 1];

    //        string optext = "";

    //        if (n1 > n2)
    //            optext = Convert.ToString(n1) + " " + curOp + " " + Convert.ToString(n2) + " = ";
    //        else
    //            optext = Convert.ToString(n2) + " " + curOp + " " + Convert.ToString(n1) + " = ";

    //        ltrlCaptcha.Text = optext;


    //        int intRes = 0;

    //        if (curOp == "+")
    //            intRes = n1 + n2;
    //        else if (curOp == "-")
    //            intRes = n1 - n2;
    //        else if (curOp == "X")
    //            intRes = n1 * n2;

    //        return intRes;
    //    }

    //    public static bool CheckCaptcha(int CapNum, string CapStr)
    //    {
    //        bool ReturnVal = false;

    //        int intEn = 0;
    //        if (int.TryParse(CapStr, out intEn))
    //        {
    //            if (CapNum == intEn)
    //                ReturnVal = true;
    //        }

    //        return ReturnVal;
    //    }

    //    public void TakeBackup(string backuploc)
    //    {
    //        using (MySqlConnection conn = new MySqlConnection(dbconnectionstring))
    //        {
    //            using (MySqlCommand cmd = new MySqlCommand())
    //            {
    //                using (MySqlBackup mb = new MySqlBackup(cmd))
    //                {
    //                    cmd.Connection = conn;
    //                    conn.Open();
    //                    mb.ExportToFile(backuploc);
    //                    conn.Close();
    //                }
    //            }
    //        }
    //    }
    //}
    public enum CampaignStatus : int
    {
        Pending,
        Sent
    }
}
