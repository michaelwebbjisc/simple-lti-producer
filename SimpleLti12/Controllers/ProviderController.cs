using System.IO;
using System.Net;
using LtiLibrary.Common;
using LtiLibrary.Extensions;
using System.Web.Mvc;
using LtiLibrary.Lti1;
using LtiLibrary.Outcomes;
using SimpleLti12.Models;

namespace SimpleLti12.Controllers
{
    public class ProviderController : Controller
    {
        // The most basic function of the Tool Provider is to receive basic launch requests from
        // the Tool Consumer.
        #region LTI 1.0 Tool Provider

        /// <summary>
        /// Display the tool requested by the Tool Consumer.
        /// </summary>
        /// <remarks>
        /// This is the basic function of a Tool Provider.
        /// </remarks>
        public ActionResult Tool()
        {
            try
            {
                // Parse and validate the request
                Request.CheckForRequiredLtiParameters();

                var ltiRequest = new LtiRequest(null);
                ltiRequest.ParseRequest(Request);

                if (!ltiRequest.ConsumerKey.Equals("12345"))
                {
                    ViewBag.Message = "Invalid Consumer Key";
                    return View();
                }

                var oauthSignature = Request.GenerateOAuthSignature("secret");
                if (!oauthSignature.Equals(ltiRequest.Signature))
                {
                    ViewBag.Message = "Invalid Signature";
                    return View();
                }

                // The request is legit, so display the tool
        
                var model = new ToolModel
                {
                    ConsumerSecret = "secret",
                    LtiRequest = ltiRequest

              
                };

                ViewBag.Message = "Hello " + ltiRequest.LisPersonNameFull;
                return View(model);
            }
            catch (LtiException e)
            {
                ViewBag.Message = e.Message;
                return View();
            }
        }

        #endregion


       

       
    }
}