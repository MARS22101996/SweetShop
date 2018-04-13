using AutoMapper;
using SweetShop.BLL.Dto;
using SweetShop.BLL.Interfaces;
using SweetShop.DAL.Entities;
using SweetShop.DAL.Interfaces;

namespace SweetShop.BLL.Services
{
   public class FeedbackService : IFeedbackService
   {
      private readonly IUnitOfWork _unitOfWork;
      private readonly IMapper _mapper;

      public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper)
      {
         _unitOfWork = unitOfWork;
         _mapper = mapper;
      }

      public void Create(FeedbackDto feedbackDto)
      {
         var feedback = _mapper.Map<Feedback>(feedbackDto);

         _unitOfWork.Feedbacks.Create(feedback);

         _unitOfWork.Save();
      }
   }
}
