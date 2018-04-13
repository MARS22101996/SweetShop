using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweetShop.BLL.Dto;
using SweetShop.BLL.Interfaces;
using SweetShop.WEB.Model;

namespace SweetShop.WEB.Controllers
{
   [Route("api/feedback")]
   public class FeedbackController : Controller
    {
       private readonly IMapper _mapper;
       private readonly IFeedbackService _feedbackService;

       public FeedbackController(
          IMapper mapper,
          IFeedbackService feedbackService)
       {
          _mapper = mapper;
          _feedbackService = feedbackService;
       }

       [HttpPost]
       public IActionResult Post([FromBody] FeedbackApiModel feedbackApiModel)
       {
          if (ModelState.IsValid)
          {
             var feedbackDto = _mapper.Map<FeedbackDto>(feedbackApiModel);
             _feedbackService.Create(feedbackDto);

             return Ok(feedbackApiModel);
          }

          return BadRequest(ModelState);
       }

   }
}