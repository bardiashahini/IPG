using MyIPGServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReturnPage : System.Web.UI.Page
{
    #region [ Variables ]
    const string OKResultCode = "OK";
    const string CanceledByUser = "CancelByUser";
    const string SystemException = "Exception";
    string receivedResultCode = string.Empty;

    PGServiceClient client = new MyIPGServiceReference.PGServiceClient();
    wsContext context = null;

    #endregion

    #region [ Events ]
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //client.ClientCredentials.ClientCertificate.Certificate = new X509Certificate2(Server.MapPath("client.p12"), "changeit");
            var cert = new X509Certificate2(Server.MapPath("client.p12"), "changeit");
            //var cert = new X509Certificate2(Server.MapPath("smtc-certficate.p12"), "SmtcBank@147852");
            //var cert = new X509Certificate2(Server.MapPath("client.p12"), "123456");
            client.ClientCredentials.ClientCertificate.Certificate = cert;
            //TODO: In Operation Mode Delete the bellow line.
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(delegate { return true; });


            if (!Page.IsPostBack)
            {

                if (Request.Form.Count > 0)
                {
                    txtRequestIdentifier.Text = Request.Form["RequestIdentifier"].ToString();
                    txtReferenceCode.Text = Request.Form["ReferenceCode"].ToString();
                    txtMessage.Text = Request.Form["Message"].ToString();
                    receivedResultCode = txtResultCode.Text = Request.Form["ResultCode"].ToString();

                    if (receivedResultCode == OKResultCode)
                    {
                        LogingMethod("100000100", "Hh123456@");
                        //LogingMethod("100000035", "r86@QKM4jeYEbd5vo8Be");
                        //LogingMethod("100000015", "'N!Y4!Ce25m5vJq5pzPZ3");
                        string[] referenceCode = new string[] { Request.Form["ReferenceCode"].ToString() };
                        context = (wsContext)Session["Context"];
                        VerifyTransaction(context, referenceCode);
                        LogOutMethod(context);

                    }
                    if (receivedResultCode == CanceledByUser)
                    {
                        lblMessage.Text = "انصراف توسط کاربر";
                    }
                    if (receivedResultCode == SystemException)
                    {
                        lblMessage.Text = "خطای سیستمی";
                    }

                }
            }
        }

        catch (Exception exc)
        {
            lblMessage.Text = exc.Message;
        }
    }


    #endregion

    #region [ Private Methods ]

    private void SetWsContextValue(string sessionID)
    {
        context = new wsContext();
        context.value = sessionID;

        Session["Context"] = context;
    }
    private string LogingMethod(string username, string password)
    {
        string sessionID = "";

        try
        {
            sessionID = client.login(username, password);
            SetWsContextValue(sessionID);

        }
        catch (FaultException<InvalidCredentialException> invExc)
        {
            lblMessage.Text = "نام کاربری یا کلمه عبور صحیح نمی باشد" + invExc.Message;
        }

        catch (FaultException<BlockUserException> blkExc)
        {
            lblMessage.Text = "حساب مسدود گردیده است" + blkExc.Message;
        }
        catch (Exception exc)
        {
            lblMessage.Text = exc.Message;
            throw exc;
        }
        return sessionID;


    }
    private void LogOutMethod(wsContext wsContext)
    {

        try
        {
            client.logout(wsContext);

        }
        catch (FaultException<ValidationException> validationExc)
        {
            lblMessage.Text = "" + " " + validationExc.Message;
        }
        catch (FaultException<InvalidSessionException> invalidSessionExc)
        {
            lblMessage.Text = "" + " " + invalidSessionExc.Message;
        }
        catch (Exception)
        {
            throw;
        }

    }
    private verifyResponseResult[] VerifyTransaction(wsContext context, string[] referenceCode)
    {
        verifyResponseResult[] result = null;
        try
        {
            result = client.verifyTransaction(context, referenceCode);
        }
        catch (FaultException<ValidationException> validationExc)
        {
            lblMessage.Text = "" + " " + validationExc.Message;
        }
        catch (FaultException<InvalidSessionException> invalidSessionExc)
        {
            lblMessage.Text = "" + " " + invalidSessionExc.Message;
        }
        catch (Exception exc)
        {
            lblMessage.Text = exc.Message;
            throw exc;
        }

        return result;
    }
    private string ReverseTransaction(wsContext context, string referenceCode)
    {

        //TODO: Complete web method calling
        string result = string.Empty;

        client.reverseTransaction(context, new reverseRequestDTO { referenceCode = referenceCode });


        return result;


        //TODO: Exception List

    }
    private reportTransactionResult ReportTransaction(wsContext context, string referenceCode)
    {
        //TODO: Complete web method calling
        reportTransactionResult result = null;
        try
        {
            client.reportTransaction(context, new reportRequestDTO { referenceCode = referenceCode });
        }
        catch (FaultException<ValidationException> validationExc)
        {
            lblMessage.Text = "" + " " + validationExc.Message;
        }
        catch (FaultException<InvalidSessionException> invalidSessionExc)
        {
            lblMessage.Text = "" + " " + invalidSessionExc.Message;
        }
        catch (Exception exc)
        {
            lblMessage.Text = exc.Message;
            throw exc;
        }

        return result;
    }
    private detailReportResponseResult[] DetailReportTransaction(wsContext context, long transactionID)
    {
        //TODO: Complete web method calling

        detailReportResponseResult[] result = null;
        try
        {
            result = client.detailReportTransaction(context, transactionID);
        }
        catch (FaultException<ValidationException> validationExc)
        {
            lblMessage.Text = "" + " " + validationExc.Message;
        }
        catch (FaultException<InvalidSessionException> invalidSessionExc)
        {
            lblMessage.Text = "" + " " + invalidSessionExc.Message;
        }
        catch (FaultException<InvalidTransactionIdException> invalidTransExc)
        {
            lblMessage.Text = "" + " " + invalidTransExc.Message;
        }
        catch (Exception exc)
        {
            lblMessage.Text = exc.Message;
            throw exc;
        }

        return result;

    }
    #endregion

}