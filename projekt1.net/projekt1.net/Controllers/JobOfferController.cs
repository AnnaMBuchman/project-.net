using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projekt1.net.Models;
using projekt1.net.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Authorization;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;

namespace projekt1.net.Controllers
{
	[Route("api/[controller]/[action]")]
	//[ApiController]
	public class JobOfferController : Controller
	{
		private readonly DataContext _context;
		private readonly BlobStorageService _blob;
		private readonly IHostingEnvironment _env;
		public readonly IConfiguration config;

		public JobOfferController(DataContext context, IHostingEnvironment env, BlobStorageService blob, IConfiguration configuration)
		{
			_context = context;
			_blob = blob;
			_env = env;
			config = configuration;
		}
		/// <summary>
		/// Get all products from the list
		/// </summary>
		/// <returns>All elements will be returned</returns>
		[HttpGet]
		//[Route("blablabla")]
		public async Task<IActionResult> Index([FromQuery(Name = "search")] string searchString)
		{
			if (string.IsNullOrEmpty(searchString))
				return View(await _context.JobOfers.Include(x => x.Company).ToListAsync());

			List<JobOffer> searchResult = await _context.JobOfers.Include(x => x.Company).Where(o => o.JobTitle.Contains(searchString)).ToListAsync();

			{ }
			return View(searchResult);
		}
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return BadRequest($"id shouldn't not be null");
			}
			var offer = await _context.JobOfers.FirstOrDefaultAsync(x => x.Id == id.Value);
			if (offer == null)
			{
				return NotFound($"offer not found in DB");
			}

			return View(offer);
		}
		[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<IActionResult> Apply(int? id)
		{
			var model = new JobApplication() { OfferId=id.Value};
			var offer = await _context.JobOfers.FirstOrDefaultAsync(x => x.Id == model.OfferId);
			
			ViewData["JobName"] = offer.JobTitle;

			return View(model);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Apply(JobApplication model,[FromForm]IFormFile formFile)
		{
			#region Read File Content  

			var uploads = Path.Combine(_env.WebRootPath, "uploads");
			bool exists = Directory.Exists(uploads);
			if (!exists)
				Directory.CreateDirectory(uploads);
			
			var fileName = Path.GetFileName(formFile.FileName);
			//var fileStream = new FileStream(Path.Combine(uploads, formFile.FileName), FileMode.Create);
			string mimeType = formFile.ContentType;
			byte[] fileData = null;
			                using (var memoryStream = new MemoryStream())
				                {
				formFile.CopyTo(memoryStream);
				fileData = memoryStream.ToArray();
				               }
			
			model.CvUrl = _blob.UploadFileToBlob(formFile.FileName, fileData, mimeType);
			//application.File = model.File;
			Execute();
			#endregion
			//var offer = await _context.JobOfers.FirstOrDefaultAsync(x => x.Id == model.OfferId);
			//offer.JobApplications.Add(application);
			//_context.Update(offer);
			await _context.JobApplications.AddAsync(model);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> DownloadFile(int id)
        {
            var jo = await _context.JobApplications
                .SingleOrDefaultAsync(m => m.Id == id);

            (Stream blobStream, string ContentType, var name)= await _blob.DownloadFileAsync(jo.CvUrl);

            return File(blobStream, ContentType, name);

        }
	///// <summary>
	///// Creates a TodoItem.
	///// </summary>
	///// <remarks>
	///// Sample request:
	/////
	/////     POST /Todo
	/////     {
	/////        "id": 1,
	/////        "name": "Item1",
	/////        "isComplete": true
	/////     }
	/////
	///// </remarks>
	///// <param name="item"></param>
	///// <returns>A newly created TodoItem</returns>
	///// <response code="201">Returns the newly created item</response>
	///// <response code="400">If the item is null</response>
	[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(JobOffer model)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			var offer = await _context.JobOfers.FirstOrDefaultAsync(x => x.Id == model.Id);
			offer.JobTitle = model.JobTitle;
			_context.Update(offer);
			await _context.SaveChangesAsync();
			return RedirectToAction("Details", new { id = model.Id });
		}
		// DELETE api/products/5
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return BadRequest($"id should not be null");
			}

			_context.JobOfers.Remove(new JobOffer() { Id = id.Value });
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		[Authorize]
	//	[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<ActionResult> Create()
		{
			var model = new JobOfferCreateView
			{
				Companies = await _context.Companies.ToListAsync()
			};
			

			return View(model);
		}
		///// <summary>
		///// Creates a TodoItem.
		///// </summary>
		///// <remarks>
		///// Sample request:
		/////
		/////     POST /Todo
		/////     {
		/////        "id": 1,
		/////        "name": "Item1",
		/////        "isComplete": true
		/////     }
		/////
		///// </remarks>
		///// <param name="item"></param>
		///// <returns>A newly created TodoItem</returns>
		///// <response code="201">Returns the newly created item</response>
		///// <response code="400">If the item is null</response>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(JobOfferCreateView model)
		{
			if (!ModelState.IsValid)
			{
				model.Companies = await _context.Companies.ToListAsync();

				return View(model);
			}
		
				
			if (model.ValidUntil< DateTime.Today)
			{
				model.Companies = await _context.Companies.ToListAsync();
				ModelState.AddModelError("ValidUntil", "Date cannot be set to past date!");
				return View(model);
			}
			if (model.SalaryTo< model.SalaryFrom)
			{
				model.Companies = await _context.Companies.ToListAsync();
				ModelState.AddModelError("SalaryFrom", "Salary To can not be smaller than Salary Form!");
				return View(model);
			}
			if ( model.SalaryFrom==0)
			{
				model.Companies = await _context.Companies.ToListAsync();
				ModelState.AddModelError("SalaryFrom", "Salary From can not be equal 0!");
				return View(model);
			}
			if (model.SalaryTo ==0)
			{
				model.Companies = await _context.Companies.ToListAsync();
				ModelState.AddModelError("SalaryTo", "Salary To can not be equal 0!");
				return View(model);
			}





			JobOffer jo = new JobOffer
			{
				
				CompanyId = model.CompanyId,
			    Description = model.Description,
				JobTitle = model.JobTitle,
				Location = model.Location,
				SalaryFrom = model.SalaryFrom,
				SalaryTo = model.SalaryTo,
				ValidUntil = model.ValidUntil,
				Created = DateTime.Now
			};

			jo.Company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == model.CompanyId);
			await _context.JobOfers.AddAsync(jo);
			await _context.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		[HttpGet]
		//[ApiExplorerSettings(IgnoreApi = true)]
		public async Task<IActionResult> Details(int id)
		{
			var offer = await _context.JobOfers.FirstOrDefaultAsync(x => x.Id == id);
			await _context.JobApplications.ForEachAsync(x => { if (x.OfferId == offer.Id)
					offer.JobApplications.Add(x); });
			offer.Company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == offer.CompanyId);
			return View(offer);
		}

		public async Task Execute()
		{
			
			string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
			var client = new SendGridClient(apiKey);
			var from = new EmailAddress("01130560@pw.edu.pl", "Example User 1");
			List<EmailAddress> tos = new List<EmailAddress>
		  {
			  new EmailAddress("anna.m.buchman@gmail.com", "Example User 2")
			  
		  };

			var subject = "Hello world email from Sendgrid ";
			var htmlContent = "<strong>Hello world with HTML content</strong>";
			//var displayRecipients = false; // set this to true if you want recipients to see each others mail id 
			var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", htmlContent, false);
			var response = await client.SendEmailAsync(msg);
		}
	}
}  