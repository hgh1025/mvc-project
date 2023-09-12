using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;

namespace HuniMVC.Infrasturcture.Sessions
{
    public class SessionStore : ISessionStore
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public SessionStore(UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public ISession Create(string sessionKey, TimeSpan idleTimeout, TimeSpan ioTimeout, Func<bool> tryEstablishSession, bool isNewSessionKey)
        {
            DistributedSessionStore.Equals(sessionKey, isNewSessionKey);
            DistributedSession.ReferenceEquals(idleTimeout, isNewSessionKey);
            throw new NotImplementedException();
        }
        //public class DistributedSession
        //{
        //        public string SessionKey { get; set; }  
        //    public TimeSpan idleTimeout { get; set; } = TimeSpan.Zero;
        //    public TimeSpan ioTimeout { get; set; }

        //    public bool IsAvailable { get; }
        //    public Task CommitAsync(CancellationToken cancellationToken = default)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
    }
}
