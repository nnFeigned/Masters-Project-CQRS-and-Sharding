using CQRS.Domain.Entities;

using MediatR;
using MongoDB.Bson;

namespace CQRS.Application.Photos.Commands;

public class CreateImageCommand : IRequest<Image>
{
    public required string FileName { get; set; }
}