using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypesDetails;

public record GetLeaveTypeDetailsQuery(int Id) : IRequest<LeaveTypeDetailDto>;