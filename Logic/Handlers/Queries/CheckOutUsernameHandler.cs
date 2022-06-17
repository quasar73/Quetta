using Common.Models.Queries;
using Data.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Logic.Handlers.Queries
{
    public class CheckOutUsernameHandler : IRequestHandler<CheckOutUsernameQuery, bool>
    {
        private readonly UserManager<User> userManager;
        public CheckOutUsernameHandler(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
        
        public async Task<bool> Handle(CheckOutUsernameQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.Username);

            return user != null;
        }
    }
}
