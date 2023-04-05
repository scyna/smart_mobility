using scyna;
using FluentValidation;

namespace Registering;

public class RegisterUserHandler : Endpoint.Handler<PROTO.RegisterUserRequest>
{
    public override void Execute()
    {
        var validator = new RequestValidator();
        if (!validator.Validate(request).IsValid)
        {
            throw scyna.Error.REQUEST_INVALID;
        }

        // context.RaiseEvent(new PROTO.RegistrationCreated
        // {
        //     ID = Engine.ID.Next(),
        //     Email = request.Email,
        //     Name = request.Name,
        // });
    }

    public class RequestValidator : AbstractValidator<PROTO.RegisterUserRequest>
    {
        public RequestValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(40);
        }
    }
}
