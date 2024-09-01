using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Logging;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger;

    public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, 
    IAppLogger<UpdateLeaveTypeCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        //Validate incoming data
        var validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (validatorResult.Errors.Count != 0)
        {
            _logger.LogWarning("Validation errors in update request for {0} - {1}",
                    nameof(LeaveType), request.Id);
            throw new BadRequestException("Invalid Leave type", validatorResult);
        }

        //Convert to domain entity object
        var leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);

        //Add to database
        await _leaveTypeRepository.CreateAsync(leaveTypeToUpdate);

        return Unit.Value;
    }
}