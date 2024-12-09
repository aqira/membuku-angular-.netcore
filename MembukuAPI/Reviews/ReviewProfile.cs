using AutoMapper;
using MembukuAPI.Authors;
using MembukuAPI.Reviews.ReviewDtos;

namespace MembukuAPI.Reviews;

public class ReviewProfile : Profile {
    public ReviewProfile() {
        CreateMap<Review, ReviewDto>()
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Book.Author));
        CreateMap<Review, ReviewRowDto>()
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Book.Author));
        CreateMap<Author, ReviewAuthorDto>();
        CreateMap<CreateReviewDto, Review>();
        CreateMap<UpdateReviewDto, Review>();
        CreateMap<UpdateReviewRatingDto, Review>();
    }
}