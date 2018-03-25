using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using AarhusWebDevCoop.ViewModels;
using System.Collections.Specialized;
using System.Net.Mail;
using Umbraco.Core.Models;

namespace AarhusWebDevCoop.Controllers
{
	public class ContactFormController : SurfaceController
	{
		// GET: ContactForm
		public ActionResult Index()
		{
			return PartialView("_ContactForm", new ContactForm());
		}
		[HttpPost]
		public ActionResult HandleFormSubmit(ContactForm model)
		{
			if (!ModelState.IsValid) { return CurrentUmbracoPage(); }
			//send mail n' stuff
			TempData["success"] = true;
			TempData["Name"] = model.Name;

			MailMessage message = new MailMessage();
			message.To.Add("INSERT-MAIL-HERE");
			message.Subject = model.Subject;
			message.From = new MailAddress(model.Email, model.Name);
			message.Body = model.Message;

			using (SmtpClient smtp = new SmtpClient())
			{
				smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtp.UseDefaultCredentials = false;
				smtp.EnableSsl = true;
				smtp.Host = "smtp.gmail.com";
				smtp.Port = 587;
				//Insert own credentials
				smtp.Credentials = new System.Net.NetworkCredential("INSERT-GMAIL-HERE", "INSERT-PASSWORD-HERE");
				smtp.EnableSsl = true;
				// send mail
				smtp.Send(message);
			}

			IContent comment = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "Message");
			comment.SetValue("messageName", model.Name);
			comment.SetValue("email", model.Email);
			comment.SetValue("subject", model.Subject);
			comment.SetValue("messageContent", model.Message);
			//save
			Services.ContentService.Save(comment);


			return RedirectToCurrentUmbracoPage();
		}
	}
}